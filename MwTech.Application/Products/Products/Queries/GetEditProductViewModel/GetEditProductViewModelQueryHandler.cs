using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.Products.Commands.EditProduct;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.Products.Queries.GetEditProductViewModel;

public class GetEditProductViewModelQueryHandler : IRequestHandler<GetEditProductViewModelQuery, EditProductViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IProductCostService _productCostService;

    public GetEditProductViewModelQueryHandler(IApplicationDbContext context, IProductCostService productCostService)
    {
        _context = context;
        _productCostService = productCostService;
    }

    public async Task<EditProductViewModel> Handle(GetEditProductViewModelQuery request, CancellationToken cancellationToken)
    {

        var product = _context.Products
            .Include(x => x.ProductCategory)
            .SingleOrDefault(x => x.Id == request.ProductId);

       

        var productCategories = await _context.ProductCategories
            .OrderBy(x => x.OrdinalNumber)
            .AsNoTracking()
            .ToListAsync();

        var units = await _context.Units
            .OrderBy(x => x.UnitCode)
            .AsNoTracking()
            .ToListAsync();
        
        var productVersions = await GetProductVersions(request);
        var productSettingsVersions = await GetProductSettingsVersions(request);
        var routeVersions = await GetRouteVersions(request, product);
        var productProportyVersions = await GetPropertyVersions(request);
        var productCosts = GetProductCosts(request);

        var editProductCommand = new EditProductCommand
        {
            Id = product.Id,
            ProductNumber = product.ProductNumber,
            OldProductNumber = product.OldProductNumber,
            TechCardNumber = product.TechCardNumber,
            Name = product.Name,
            Description = product.Description,
            ProductCategoryId = product.ProductCategoryId,
            UnitId = product.UnitId,
            ReturnedFromProd = product.ReturnedFromProd,
            NoCalculateTkw = product.NoCalculateTkw,
            IsActive = product.IsActive,
            IsTest = product.IsTest,
            MwbaseMatid = product.MwbaseMatid,
            MwbaseWyrobId = product.MwbaseWyrobId,
            Idx01 = product.Idx01,
            Idx02 = product.Idx02,
            ContentsOfRubber = product.ContentsOfRubber,
            Density = product.Density,
            ScalesId = product.ScalesId,
            Aps01 = product.Aps01,
            Aps02 = product.Aps02,
            DecimalPlaces = product.DecimalPlaces,
            WeightTolerance = product.WeightTolerance,
        };


        var vm = new EditProductViewModel
        {
            Product = product,
            EditProductCommand = editProductCommand,
            ProductCategories = productCategories,
            Units = units,
            ProductVersions = productVersions,
            RouteVersions = routeVersions,
            ProductPropertyVersions = productProportyVersions,
            ProductSettingVersions = productSettingsVersions,
            ProductCosts = productCosts,
        };

        if (product.Idx01 != null)
        {
            vm.Idx01 = _context.Products.FirstOrDefault(x => x.ProductNumber == product.Idx01);
        }

        if (product.Idx02 != null)
        {
            vm.Idx02 = _context.Products.FirstOrDefault(x => x.ProductNumber == product.Idx02);
        }

        return vm;
    }

    private async Task<List<ProductVersion>> GetProductVersions(GetEditProductViewModelQuery request)
    {
        return await _context.ProductVersions
                    .Include(x => x.Accepted01ByUser)
                    .Include(x => x.Accepted02ByUser)
                    .Include(x => x.CreatedByUser)
                    .Where(x => x.ProductId == request.ProductId)
                    .OrderByDescending(x => x.VersionNumber)
                    .AsNoTracking()
                    .ToListAsync();
    }

    private async Task<List<ProductSettingVersion>> GetProductSettingsVersions(GetEditProductViewModelQuery request)
    {
        return await _context.ProductSettingVersions
                    .Include(x => x.Accepted01ByUser)
                    .Include(x => x.Accepted02ByUser)
                    .Include(x => x.Accepted03ByUser)
                    .Include(x => x.CreatedByUser)
                    .Include(x => x.Machine)
                    .Include(x => x.MachineCategory)
                    .Include(x => x.WorkCenter)
                    .Where(x => x.ProductId == request.ProductId)
                    .OrderBy(x => x.AlternativeNo)
                    .ThenByDescending(x => x.ProductSettingVersionNumber)
                    .AsNoTracking()
                    .ToListAsync();
    }

    private async Task<List<ProductPropertyVersion>> GetPropertyVersions(GetEditProductViewModelQuery request)
    {
        return await _context.ProductPropertyVersions
                    .Include(x => x.Accepted01ByUser)
                    .Include(x => x.Accepted02ByUser)
                    .Include(x => x.CreatedByUser)
                    .Where(x => x.ProductId == request.ProductId)
                    .OrderBy(x => x.Name)
                    .AsNoTracking()
                    .ToListAsync();
    }

    private async Task<List<RouteVersion>> GetRouteVersions(GetEditProductViewModelQuery request, Product product)
    {
        int productId = request.ProductId;
        var routeVersions = new List<RouteVersion>();
        
        if (product.ProductCategory.RouteSource == 0)
        {
            routeVersions = await _context.RouteVersions
                    .Include(x => x.Accepted01ByUser)
                    .Include(x => x.Accepted02ByUser)
                    .Include(x => x.CreatedByUser)
                    .Include(x => x.ProductCategory)
                    .Where(x => x.ProductId == request.ProductId)
                    .OrderBy(x => x.Name)
                    .AsNoTracking()
                    .ToListAsync();
        }

        /*
        if (product.ProductCategory.RouteSource == 1)
        {
            var idx01 = _context.Products.SingleOrDefault(x => x.ProductNumber == product.Idx01)?.Id;
            if (idx01 != null)
            {
                routeVersions = await _context.RouteVersions
                   .Include(x => x.Accepted01ByUser)
                   .Include(x => x.Accepted02ByUser)
                   .Include(x => x.CreatedByUser)
                   .Include(x => x.ProductCategory)
                   .Where(x => x.ProductId == idx01 && x.ProductCategoryId == product.ProductCategoryId)
                   .OrderBy(x => x.Name)
                   .AsNoTracking()
                   .ToListAsync();
            }
        }
        */

        return routeVersions;
    }

    private List<ProductCost> GetProductCosts(GetEditProductViewModelQuery request)
    {
        var productCosts = _context.ProductCosts
                    .Include(x => x.Product)
                    .Include(x => x.AccountingPeriod)
                    .Include(x => x.Currency)
                    .Include(x => x.CreatedByUser)
                    .Include(x => x.ModifiedByUser)
                    .Where(x => x.ProductId == request.ProductId)
                    .OrderBy(x => x.AccountingPeriodId)
                    .AsNoTracking()
                    .ToList();

        foreach (var item in productCosts)
        {
            if (item.Cost != null)
                item.CostInPln = _productCostService.CalcutateToPln((decimal)item.Cost, item.Currency, item.AccountingPeriodId);
        }


        var periods = _context.AccountingPeriods.ToList();

        foreach (var item in periods)
        {
            if (!productCosts.Where(x=>x.AccountingPeriodId == item.Id).Any() )
            {
                var newProductCost = new ProductCost
                {
                    AccountingPeriodId = item.Id,
                    AccountingPeriod = item,
                    ProductId = request.ProductId,
                    Product = _context.Products.SingleOrDefault(x=>x.Id==request.ProductId),
                    CurrencyId = 1,
                    Currency = _context.Currencies.SingleOrDefault(x=>x.Id==1),

                };

                productCosts.Add(newProductCost);
            }
        }

        return productCosts;
    }

    
}
