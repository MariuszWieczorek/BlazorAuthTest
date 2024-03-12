using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Products.Queries.GetProductTkw;

public class GetProductTkwQueryHandler : IRequestHandler<GetProductTkwQuery, ProductTkwViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetProductTkwQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProductTkwViewModel> Handle(GetProductTkwQuery request, CancellationToken cancellationToken)
    {
        int productVersionId = request.ProductVersionId;

        if (productVersionId == 0)
        {
            productVersionId = _context.ProductVersions
                .SingleOrDefault(x => x.ProductId == request.ProductId && x.DefaultVersion == true)?.Id??0;
        }


        
        var bomsTree = await _context.BomTrees
                    .FromSqlInterpolated($"select * from dbo.mwtech_bom_cte({request.ProductId},{productVersionId})")
                    //.Where(x => x.HowManyParts > 0  )
                    .OrderBy(x=>x.Level)
                    .AsNoTracking()
                    .ToListAsync();
        

        var bomsTreeGrp = bomsTree
                    .GroupBy(x=> new {x.Level,x.SetProductNumber,x.SetProductId,x.SetProductVersionId})  
                    .OrderBy(x => x.Key.Level)
                    .ToList();


        var bomsTreeSur = bomsTree
                   .GroupBy(x => new { x.Level, x.PartProductNumber, x.PartProductId, x.PartProductVersionId })
                   .OrderBy(x => x.Key.Level)
                   .ToList();

        // &&  !x.SetProductNumber.StartsWith("MIE")



        var listOfProductTkw = new List<ProductTkw>();

        foreach (var item in bomsTreeGrp)
        {
            var productTkw = new ProductTkw
            {
                Level = item.Key.Level,
                ProductNumber = item.Key.SetProductNumber,
                ProductId = (int)item.Key.SetProductId,
                ProductVersionId = item.Key.SetProductVersionId,
                AllowRecalculate = !item.Key.SetProductNumber.Contains("MIE")
            };

            listOfProductTkw.Add(productTkw);
        }

        foreach (var item in bomsTreeSur)
        {
            if (!listOfProductTkw.Where(x=>x.ProductId == item.Key.PartProductId).Any() )
            {
                var productTkw = new ProductTkw
                {
                    Level = 99,
                    ProductNumber = item.Key.PartProductNumber,
                    ProductId = item.Key.PartProductId,
                    ProductVersionId = item.Key.PartProductVersionId,
                    AllowRecalculate = false,
                };

                listOfProductTkw.Add(productTkw);
            }
            
        }

        listOfProductTkw = listOfProductTkw.OrderBy(x => x.Level).ThenBy(x=>x.ProductNumber).ToList();

        var defaultPeriod = _context.AccountingPeriods.SingleOrDefault(x => x.IsDefault);

        foreach (var item in listOfProductTkw)
        {
            var productCost = _context.ProductCosts
               .Include(x => x.Currency)
               .SingleOrDefault(x => x.ProductId == item.ProductId && x.AccountingPeriodId == defaultPeriod.Id);


            if (productCost != null)
            {
                item.TotalCost = CalcutateToPln(productCost.Cost, productCost.Currency, defaultPeriod.Id);
                item.LabourCost = productCost.LabourCost;
                item.MaterialCost = productCost.MaterialCost;
            }

        }

        listOfProductTkw = listOfProductTkw.OrderBy(x => x.Level).ToList();

        var ProductTkwViewModel = new ProductTkwViewModel
        {
            ProductId = request.ProductId,
            ProductNumber = _context.Products.SingleOrDefault(x=>x.Id == request.ProductId)?.Name??String.Empty,
            ListOfProductTkw = listOfProductTkw,
        };

        
        return ProductTkwViewModel;
    }

    private decimal CalcutateToPln(decimal cost, Currency currency, int periodId)
    {
        decimal costInPln = 0;
        int pln = 1;

        if (currency != null)
        {
            if (currency.Id == pln)
            {
                costInPln = cost;
            }
            else
            {
                var rate = _context.CurrencyRates
                    .SingleOrDefault(x => x.AccountingPeriodId == periodId && x.FromCurrencyId == currency.Id);


                if (rate != null)
                {
                    var x = Math.Round(cost * rate.Rate, 2);
                    costInPln = x;
                }

            }
        }
        return costInPln;
    }
}
