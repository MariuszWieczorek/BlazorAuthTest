using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.Common;
using MwTech.Domain.Entities;
using System.Linq.Dynamic.Core;

namespace MwTech.Application.Products.Products.Queries.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductsViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IProductService _productWeightService;
    private readonly IProductCostService _productCostService;
    private readonly ILogger<GetProductsQueryHandler> _logger;

    public GetProductsQueryHandler(IApplicationDbContext context,
        IProductService productWeightService,
        IProductCostService productCostService,
        ILogger<GetProductsQueryHandler> logger
        )
    {
        _context = context;
        _productWeightService = productWeightService;
        _productCostService = productCostService;
        _logger = logger;
    }
    public async Task<ProductsViewModel> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {


        


        var  products =  _context.Products
                    .Include(x => x.ProductCategory)
                    .Include(x => x.Unit)
                    .AsNoTracking()
                    .AsQueryable();


        if (request.ProductFilter == null)
        {
            request.ProductFilter = new ProductFilter();
        }

        //       .Include(x => x.ProductVersions)
        //       .Include(x => x.BomSets)
        //       .Include(x => x.BomParts)


        var productsToCount = _context.Products
            .AsNoTracking()
            .AsQueryable();

        products = Filter(products, request.ProductFilter);

        productsToCount = Filter(productsToCount, request.ProductFilter);

        var productsCount = productsToCount.Count();
        
        products = products.OrderBy(x => x.ProductNumber);

        //var productList = await products.ToListAsync();
        //   .Select(x=>x.ToProductDto())
        //     .PaginatedListAsync(request.PageNumber, request.PageSize);


        // stronicowanie
        if (request.PagingInfo != null)
        {

            request.PagingInfo.TotalItems = products.Count();
            request.PagingInfo.ItemsPerPage = 100;

            if (request.PagingInfo.ItemsPerPage > 0 && request.PagingInfo.TotalItems > 0)
                products = products
                    .Skip((request.PagingInfo.CurrentPage - 1) * request.PagingInfo.ItemsPerPage)
                    .Take(request.PagingInfo.ItemsPerPage);
        }

        var productsList = await products.ToListAsync();

          productsList = await CalculateWeights(productsList);
          productsList = await _productCostService.GetProductsCost(productsList);
          productsList = await CalculateParts(productsList);
          productsList = await CalculateSets(productsList);
          productsList = await AddInfo(productsList);


        var vm = new ProductsViewModel
        {
            ProductFilter = request.ProductFilter,
            ProductCategories = await _context.ProductCategories.OrderBy(x=>x.OrdinalNumber).AsNoTracking().ToListAsync(),
            Products = productsList,
            PagingInfo = request.PagingInfo,
            CurrentPeriod = _context.AccountingPeriods.SingleOrDefault(x=>x.IsDefault),
            ProductsCount = productsCount
        };
            
        
        return vm;
    }

    private async Task<List<Product>> AddInfo(List<Product> productsList)
    {
        foreach (var item in productsList)
        {

            if (item.ProductCategory.CategoryNumber == "OKC")
            {


                string info = string.Empty;

                var x = _context.Boms
                    .Include(x => x.Set)
                    .Where(x => x.PartId == item.Id && x.Set.TechCardNumber != null)
                    .GroupBy(x => new { x.Set.TechCardNumber, x.Layer})
                    .ToList();
                    
                //.OrderBy(x => x.Key.TechCardNumber)
                    //.ToListAsync();

                foreach (var part in x)
                {
                    info = info.Trim() + $"{part.Key.TechCardNumber}({part.Key.Layer});";
                }

                item.info = info;
            }

            if (item.ProductCategory.CategoryNumber == "ODR")
            {


                string info = string.Empty;

                var x = _context.Boms
                    .Include(x => x.Set)
                    .Where(x => x.PartId == item.Id && x.Set.TechCardNumber != null)
                    .GroupBy(x => new { x.Set.TechCardNumber})
                    .ToList();

                //.OrderBy(x => x.Key.TechCardNumber)
                //.ToListAsync();

                foreach (var part in x)
                {
                    info = info.Trim() + $"{part.Key.TechCardNumber};";
                }

                item.info = info;
            }
        }

        return productsList;
    }

    private async Task<List<Product>> CalculateWeights(List<Product> products)
    {
        foreach (var item in products)
        {
            item.ProductWeight = await _productWeightService.CalculateWeight(item.Id,0);
        }

        return products;
    }

    private async Task<List<Product>> CalculateParts(List<Product> products)
    {
        foreach (var item in products)
        {
            item.PartsCounter =  await _context.Boms
                .Where(x => x.PartId == item.Id)
                .CountAsync();
        }

        return products;
    }


    private async Task<List<Product>> CalculateSets(List<Product> products)
    {
        foreach (var item in products)
        {
            item.SetsCounter = await _context.Boms
                .Include(x=>x.SetVersion)
                .Where(x => x.SetId == item.Id && x.SetVersion.DefaultVersion)
                .CountAsync();
        }

        return products;
    }

    private IQueryable<Product> Filter(IQueryable<Product> products, ProductFilter productFilter)
    {
        if (productFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(productFilter.Name))
                products = products.Where(x => x.Name.Contains(productFilter.Name.Trim()));

            /*
            if (!string.IsNullOrWhiteSpace(productFilter.ProductNumber))
                products = products.Where(x => x.ProductNumber.Contains(productFilter.ProductNumber.Trim()));
            */

            if (!string.IsNullOrWhiteSpace(productFilter.ProductNumber))
                products = products.Where(x => EF.Functions.Like(x.ProductNumber, productFilter.ProductNumber));

            /*
            if (!string.IsNullOrWhiteSpace(productFilter.ProductNumber))
                if (productFilter.ProductNumber.Contains("%") || productFilter.ProductNumber.Contains("_"))
                {
                    products = products.Where(x => EF.Functions.Like(x.ProductNumber, productFilter.ProductNumber));
                }
                else
                {
                    products = products.Where(x => x.ProductNumber.Contains(productFilter.ProductNumber.Trim()));
                }
            */

            if (!string.IsNullOrWhiteSpace(productFilter.OldProductNumber))
                products = products.Where(x => x.OldProductNumber.Contains(productFilter.OldProductNumber.Trim()));

            if (!string.IsNullOrWhiteSpace(productFilter.Idx01))
                products = products.Where(x => x.Idx01.Contains(productFilter.Idx01.Trim()));

            if (!string.IsNullOrWhiteSpace(productFilter.Idx02))
                products = products.Where(x => x.Idx02.Contains(productFilter.Idx02.Trim()));

            if (productFilter.ProductCategoryId != 0)
                products = products.Where(x => x.ProductCategoryId == productFilter.ProductCategoryId);

            if (productFilter.Id != 0)
                products = products.Where(x => x.Id == productFilter.Id);

            if (productFilter.TechCardNumber != 0 && productFilter.TechCardNumber != null)
                products = products.Where(x => x.TechCardNumber == productFilter.TechCardNumber);

            if (productFilter.IsActive == 1)
                products = products.Where(x => x.IsActive == true);
            
            if (productFilter.IsActive == 0)
                products = products.Where(x => x.IsActive == false);

            if (productFilter.IsTest == 1)
                products = products.Where(x => x.IsTest == true);

            if (productFilter.IsTest == 0)
                products = products.Where(x => x.IsTest == false);



            if (!string.IsNullOrWhiteSpace(productFilter.ComponentProductNumber))
            {
                //products = products.Where(x => x.BomSets.Where(x => x.Part.ProductNumber == productFilter.ComponentProductNumber));
                
                
                var component = _context.Products.FirstOrDefault(x => x.ProductNumber.Contains(productFilter.ComponentProductNumber.Trim()));
                if (component != null)
                {
                    int componentId = component.Id;
                    products = products.Where(x => x.BomSets
                    .Where(x => x.PartId == componentId
                                  && x.SetVersion.IsActive
                    ).Any());
                }
                else
                {
                    products = products.Where(x => false);
                }
            }

     
        }

        return products;
    }

}
