using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductCosts.Queries.GetProductCosts;

public class GetProductCostsQueryHandler : IRequestHandler<GetProductCostsQuery, ProductCostsViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IProductCostService _productCostService;

    public GetProductCostsQueryHandler(IApplicationDbContext context, IProductCostService productCostService)
    {
        _context = context;
        _productCostService = productCostService;
    }
    public async Task<ProductCostsViewModel> Handle(GetProductCostsQuery request, CancellationToken cancellationToken)
    {


        var productCosts = _context.Products
           .AsNoTracking()
           .Include(x => x.ProductCategory)
           .Join(_context.AccountingPeriods, p => true, a => true, (p, a) => new { p, a })
           .ToList()
           .GroupJoin(_context.ProductCosts
           .Include(x => x.Currency)
           .Include(x=>x.CreatedByUser)
           .Include(x=>x.ModifiedByUser),
            pa => new { p1 = pa.p.Id, p2 = pa.a.Id },
            c => new { p1 = c.ProductId, p2 = c.AccountingPeriodId },
            (pa, c) => new { pa, c })
           .SelectMany(x => x.c.DefaultIfEmpty(),
           (p, c) => new ProductCostDto
           {
               Id = c?.Id,
               PeriodId = p.pa.a.Id,
               PeriodNumber = p.pa.a.PeriodNumber,
               ProductId = p.pa.p.Id,
               ProductNumber = p.pa.p.ProductNumber,
               ProductName = p.pa.p.Name,
               ProductCategoryId = p.pa.p.ProductCategory.Id,
               ProductCategoryName = p.pa.p.ProductCategory.Name,
               Cost = c?.Cost,
               EstimatedCost = c?.EstimatedCost,
               CostInPln = null,
               Currency = c?.Currency,
               CurrencyId = c?.CurrencyId??0,
               CreatedByUser = c?.CreatedByUser?.UserName,
               ModifiedByUser = c?.ModifiedByUser?.UserName,
               CreatedDate = c?.CreatedDate,
               ModifiedDate = c?.ModifiedDate,
               Description = c?.Description,
               IsCalculated = c?.IsCalculated,
               IsImported = c?.IsImported
           }
           ).AsQueryable();


        if (request.ProductCostFilter == null)
        {
            var period = _context.AccountingPeriods.SingleOrDefault(x => x.IsDefault);
            int periodId = period != null ? period.Id : 0;

            request.ProductCostFilter = new ProductCostFilter
            {
                AccountingPeriodId = periodId
            };
        }



        productCosts = Filter(productCosts, request.ProductCostFilter);


        // stronicowanie
        if (request.PagingInfo != null)
        {

            request.PagingInfo.TotalItems = productCosts.Count();
            request.PagingInfo.ItemsPerPage = 10;

            if (request.PagingInfo.ItemsPerPage > 0 && request.PagingInfo.TotalItems > 0)
                productCosts = productCosts
                    .Skip((request.PagingInfo.CurrentPage - 1) * request.PagingInfo.ItemsPerPage)
                    .Take(request.PagingInfo.ItemsPerPage);
        }

        var productCostsList = productCosts.ToList();

        foreach (var item in productCostsList)
        {
            if (item.Cost != null)
                item.CostInPln = _productCostService.CalcutateToPln((decimal)item.Cost, item.Currency, item.PeriodId);
            
            if (item.EstimatedCost != null)
                item.EstimatedCostInPln = _productCostService.CalcutateToPln((decimal)item.EstimatedCost, item.Currency, item.PeriodId);
        }


        

        var vm = new ProductCostsViewModel
            { 
              ProductCosts = productCostsList,
              ProductCostFilter = request.ProductCostFilter,
              AccountingPeriods = await _context.AccountingPeriods.ToListAsync(),
              Currencies = await _context.Currencies.ToListAsync(),
              PagingInfo = request.PagingInfo
            };

        return vm;
           
    }

    private IQueryable<ProductCostDto> Filter(IQueryable<ProductCostDto> productCosts, ProductCostFilter productCostFilter)
    {
        if (productCostFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(productCostFilter.ProductNumber))
                productCosts = productCosts
                    .Where(x => x.ProductNumber.Contains(productCostFilter.ProductNumber));

            if (productCostFilter.AccountingPeriodId != 0)
                productCosts = productCosts
                    .Where(x => x.PeriodId == productCostFilter.AccountingPeriodId);

            if (productCostFilter.ProductCategoryId != 0)
                productCosts = productCosts
                    .Where(x => x.ProductCategoryId == productCostFilter.ProductCategoryId);

            if (productCostFilter.CurrencyId != 0)
                productCosts = productCosts
                    .Where(x => x.CurrencyId == productCostFilter.CurrencyId);

            if (productCostFilter.ShowNoSavedPositions == false)
                productCosts = productCosts
                    .Where(x => x.Id != null);

        }
        return productCosts;
    }

    
}
