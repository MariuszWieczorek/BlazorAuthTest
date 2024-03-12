using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System.Linq.Dynamic.Core;

namespace MwTech.Application.ProductSettingsVersions.Queries.GetProductsSettingsVersions;

public class GetProductsSettingsVersionsQueryHandler : IRequestHandler<GetProductsSettingsVersionsQuery, ProductsSettingVersionsViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetProductsSettingsVersionsQueryHandler> _logger;

    public GetProductsSettingsVersionsQueryHandler(IApplicationDbContext context,
        ILogger<GetProductsSettingsVersionsQueryHandler> logger
        )
    {
        _context = context;
        _logger = logger;
    }
    public async Task<ProductsSettingVersionsViewModel> Handle(GetProductsSettingsVersionsQuery request, CancellationToken cancellationToken)
    {
        /*

        var  productsSettingVersions =  _context.ProductSettingVersions
                    .Include(x => x.Product)
                    .AsNoTracking()
                    .AsQueryable();
        */

        var productsSettingsVersions = _context.ProductSettingVersions
            .Include(x => x.Product)
            .Include(x => x.Accepted01ByUser)
            .Include(x => x.Accepted02ByUser)
            .Include(x => x.Accepted03ByUser)
            .Include(x => x.CreatedByUser)
            .Include(x => x.Machine)
            .Include(x => x.MachineCategory)
            .Include(x => x.WorkCenter)
            .OrderByDescending(x => x.ProductSettingVersionNumber)
            .AsNoTracking()
            .AsQueryable();

        productsSettingsVersions = Filter(productsSettingsVersions, request.ProductSettingVersionFilter);

        productsSettingsVersions = productsSettingsVersions
                .OrderBy(x => x.Product.ProductNumber)
                .ThenBy(x=>x.AlternativeNo)
                .ThenByDescending(x=>x.ProductSettingVersionNumber);


        
        //var productsSettingsVersionsList = new List<ProductSettingVersion>();

        // stronicowanie
        if (request.PagingInfo != null)
        {

            request.PagingInfo.TotalItems = productsSettingsVersions.Count();
            request.PagingInfo.ItemsPerPage = 100;

            if (request.PagingInfo.ItemsPerPage > 0 && request.PagingInfo.TotalItems > 0)
                productsSettingsVersions = productsSettingsVersions
                    .Skip((request.PagingInfo.CurrentPage - 1) * request.PagingInfo.ItemsPerPage)
                    .Take(request.PagingInfo.ItemsPerPage);
        }

        var productsSettingsVersionsList = await productsSettingsVersions.ToListAsync();  

        var vm = new ProductsSettingVersionsViewModel
        {
            ProductSettingVersionFilter = request.ProductSettingVersionFilter,
            ProductsSettingsVersions = productsSettingsVersionsList,
            ProductCategories = await _context.ProductCategories.OrderBy(x=>x.OrdinalNumber).ToListAsync(),
            MachineCategories = await _context.MachineCategories.OrderBy(x=>x.Name).ToListAsync(),
            PagingInfo = request.PagingInfo,
        };
            
        
        return vm;
    }

    
    private IQueryable<ProductSettingVersion> Filter(IQueryable<ProductSettingVersion> productsSettingVersions, ProductSettingVersionFilter productSettingVersionFilter)
    {
        if (productSettingVersionFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(productSettingVersionFilter.ProductNumber))
                productsSettingVersions = productsSettingVersions.Where(x => x.Product.ProductNumber.Contains(productSettingVersionFilter.ProductNumber.Trim()));

            if (!string.IsNullOrWhiteSpace(productSettingVersionFilter.MachineNumber))
                productsSettingVersions = productsSettingVersions.Where(x => x.Machine.MachineNumber.Contains(productSettingVersionFilter.MachineNumber.Trim()));
            
            if (!string.IsNullOrWhiteSpace(productSettingVersionFilter.Name))
                productsSettingVersions = productsSettingVersions.Where(x => x.Name.Contains(productSettingVersionFilter.Name.Trim()));

            if (productSettingVersionFilter.MachineCategoryId != 0)
                productsSettingVersions = productsSettingVersions.Where(x => x.MachineCategoryId == productSettingVersionFilter.MachineCategoryId);

            if (!string.IsNullOrWhiteSpace(productSettingVersionFilter.WorkCenterNumber))
                productsSettingVersions = productsSettingVersions.Where(x => x.WorkCenter.ResourceNumber.Contains(productSettingVersionFilter.WorkCenterNumber.Trim()));

            if (productSettingVersionFilter.ProductCategoryId != 0)
                productsSettingVersions = productsSettingVersions.Where(x => x.Product.ProductCategoryId == productSettingVersionFilter.ProductCategoryId);

            if (productSettingVersionFilter.IsActive == true)
                productsSettingVersions = productsSettingVersions.Where(x => x.IsActive == true);

            if (productSettingVersionFilter.NoFirstAcceptation == true)
                productsSettingVersions = productsSettingVersions.Where(x => x.IsAccepted01 == false);

            if (productSettingVersionFilter.NoSecondAcceptation == true)
                productsSettingVersions = productsSettingVersions.Where(x => x.IsAccepted02 == false);

            if (productSettingVersionFilter.NoThirdAcceptation == true)
                productsSettingVersions = productsSettingVersions.Where(x => x.IsAccepted03 == false);

            if (productSettingVersionFilter.TechCardNumber != 0 && productSettingVersionFilter.TechCardNumber != null)
                productsSettingVersions = productsSettingVersions.Where(x => x.Product.TechCardNumber == productSettingVersionFilter.TechCardNumber);
        }   

        return productsSettingVersions;
    }
}
