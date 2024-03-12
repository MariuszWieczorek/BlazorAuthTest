using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System.Linq.Dynamic.Core;

namespace MwTech.Application.GetProductsPropertiesVersions.Queries.GetProductsPropertiesVersions;

public class GetProductsPropertiesVersionsQueryHandler : IRequestHandler<GetProductsPropertiesVersionsQuery, ProductsPropertiesVersionsViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetProductsPropertiesVersionsQueryHandler> _logger;

    public GetProductsPropertiesVersionsQueryHandler(IApplicationDbContext context,
        ILogger<GetProductsPropertiesVersionsQueryHandler> logger
        )
    {
        _context = context;
        _logger = logger;
    }
    public async Task<ProductsPropertiesVersionsViewModel> Handle(GetProductsPropertiesVersionsQuery request, CancellationToken cancellationToken)
    {

        var productsPropertiesVersions = _context.ProductPropertyVersions
            .Include(x => x.Product)
            .Include(x => x.Accepted01ByUser)
            .Include(x => x.Accepted02ByUser)
            .Include(x => x.CreatedByUser)
            .OrderByDescending(x => x.VersionNumber)
            .AsNoTracking()
            .AsQueryable();

        productsPropertiesVersions = Filter(productsPropertiesVersions, request.ProductPropertyVersionFilter);

        productsPropertiesVersions = productsPropertiesVersions
                .OrderBy(x => x.Product.ProductNumber)
                .ThenBy(x=>x.AlternativeNo)
                .ThenByDescending(x=>x.VersionNumber);


        // stronicowanie
        if (request.PagingInfo != null)
        {

            request.PagingInfo.TotalItems = productsPropertiesVersions.Count();
            request.PagingInfo.ItemsPerPage = 100;

            if (request.PagingInfo.ItemsPerPage > 0 && request.PagingInfo.TotalItems > 0)
                productsPropertiesVersions = productsPropertiesVersions
                    .Skip((request.PagingInfo.CurrentPage - 1) * request.PagingInfo.ItemsPerPage)
                    .Take(request.PagingInfo.ItemsPerPage);
        }

        var productsPropertiesVersionsList = await productsPropertiesVersions.ToListAsync();  

        var vm = new ProductsPropertiesVersionsViewModel
        {
            ProductPropertyVersionFilter = request.ProductPropertyVersionFilter,
            ProductsPropertiesVersions = productsPropertiesVersionsList,
            ProductCategories = await _context.ProductCategories.OrderBy(x=>x.OrdinalNumber).ToListAsync(),
            PagingInfo = request.PagingInfo,
        };
            
        
        return vm;
    }

    
    private IQueryable<ProductPropertyVersion> Filter(IQueryable<ProductPropertyVersion> productsPropertyVersions, ProductPropertyVersionFilter productPropertyVersionFilter)
    {
        if (productPropertyVersionFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(productPropertyVersionFilter.ProductNumber))
                productsPropertyVersions = productsPropertyVersions.Where(x => x.Product.ProductNumber.Contains(productPropertyVersionFilter.ProductNumber.Trim()));

            if (!string.IsNullOrWhiteSpace(productPropertyVersionFilter.Name))
                productsPropertyVersions = productsPropertyVersions.Where(x => x.Name.Contains(productPropertyVersionFilter.Name.Trim()));

            if (productPropertyVersionFilter.ProductCategoryId != 0)
                productsPropertyVersions = productsPropertyVersions.Where(x => x.Product.ProductCategoryId == productPropertyVersionFilter.ProductCategoryId);

            if (productPropertyVersionFilter.IsActive == true)
                productsPropertyVersions = productsPropertyVersions.Where(x => x.IsActive == true);

            if (productPropertyVersionFilter.NoFirstAcceptation == true)
                productsPropertyVersions = productsPropertyVersions.Where(x => x.IsAccepted01 == false);

            if (productPropertyVersionFilter.NoSecondAcceptation == true)
                productsPropertyVersions = productsPropertyVersions.Where(x => x.IsAccepted02 == false);

            if (productPropertyVersionFilter.TechCardNumber != 0 && productPropertyVersionFilter.TechCardNumber != null)
                productsPropertyVersions = productsPropertyVersions.Where(x => x.Product.TechCardNumber == productPropertyVersionFilter.TechCardNumber);
        }   

        return productsPropertyVersions;
    }
}
