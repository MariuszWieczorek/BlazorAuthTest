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

namespace MwTech.Application.Products.Products.Queries.GetProductTechCardViewModel;

public class GetProductTechCardViewModelQueryHandler : IRequestHandler<GetProductTechCardViewModelQuery, ProductTechCardViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IProductService _productWeightService;

    public GetProductTechCardViewModelQueryHandler(IApplicationDbContext context,
            IProductService productWeightService)
    {
        _context = context;
        _productWeightService = productWeightService;
    }

    public async Task<ProductTechCardViewModel> Handle(GetProductTechCardViewModelQuery request, CancellationToken cancellationToken)
    {

        int? productId = request.ProductId;
        string productNumber = request.ProductNumber;
        Product idx01 = null;
        Product idx02 = null;


        if (productNumber != null)
        {
            productId = _context.Products.FirstOrDefault(x => x.ProductNumber == productNumber)?.Id??0;
        }
        
        var product = _context.Products
                .Include(x => x.Unit)
                .Include(x => x.ProductCategory)
                .SingleOrDefault(x => x.Id == productId);
        

        if (product != null)
        {
            if (product.Idx01 != null && !string.IsNullOrEmpty(product.Idx01) )
            {
                idx01 = _context.Products.FirstOrDefault(x => x.ProductNumber == product.Idx01);
            }

            if (product.Idx02 != null && !string.IsNullOrEmpty(product.Idx02))
            {
                idx02 = _context.Products.FirstOrDefault(x => x.ProductNumber == product.Idx02);
            }
        }



        

        var productVersions = await _context.ProductVersions
            .Include(x => x.Accepted01ByUser)
            .Include(x => x.Accepted02ByUser)
            .Include(x => x.CreatedByUser)
            .Where(x => x.ProductId == productId && x.DefaultVersion)
            .OrderByDescending(x => x.VersionNumber)
            .AsNoTracking()
            .ToListAsync();

        var productDefaultVersion = _context.ProductVersions
            .SingleOrDefault(x => x.ProductId == productId && x.DefaultVersion);

        List<Bom> boms = null;

        if (productDefaultVersion != null)
        {
            boms = await _context.Boms
                        .Include(x => x.Set)
                        .ThenInclude(x => x.Unit)
                        .Include(x => x.Set)
                        .ThenInclude(x => x.ProductCategory)
                        .Include(x => x.Part)
                        .ThenInclude(x => x.Unit)
                        .Include(x => x.Part)
                        .ThenInclude(x => x.ProductCategory)
                        .Where(x => x.SetVersionId == productDefaultVersion.Id)
                        .OrderBy(x => x.OrdinalNumber)
                        .ThenBy(x => x.Part.ProductNumber)
                        .AsNoTracking()
                        .ToListAsync();


            _productWeightService.CalculateWeight(boms);
        }

        var productSettingsVersions = await _context.ProductSettingVersions
            .Include(x => x.Accepted01ByUser)
            .Include(x => x.Accepted02ByUser)
            .Include(x => x.Accepted03ByUser)
            .Include(x => x.CreatedByUser)
            .Where(x => x.ProductId == productId)
            .OrderByDescending(x => x.ProductSettingVersionNumber)
            .AsNoTracking()
            .ToListAsync();

        var routeVersions = await _context.RouteVersions
            .Include(x => x.Accepted01ByUser)
            .Include(x => x.Accepted02ByUser)
            .Include(x => x.CreatedByUser)
            .Where(x => x.ProductId == productId)
            .OrderBy(x => x.Name)
            .AsNoTracking()
            .ToListAsync();

        var productProperties = await _context.ProductProperties
            .Include(x=>x.ProductPropertiesVersion)
            .ThenInclude(x => x.Product)
            .Include(x => x.Property)
            .ThenInclude(x => x.Unit)
            .Include(x => x.Property)
            .ThenInclude(x => x.ProductCategory)
            .Where(x => (x.ProductPropertiesVersion.Product.ProductNumber == product.Idx01 
            || x.ProductPropertiesVersion.Product.ProductNumber == product.Idx02
            || x.ProductPropertiesVersion.ProductId == productId) && x.ProductPropertiesVersion.DefaultVersion && !x.Property.HideOnReport)
            .OrderBy(x => x.Property.ProductCategory.OrdinalNumber)
            .ThenBy(x=>x.Property.OrdinalNo)
            .ThenBy(x=>x.Property.PropertyNumber)
            .AsNoTracking()
            .ToListAsync();


        var vm = new ProductTechCardViewModel
        {
            Product = product,
            ProductVersions = productVersions,
            RouteVersions = routeVersions,
            ProductProperties = productProperties,
            ProductSettingVersions = productSettingsVersions,
            Boms = boms,
            Idx01 = idx01,
            Idx02 = idx02
        };
        return vm;
    }
}
