using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductCategories.Queries.GetProductCategories;

public class GetProductCategoriesQueryHandler : IRequestHandler<GetProductCategoriesQuery, ProductCategoriesViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetProductCategoriesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ProductCategoriesViewModel> Handle(GetProductCategoriesQuery request, CancellationToken cancellationToken)
    {
        var productCategories = _context.ProductCategories
            .Include(x=>x.Products)
            .OrderBy(x=>x.CategoryNumber)
            .AsNoTracking()
            .AsQueryable();

        productCategories = Filter(productCategories, request.ProductCategoryFilter);

        // stronicowanie
        if (request.PagingInfo != null)
        {

            request.PagingInfo.TotalItems = productCategories.Count();
            request.PagingInfo.ItemsPerPage = 100;

            if (request.PagingInfo.ItemsPerPage > 0 && request.PagingInfo.TotalItems > 0)
                productCategories = productCategories
                    .Skip((request.PagingInfo.CurrentPage - 1) * request.PagingInfo.ItemsPerPage)
                    .Take(request.PagingInfo.ItemsPerPage);
        }

        var productCategoriesList = await productCategories
            .OrderBy(x=>x.OrdinalNumber)
            .ToListAsync();

        var vm = new ProductCategoriesViewModel
            { ProductCategories = productCategoriesList,
              ProductCategoryFilter = request.ProductCategoryFilter,
              PagingInfo = request.PagingInfo
            };

        return vm;
           
    }

    public IQueryable<ProductCategory> Filter(IQueryable<ProductCategory> productCategories, ProductCategoryFilter productCategoryFilter)
    {
        if (productCategoryFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(productCategoryFilter.Name))
                productCategories = productCategories.Where(x => x.Name.Contains(productCategoryFilter.Name));

            if (!string.IsNullOrWhiteSpace(productCategoryFilter.ProductCategoryNumber))
                productCategories = productCategories.Where(x => x.CategoryNumber.Contains(productCategoryFilter.ProductCategoryNumber));
        }
        
        return productCategories;
    }
}
