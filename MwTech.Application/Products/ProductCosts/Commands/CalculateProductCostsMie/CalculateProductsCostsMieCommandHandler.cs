using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductCosts.Commands.CalculateProductsCostsMie;

public class CalculateProductsCostsMieCommandHandler : IRequestHandler<CalculateProductsCostsMieCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<CalculateProductsCostsMieCommandHandler> _logger;
    private readonly IProductCostService _productCostService;

    public CalculateProductsCostsMieCommandHandler(IApplicationDbContext context,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUser,
        ILogger<CalculateProductsCostsMieCommandHandler> logger,
        IProductCostService productCostService
        )
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _currentUser = currentUser;
        _logger = logger;
        _productCostService = productCostService;
    }
    public async Task Handle(CalculateProductsCostsMieCommand request, CancellationToken cancellationToken)
    {

        var products = await _context.Products
            .Include(x => x.ProductCategory)
            .Where(x => x.ProductNumber.Contains("MIE.")
                && x.ProductCategoryId == 2 
                && x.ReturnedFromProd == false)
            .AsNoTracking()
            .ToListAsync();

        var prod0 = products.Where(x => !x.ProductNumber.Contains("_"));
        var prod1 = products.Where(x => x.ProductNumber.Contains("_1"));
        var prod2 = products.Where(x => x.ProductNumber.Contains("_2"));
        var prod3 = products.Where(x => x.ProductNumber.Contains("_3"));


        // ważna kolejność przeliczania 
        // np. aby najpierw przeliczyć koszt mieszanki 1 cyklu, później 2 cyklu itd
        foreach (var item in prod0)
        {
            await _productCostService.CaluclateProductCost(item.Id);
        }


        foreach (var item in prod1)
        {
            await _productCostService.CaluclateProductCost(item.Id);
        }

        foreach (var item in prod2)
        {
            await _productCostService.CaluclateProductCost(item.Id);
        }

        foreach (var item in prod3)
        {
            await _productCostService.CaluclateProductCost(item.Id);
        }


        return;
    }

    
}
