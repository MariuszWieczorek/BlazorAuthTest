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

namespace MwTech.Application.Comarch.Commands.ImportMieszanekMarszruty;

public class ImportMieszanekMarszrutyCommandHandler : IRequestHandler<ImportMieszanekMarszrutyCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IComarchService _comarchService;
    private readonly IComarchDbContext _comarch;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<ImportMieszanekMarszrutyCommandHandler> _logger;

    public ImportMieszanekMarszrutyCommandHandler(IApplicationDbContext context,
        IComarchService comarchService,
        IComarchDbContext comarch,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUserService,
        ILogger<ImportMieszanekMarszrutyCommandHandler> logger
        )
    {
        _context = context;
        _comarchService = comarchService;
        _comarch = comarch;
        _dateTimeService = dateTimeService;
        _currentUserService = currentUserService;
        _logger = logger;
    }

    public async Task Handle(ImportMieszanekMarszrutyCommand request, CancellationToken cancellationToken)
    {



        // Pobieramy mieszanki wraz z domyślnymi recepturami z ComarchXL
        // pobieramy wszystkie bomy z Comarch
        var comarchBoms = await _comarchService.GetAllBoms();

        // bieżemy Bomy tylko na mieszanki
        var mieszanki = comarchBoms.Where(x => x.kod_towaru.StartsWith("MIE"));


        // grupujemy mieszanki po indeksie i symbolu receptury
        var groupedIndexes
            = mieszanki
            .GroupBy(x => new { x.kod_towaru, x.nazwa_towaru, x.symbol_receptury, x.id_receptury, x.ilosc_ewidencyjna, x.czas_mieszania, x.czas_filtrowania_1kg });


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
                _logger.LogInformation($"Error: Import Mieszanek, Brak Indeksu: {m.Key.kod_towaru}");
                throw new Exception($"Error: Import Mieszanek, Brak Indeksu: {m.Key.kod_towaru}");
            }

            // Pobieramy Wersje Produktu z MWTech
            var routeVersions = await _context.RouteVersions
                .Where(x => x.ProductId == product.Id)
                .ToListAsync();

            // Sprawdzamy, czy wśród pobranych wersji istnieje szukana wersja

            var existProductVersion = routeVersions
                .Where(x => x.VersionNumber == m.Key.id_receptury).Any();



            // jeżeli istnieje wersja  i ma identyczny numer jak w ComarchXL to nic nie robimy
            // jeżeli nie istnieje lub ma inny numer to ją zakładamy
            if (existProductVersion)
            {
                continue;
            }

            // usuwamy z wszystkich wersji znacznik domyślna
            foreach (var item in routeVersions)
            {
                item.DefaultVersion = false;
                item.ComarchDefaultVersion = false;
            }

            var routeVersion = new RouteVersion
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
                IsActive = true,
                IfsDefaultVersion = false,
                ComarchDefaultVersion = true,
                ToIfs = false,
                CreatedByUserId = _currentUserService.UserId,
                CreatedDate = _dateTimeService.Now,
            };

            _context.RouteVersions.Add(routeVersion);

            _logger.LogInformation($"XL_IMPORT_MARSZRUTY: {product.Id} / {product.ProductNumber} / {m.Key.id_receptury}");

            

            decimal value = 0m;

            try
            {
                value = Convert.ToDecimal(m.Key.czas_mieszania);
            }
            catch (Exception)
            {
                string msg = $"nie można przekonwertować {m.Key.czas_mieszania} na decimal";
                value = 0;
                //throw new Exception(msg);
            }

            var resourceId = _context.Resources.SingleOrDefault(x=>x.ResourceNumber == "XL.MIKSER").Id;
            var operationId = _context.Operations.SingleOrDefault(x=>x.OperationNumber == "XL.MIESZANIE").Id;

            var manufactoringRoute = new ManufactoringRoute
            {
            RouteVersion = routeVersion,    
            OrdinalNumber = 1,
            ResourceId = resourceId,
            WorkCenterId = resourceId,
            OperationId = operationId,
            OperationLabourConsumption = value,
            OperationMachineConsumption = value
            };

            _context.ManufactoringRoutes.Add(manufactoringRoute);

            await _context.SaveChangesAsync();
        }

        return;
    }

}
