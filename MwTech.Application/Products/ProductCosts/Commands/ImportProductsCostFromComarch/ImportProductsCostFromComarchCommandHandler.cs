using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductCosts.Commands.ImportProductsCostFromComarch;

public class ImportProductsCostFromComarchCommandHandler : IRequestHandler<ImportProductsCostFromComarchCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IComarchService _comarchService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;
    private readonly IProductCostService _productCostService;

    public ImportProductsCostFromComarchCommandHandler(IApplicationDbContext context,
        IComarchService comarchService,
        ICurrentUserService currentUserService,
        IDateTimeService dateTimeService,
        IProductCostService productCostService
        )
    {
        _context = context;
        _comarchService = comarchService;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
        _productCostService = productCostService;
    }
    public async Task Handle(ImportProductsCostFromComarchCommand request, CancellationToken cancellationToken)
    {


        var comarchProductsCost = await _comarchService.GetTwrCost();
        var currentUserId = _currentUserService.UserId;

        if (comarchProductsCost.Count() == 0)
            throw new Exception("Brak kosztów");

        var period = new AccountingPeriod();

        try
        {
            period = _context.AccountingPeriods
            .SingleOrDefault(x => x.IsDefault);
        }
        catch (Exception)
        {

            throw new Exception("Więcej niż jeden domyślny okres, lub brak domyślnych okresów");
        }
        



        var pln = _context.Currencies
                .SingleOrDefault(x => x.CurrencyCode == "PLN");

       
        // Lista Produktów do wyceny
        var productsToPricing = await _context.Products
            .Include(x=>x.ProductCategory)
            .Where(x =>
               x.ProductCategory.CategoryNumber == "SUR"
            || x.ProductCategory.CategoryNumber == "PREP"
            || x.ProductCategory.CategoryNumber == "OTT"
            || x.ProductCategory.CategoryNumber == "OKS"
            || x.ProductCategory.CategoryNumber == "OME"
            || x.ProductCategory.CategoryNumber == "DRU")
            .ToListAsync();
                

        foreach (var item in productsToPricing)
        {
            var price = comarchProductsCost.FirstOrDefault(x => x.ProductNumber == item.ProductNumber);
            if (price != null)
            {
                var productCost = _context.ProductCosts
                            .SingleOrDefault(x => x.ProductId == item.Id && x.AccountingPeriodId == period.Id);

                if (productCost == null)
                {
                    var newProductCost = new ProductCost()
                    {
                        ProductId = item.Id,
                        AccountingPeriodId = period.Id,
                        CurrencyId = pln?.Id,
                        Cost = price.Cost,
                        IsImported = true,
                        ImportedDate = DateTime.Now,
                        Description = "pobrany z ComarchXL",
                        CreatedByUserId = currentUserId,
                        CreatedDate = _dateTimeService.Now

                    };

                    _context.ProductCosts.Add(newProductCost);
                }
                else
                {

                    productCost.CurrencyId = pln?.Id;
                    productCost.Cost = price.Cost;
                    productCost.IsImported = true;
                    productCost.ImportedDate = DateTime.Now;
                    productCost.Description = "pobrany z ComarchXL";
                    productCost.ModifiedByUserId = currentUserId;
                    productCost.ModifiedDate = _dateTimeService.Now;

                    _context.ProductCosts.Update(productCost);
                    
                }

                await _context.SaveChangesAsync();
            }


        }
        return;
    }
}
