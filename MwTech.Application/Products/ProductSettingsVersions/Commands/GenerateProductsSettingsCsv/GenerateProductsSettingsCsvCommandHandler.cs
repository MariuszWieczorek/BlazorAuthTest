using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using MwTech.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.ProductSettingsVersions.Commands.GenerateProductsSettingsCsv;

public class GenerateProductsSettingsCsvCommandHandler : IRequestHandler<GenerateProductsSettingsCsvCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<GenerateProductsSettingsCsvCommandHandler> _logger;
    private readonly IProductCsvService _productCsvService;

    public GenerateProductsSettingsCsvCommandHandler(IApplicationDbContext context,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUser,
        ILogger<GenerateProductsSettingsCsvCommandHandler> logger,
        IProductCsvService productCsvService
        )
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _currentUser = currentUser;
        _logger = logger;
        _productCsvService = productCsvService;
    }
    public async Task Handle(GenerateProductsSettingsCsvCommand request, CancellationToken cancellationToken)
    {

        var products = await _context.Products
                    .Include(x => x.ProductCategory)
                    .Where(y => y.ProductCategory.CategoryNumber == request.ProductCategoryNumber)
                    .AsNoTracking()
                    .ToListAsync();

        foreach (var item in products)
        {
            await _productCsvService.GenerateCsv(item.Id,0,CsvTrigger.CsvGenerator);
        }

        return;
    }

    
}
