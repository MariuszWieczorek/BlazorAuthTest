using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Extensions;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Common.Models;
using MwTech.Application.Products.Products.Extensions;
using MwTech.Domain.Entities;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Products.Queries.GetProductsDataTable;

public class GetProductsDataTableQueryHandler : IRequestHandler<GetProductsDataTableQuery, GetProductsDataTableViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetProductsDataTableQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<GetProductsDataTableViewModel> Handle(GetProductsDataTableQuery request, CancellationToken cancellationToken)
    {
        
        var  products =  _context.Products
                    .Include(x => x.ProductCategory)
                    .Include(x => x.Unit)
                    .AsNoTracking()
                    .AsQueryable();




        products = Filter(products, request.ProductFilter);

        
        products = !string.IsNullOrWhiteSpace(request.OrderInfo) ?
            products = products.OrderBy(request.OrderInfo) :
            products = products.OrderByDescending(x => x.ProductNumber);

        var productList = await products
             .Select(x=>x.ToProductDataTableDto())
             .PaginatedListAsync(request.PageNumber, request.PageSize);


        

        var vm = new GetProductsDataTableViewModel
        {
            ProductFilter = request.ProductFilter,
            ProductCategories = _context.ProductCategories.ToList(),
            Products = productList
         };
            
        
        return vm;
    }

    private IQueryable<Product> Filter(IQueryable<Product> products, ProductDataTableFilter productFilter)
    {
        if (productFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(productFilter.Name))
                products = products.Where(x => x.Name.Contains(productFilter.Name));

            if (!string.IsNullOrWhiteSpace(productFilter.ProductNumber))
                products = products.Where(x => x.ProductNumber.Contains(productFilter.ProductNumber));

            if (productFilter.ProductCategoryId != 0)
                products = products.Where(x => x.ProductCategoryId == productFilter.ProductCategoryId);

            if (productFilter.Id != 0)
                products = products.Where(x => x.Id == productFilter.Id);

            if (productFilter.TechCardNumber != 0 && productFilter.TechCardNumber != null)
                products = products.Where(x => x.TechCardNumber == productFilter.TechCardNumber);

            if (productFilter.IsActive == true)
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
