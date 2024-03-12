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

namespace MwTech.Application.Comarch.Commands.ImportMieszanekSkladniki;

public class ImportMieszanekSkladnikiCommandHandler : IRequestHandler<ImportMieszanekSkladnikiCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IComarchService _comarchService;
    private readonly IComarchDbContext _comarch;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<ImportMieszanekSkladnikiCommandHandler> _logger;

    public ImportMieszanekSkladnikiCommandHandler(IApplicationDbContext context,
        IComarchService comarchService,
        IComarchDbContext comarch,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUserService,
        ILogger<ImportMieszanekSkladnikiCommandHandler> logger
        )
    {
        _context = context;
        _comarchService = comarchService;
        _comarch = comarch;
        _dateTimeService = dateTimeService;
        _currentUserService = currentUserService;
        _logger = logger;
    }

    public async Task Handle(ImportMieszanekSkladnikiCommand request, CancellationToken cancellationToken)
    {
        
        // pobieramy wszystkie bomy z Comarch
        var allBoms = await _comarchService.GetAllBoms();

        // bieżemy Bomy tylko na mieszanki
        var mieszanki = allBoms.Where(x => x.kod_towaru.StartsWith("MIE"));

        // grupujemy po składnikach
        var comarchGrpIndexes
            = mieszanki.GroupBy(x => new { x.kod_skladnika, x.nazwa_skladnika });

        // pobieramy wszystkie indeksy z MwTech
        var products = await _context.Products.ToListAsync();

        int kgUnitId = 12;

        foreach (var m in comarchGrpIndexes)
        {
            // pomijami indeksy które są w MwTech
            // w polu productNumber lub w polu OldProductNumber


            if (!products.Where(x => x.ProductNumber == m.Key.kod_skladnika).Any())
                if (!products.Where(x => x.OldProductNumber == m.Key.kod_skladnika).Any())
                {
                    var product = new Product
                    {
                        ProductNumber = m.Key.kod_skladnika,
                        OldProductNumber = m.Key.kod_skladnika,
                        Name = m.Key.nazwa_skladnika,
                        ProductCategoryId = m.Key.kod_skladnika.StartsWith("MIE") ? 2 : 22,
                        UnitId = kgUnitId, // kg
                        IsActive = true,
                        CreatedByUserId = _currentUserService.UserId,
                        CreatedDate = _dateTimeService.Now,
                        Description = "import z Comarch",
                    };

                    _logger.LogWarning($"XL_IMPORT_SKLADNIK: {m.Key.kod_skladnika}");


                    await _context.Products.AddAsync(product);

                    // Dla indeksów różnych niż mieszanki zakładamy numer wersji 
                    // Dla mieszankek wersja zostanie założona w procedurze
                    if (!m.Key.kod_skladnika.StartsWith("MIE"))
                    {
                        var productVersion = new ProductVersion
                        {
                            Product = product,
                            ProductQty = 1,
                            Name = "wersja 1",
                            VersionNumber = 1,
                            ProductWeight = 1,
                            DefaultVersion = true,
                            CreatedByUserId = _currentUserService.UserId,
                            CreatedDate = _dateTimeService.Now,
                            IsActive = true,
                            ComarchDefaultVersion = true,
                            ToIfs = false,
                            IfsDefaultVersion = false
                        };

                        await _context.ProductVersions.AddAsync(productVersion);

                        
                    }
                    await _context.SaveChangesAsync();
                }
        }

        return;
    }
}
