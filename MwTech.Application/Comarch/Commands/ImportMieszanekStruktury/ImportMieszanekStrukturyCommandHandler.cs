using MediatR;
using MwTech.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;
using MwTech.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace MwTech.Application.Comarch.Commands.ImportMieszanekStruktury;

public class ImportMieszanekStrukturyCommandHandler : IRequestHandler<ImportMieszanekStrukturyCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IComarchService _comarchService;
    private readonly IComarchDbContext _comarch;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<ImportMieszanekStrukturyCommandHandler> _logger;

    public ImportMieszanekStrukturyCommandHandler(IApplicationDbContext context,
        IComarchService comarchService,
        IComarchDbContext comarch,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUserService,
        ILogger<ImportMieszanekStrukturyCommandHandler> logger
        )
    {
        _context = context;
        _comarchService = comarchService;
        _comarch = comarch;
        _dateTimeService = dateTimeService;
        _currentUserService = currentUserService;
        _logger = logger;
    }

    public async Task Handle(ImportMieszanekStrukturyCommand request, CancellationToken cancellationToken)
    {



        // Pobieramy mieszanki wraz z domyślnymi recepturami z ComarchXL
        // pobieramy wszystkie bomy z Comarch
        var comarchBoms = await _comarchService.GetAllBoms();

        // bieżemy Bomy tylko na mieszanki
        var mieszanki = comarchBoms.Where(x => x.kod_towaru.StartsWith("MIE"));


        // grupujemy mieszanki po indeksie i symbolu receptury
        var groupedIndexes
            = mieszanki
            .GroupBy(x => new { x.kod_towaru, x.nazwa_towaru, x.symbol_receptury, x.id_receptury, x.ilosc_ewidencyjna });

        // pobieramy wszystkie indeksy z MwTech
        var products = await _context.Products.ToListAsync();


        // pobieramy zapętlające się indeksy (nawroty, kapsle itd)
        var loopedIndexes = _comarchService.GetLoopedIndexes();


        // iterujemy po zgrupownym indeks + receptura 
        foreach (var m in groupedIndexes)
        {


            // Sprawdzamy czy indeks znajduje się w MwTech, po starym indeksie
            var product = products.SingleOrDefault(x => x.OldProductNumber == m.Key.kod_towaru);

            if (product == null)
            {
                _logger.LogInformation($"Import Mieszanek, Brak Indeksu: {m.Key.kod_towaru}");
                throw new Exception($"Import Mieszanek, Brak Indeksu: {m.Key.kod_towaru}");
            }

            // Pobieramy Wersje Produktu z MWTech
            var productVersions = await _context.ProductVersions
                .Where(x => x.ProductId == product.Id)
                .ToListAsync();

            // Sprawdzamy, czy wśród pobranych wersji istnieje szukana wersja

            var existProductVersion = productVersions
                .Where(x => x.VersionNumber == m.Key.id_receptury).Any();



            // jeżeli istnieje wersja  i ma identyczny numer jak w ComarchXL to nic nie robimy
            // jeżeli nie istnieje lub ma inny numer to ją zakładamy
            if (existProductVersion)
            {
                continue;
            }

            // usuwamy z wszystkich wersji znacznik domyślna
            foreach (var item in productVersions)
            {
                item.DefaultVersion = false;
                item.ComarchDefaultVersion = false;
            }

            var productVersion = new ProductVersion
            {
                Product = product,
                ProductQty = m.Key.ilosc_ewidencyjna,
                Name = m.Key.symbol_receptury,
                VersionNumber = m.Key.id_receptury,
                Accepted01ByUserId = _currentUserService.UserId,
                Accepted01Date = _dateTimeService.Now,
                IsAccepted01 = true,
                Accepted02ByUserId = _currentUserService.UserId,
                Accepted02Date = _dateTimeService.Now,
                IsAccepted02 = true,
                DefaultVersion = true,
                ComarchDefaultVersion = true,
                ToIfs = false,
                IfsDefaultVersion = false,
                IsActive = true,
                CreatedByUserId = _currentUserService.UserId,
                CreatedDate = _dateTimeService.Now,
            };

            _context.ProductVersions.Add(productVersion);

            _logger.LogInformation($"XL_IMPORT_BOM: {product.ProductNumber} {m.Key.id_receptury}");



            // jeżeli indeks nie zapętla się, to pobieramy jego BOM
            if (!loopedIndexes.Where(x => x.LoopedIndex.Equals(m.Key.kod_towaru.Trim())).Any())
            {


                var comarchProductBoms = comarchBoms.Where(x => x.kod_towaru == m.Key.kod_towaru);

                foreach (var comarchProductBom in comarchProductBoms)
                {
                    var part = products
                        .SingleOrDefault(x => x.OldProductNumber == comarchProductBom.kod_skladnika);
                    
                    if (part == null)
                    {
                        part = products
                        .SingleOrDefault(x => x.ProductNumber == comarchProductBom.kod_skladnika);
                    }


                    if (part == null)
                    {
                        _logger.LogInformation($"Import Mieszanek, Brak składnika: {comarchProductBom.kod_skladnika}");
                        throw new Exception($"Import Mieszanek, Brak składnika: {comarchProductBom.kod_skladnika}");
                    }


                    var bom = new Bom
                    {
                        SetVersion = productVersion,
                        Set = product,
                        PartQty = comarchProductBom.ilosc_skladnika, // * 1000,
                        PartId = part.Id
                    };

                    _context.Boms.Add(bom);
                }
            }

             await _context.SaveChangesAsync();

        }



        //--
        return;
    }


}
