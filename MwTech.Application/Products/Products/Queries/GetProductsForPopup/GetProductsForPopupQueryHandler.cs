using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.Common;
using MwTech.Domain.Entities;
using System.Linq.Dynamic.Core;

namespace MwTech.Application.Products.Products.Queries.GetProductsForPopup;

public class GetProductsForPopupQueryHandler : IRequestHandler<GetProductsForPopupQuery, ProductsForPopupViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IProductService _productWeightService;
    private readonly IProductCostService _productCostService;
    private readonly ILogger<GetProductsForPopupQueryHandler> _logger;

    public GetProductsForPopupQueryHandler(IApplicationDbContext context,
        IProductService productWeightService,
        IProductCostService productCostService,
        ILogger<GetProductsForPopupQueryHandler> logger
        )
    {
        _context = context;
        _productWeightService = productWeightService;
        _productCostService = productCostService;
        _logger = logger;
    }
    public async Task<ProductsForPopupViewModel> Handle(GetProductsForPopupQuery request, CancellationToken cancellationToken)
    {

   
        var products = _context.Products
                    .Include(x => x.ProductCategory)
                    .Include(x => x.Unit)
                    .AsNoTracking()
                    .AsQueryable();

        var productsToCount = _context.Products
            .AsNoTracking()
            .AsQueryable();

   

        products = Filter(products, request.ProductFilter);
        products = products.OrderBy(x => x.ProductNumber);
         
   
        var productsList = await products.Take(500).ToListAsync();

       
        var vm = new ProductsForPopupViewModel
        {
            ProductFilter = request.ProductFilter,
            ProductCategories = _context.ProductCategories.OrderBy(x=>x.OrdinalNumber).ToList(),
            Products = productsList
        };
            
        
        return vm;
    }


    private IQueryable<Product> Filter(IQueryable<Product> products, ProductFilter productFilter)
    {
        if (productFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(productFilter.Name))
                products = products.Where(x => x.Name.Contains(productFilter.Name));

            if (!string.IsNullOrWhiteSpace(productFilter.ProductNumber))
                products = products.Where(x => x.ProductNumber.Contains(productFilter.ProductNumber.Trim()));

            if (!string.IsNullOrWhiteSpace(productFilter.OldProductNumber))
                products = products.Where(x => x.OldProductNumber.Contains(productFilter.OldProductNumber));

            if (!string.IsNullOrWhiteSpace(productFilter.Idx01))
                products = products.Where(x => x.Idx01.Contains(productFilter.Idx01));

            if (!string.IsNullOrWhiteSpace(productFilter.Idx02))
                products = products.Where(x => x.Idx02.Contains(productFilter.Idx02));

            if (productFilter.ProductCategoryId != 0)
                products = products.Where(x => x.ProductCategoryId == productFilter.ProductCategoryId);

            if (productFilter.Id != 0)
                products = products.Where(x => x.Id == productFilter.Id);

            if (productFilter.TechCardNumber != 0 && productFilter.TechCardNumber != null)
                products = products.Where(x => x.TechCardNumber == productFilter.TechCardNumber);

            if (productFilter.IsActive == 1)
                products = products.Where(x => x.IsActive == true);

            if (!string.IsNullOrWhiteSpace(productFilter.ComponentProductNumber))
            {
                var component = _context.Products.FirstOrDefault(x => x.ProductNumber.Contains(productFilter.ComponentProductNumber));
                if (component != null)
                {
                    int componentId = component.Id;
                    products = products.Where(x => x.BomSets.Where(x => x.PartId == componentId).Any());
                }

            }
        }

        return products;
    }

}
