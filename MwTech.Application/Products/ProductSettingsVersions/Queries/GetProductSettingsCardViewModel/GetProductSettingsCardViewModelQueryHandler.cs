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

namespace MwTech.Application.ProductSettingsVersions.Queries.GetProductSettingsCardViewModel;

public class GetProductSettingsCardViewModelQueryHandler : IRequestHandler<GetProductSettingsCardViewModelQuery, ProductSettingsCardViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IProductService _productWeightService;

    public GetProductSettingsCardViewModelQueryHandler(IApplicationDbContext context,
            IProductService productWeightService)
    {
        _context = context;
        _productWeightService = productWeightService;
    }

    public async Task<ProductSettingsCardViewModel> Handle(GetProductSettingsCardViewModelQuery request, CancellationToken cancellationToken)
    {

        int? productSettingsVersionId = request.ProductSettingsVersionId;
        
        var productSettingsVersion = _context.ProductSettingVersions
            .Include(x => x.Accepted01ByUser)
            .Include(x => x.Accepted02ByUser)
            .Include(x => x.Accepted03ByUser)
            .Include(x => x.CreatedByUser)
            .Include(x => x.MachineCategory)
            .Include(x => x.Machine)
            .Include(x => x.WorkCenter)
            .FirstOrDefault(x=>x.Id == productSettingsVersionId);

        var product = _context.Products
                .Include(x => x.Unit)
                .Include(x => x.ProductCategory)
                .SingleOrDefault(x => x.Id == productSettingsVersion.ProductId);

        int? productId = product.Id;

        var bomDefaultVersion = _context.ProductVersions
                        .Include(x => x.Accepted01ByUser)
                        .Include(x => x.Accepted02ByUser)
                        .Include(x => x.CreatedByUser)
                        .SingleOrDefault(x => x.ProductId == productId && x.DefaultVersion);

        var routeDefaultVersion = _context.RouteVersions
                        .Include(x => x.Accepted01ByUser)
                        .Include(x => x.Accepted02ByUser)
                        .Include(x => x.CreatedByUser)
                        .SingleOrDefault(x => x.ProductId == productId && x.DefaultVersion);

        var propertiesDefaultVersion = _context.ProductPropertyVersions
                        .Include(x => x.Accepted01ByUser)
                        .Include(x => x.Accepted02ByUser)
                        .Include(x => x.CreatedByUser)
                        .SingleOrDefault(x => x.ProductId == productId && x.DefaultVersion);


        List<ProductSettingVersionPosition> settings = null;
        List<Bom> boms = null;
        List<ManufactoringRoute> routes = null;
        List<ProductProperty> productProperties = null;

        if (productSettingsVersion !=null)
        {
            settings = await _context.ProductSettingVersionPositions
                        .Include(x => x.Setting)
                        .Include(x => x.Setting.Unit)
                        .Include(x => x.Setting.SettingCategory)
                        .Where(x => x.ProductSettingVersionId == productSettingsVersionId
                         && !x.Setting.HideOnPrintout
                         && (x.Value != null || x.Text != null || x.MaxValue != null || x.MinValue != null ))
                        .OrderBy(x => x.Setting.SettingCategory.OrdinalNumber)
                        .ThenBy(x => x.Setting.OrdinalNumber)
                        .AsNoTracking()
                        .ToListAsync();
        }

        if (bomDefaultVersion != null)
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
                        .Where(x => x.SetVersionId == bomDefaultVersion.Id)
                        .OrderBy(x => x.OrdinalNumber)
                        .ThenBy(x => x.Part.ProductNumber)
                        .AsNoTracking()
                        .ToListAsync();


            _productWeightService.CalculateWeight(boms);
        }


        if (propertiesDefaultVersion != null)
        {
            productProperties = await _context.ProductProperties
               .Include(x => x.ProductPropertiesVersion)
               .ThenInclude(x => x.Product)
               .Include(x => x.Property)
               .ThenInclude(x => x.Unit)
               .Include(x => x.Property)
               .ThenInclude(x => x.ProductCategory)
               .Where(x => x.ProductPropertiesVersion.ProductId == productId && x.ProductPropertiesVersion.DefaultVersion)
               .OrderBy(x => x.Property.ProductCategory.OrdinalNumber)
               .ThenBy(x => x.Property.OrdinalNo)
               .ThenBy(x => x.Property.PropertyNumber)
               .AsNoTracking()
               .ToListAsync();
        }




        var vm = new ProductSettingsCardViewModel
        {
            Product = product,
            
            ProductSettingVersion = productSettingsVersion,
            ProductSettingsVersionPositions = settings,
            
            ProductVersion = bomDefaultVersion,
            Boms = boms,

            RouteVersion = routeDefaultVersion,
            Routes = routes,

            ProductPropertyVersion = propertiesDefaultVersion,
            ProductProperties = productProperties,
            
        };
        return vm;
    }
}
