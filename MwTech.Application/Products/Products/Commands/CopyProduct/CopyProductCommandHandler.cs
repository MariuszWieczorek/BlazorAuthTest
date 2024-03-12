using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.Products.Commands.CopyProduct;

public class CopyProductCommandHandler : IRequestHandler<CopyProductCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;

    public CopyProductCommandHandler(IApplicationDbContext context, IDateTimeService dateTimeService, ICurrentUserService currentUser)
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _currentUser = currentUser;
    }

    public async Task Handle(CopyProductCommand request, CancellationToken cancellationToken)
    {
        Product newProduct = await CopyProduct(request);

        await CopyProductPropertiesVersion(request, newProduct);
        await CopyProductVersions(request, newProduct);
        await CopyProductRouteVersions(request, newProduct);
        await CopyProductSettingVersions(request, newProduct);

        await _context.SaveChangesAsync();

        return;

    }

    private async Task<Product> CopyProduct(CopyProductCommand request)
    {
        var productToCopy = await _context.Products
            .SingleAsync(x => x.Id == request.Id);


        var newProduct = new Product
        {
            Name = productToCopy.Name.Trim() + "_copy",
            ProductNumber = productToCopy.ProductNumber.Trim() + "_copy",
            Idx01 = productToCopy.Idx01,
            Idx02 = productToCopy.Idx02,
            OldProductNumber= productToCopy.OldProductNumber,
            Description = productToCopy.Description,
            ProductCategoryId = productToCopy.ProductCategoryId,
            UnitId = productToCopy.UnitId,

            CreatedByUserId = _currentUser.UserId,
            CreatedDate = _dateTimeService.Now,
            ModifiedByUserId = null,
            ModifiedDate = null,
            IsActive = true,
            IsTest = productToCopy.IsTest,

            ContentsOfRubber = productToCopy.ContentsOfRubber,
            Density = productToCopy.Density,
            ScalesId = productToCopy.ScalesId,
            Aps01 = productToCopy.Aps01,
            Aps02 = productToCopy.Aps02

        };

        await _context.Products.AddAsync(newProduct);
        return newProduct;
    }



    #region Copy Product Properties
    private async Task CopyProductPropertiesVersion(CopyProductCommand request, Product newProduct)
    {
        var productPropertiesVersionsToCopy = await _context.ProductPropertyVersions
                    .Where(x => x.ProductId == request.Id)
                    .AsNoTracking()
                    .ToListAsync();

        //.Where(x => x.ProductId == request.Id && x.DefaultVersion)

        foreach (var propertiesVersion in productPropertiesVersionsToCopy)
        {

            var newProductPropertiesVersion = new ProductPropertyVersion
            {
                Product = newProduct,
                VersionNumber = propertiesVersion.VersionNumber,
                AlternativeNo = propertiesVersion.AlternativeNo,
                Name = propertiesVersion.Name,

                Description = propertiesVersion.Description,
                DefaultVersion = propertiesVersion.DefaultVersion,


                IsAccepted01 = false,
                Accepted01ByUserId = null,
                Accepted01Date = null,

                IsAccepted02 = false,
                Accepted02ByUser = null,
                Accepted02Date = null,

                CreatedByUserId = _currentUser.UserId,
                CreatedDate = _dateTimeService.Now,

                ModifiedByUser = null,
                ModifiedDate = null,

            };


            _context.ProductPropertyVersions.Add(newProductPropertiesVersion);

            await CopyProductPropertiesVersionPositions(request, newProduct, propertiesVersion, newProductPropertiesVersion);

        }
    }
    private async Task CopyProductPropertiesVersionPositions(CopyProductCommand request, Product newProduct, ProductPropertyVersion productPropertiesVersion, ProductPropertyVersion newProductPropertiesVersion)
    {
        var routePositionsToCopy = await _context.ProductProperties
                    .Where(x => x.ProductPropertiesVersionId == productPropertiesVersion.Id)
                    .AsNoTracking()
                    .ToListAsync();

        foreach (var position in routePositionsToCopy)
        {

            var newProductPropertyPosition = new ProductProperty
            {
                ProductPropertiesVersion = newProductPropertiesVersion,
                PropertyId = position.PropertyId,
                MinValue = position.MinValue,
                Value = position.Value,
                MaxValue = position.MaxValue,
                Text = position.Text
   
            };

            // todo docelowo usunąć ProductId

            _context.ProductProperties.Add(newProductPropertyPosition);

        }
    }


    #endregion

    #region Copy Product Setting Versions
    private async Task CopyProductSettingVersions(CopyProductCommand request, Product newProduct)
    {
        var settingVersionsToCopy = await _context.ProductSettingVersions
                    .Where(x => x.ProductId == request.Id)
                    .AsNoTracking()
                    .ToListAsync();

        //.Where(x => x.ProductId == request.Id && x.DefaultVersion)

        foreach (var settingVersion in settingVersionsToCopy)
        {

            var newProductSettingVersion = new ProductSettingVersion
            {
                Product = newProduct,
                ProductSettingVersionNumber = settingVersion.ProductSettingVersionNumber,
                AlternativeNo = settingVersion.AlternativeNo,
                Name = settingVersion.Name,

                MachineCategoryId= settingVersion.MachineCategoryId,
                MachineId= settingVersion.MachineId,


                Description = settingVersion.Description,
                DefaultVersion = settingVersion.DefaultVersion,


                IsAccepted01 = false,
                Accepted01ByUserId = null,
                Accepted01Date = null,

                IsAccepted02 = false,
                Accepted02ByUser = null,
                Accepted02Date = null,

                IsAccepted03 = false,
                Accepted03ByUser = null,
                Accepted03Date = null,

                CreatedByUserId = _currentUser.UserId,
                CreatedDate = _dateTimeService.Now,

                ModifiedByUser = null,
                ModifiedDate = null,


            };


            _context.ProductSettingVersions.Add(newProductSettingVersion);

            await CopyProductSettingVersionPositions(request, newProduct, settingVersion, newProductSettingVersion);

        }
    }
    private async Task CopyProductSettingVersionPositions(CopyProductCommand request, Product newProduct, ProductSettingVersion productSettingVersion, ProductSettingVersion newProductSettingVersion)
    {
        var routePositionsToCopy = await _context.ProductSettingVersionPositions
                    .Where(x => x.ProductSettingVersionId == productSettingVersion.Id)
                    .AsNoTracking()
                    .ToListAsync();

        foreach (var position in routePositionsToCopy)
        {

            var newProductSettingPosition = new ProductSettingVersionPosition
            {
                ProductSettingVersion = newProductSettingVersion,
                SettingId = position.SettingId, 
                MinValue= position.MinValue,
                Value = position.Value,
                MaxValue = position.MaxValue,
                Text = position.Text,
                IsActive = position.IsActive,
                Description = position.Description,
            };

            _context.ProductSettingVersionPositions.Add(newProductSettingPosition);

        }
    }

    
    #endregion


    #region Copy Product Route Version 
    private async Task CopyProductRouteVersions(CopyProductCommand request, Product newProduct)
    {
        var routeVersionsToCopy = await _context.RouteVersions
                    .Where(x => x.ProductId == request.Id)
                    .AsNoTracking()
                    .ToListAsync();

        //.Where(x => x.ProductId == request.Id && x.DefaultVersion)

        foreach (var routeVersion in routeVersionsToCopy)
        {

            var newRouteVersion = new RouteVersion
            {
                Product = newProduct,
                VersionNumber = routeVersion.VersionNumber,
                AlternativeNo = routeVersion.AlternativeNo,
                Name = routeVersion.Name,
                Description = routeVersion.Description,
                DefaultVersion = routeVersion.DefaultVersion,
                IfsDefaultVersion = false,
                ComarchDefaultVersion = false,
                ToIfs = routeVersion.ToIfs,

                IsAccepted01 = false,
                Accepted01ByUserId = null,
                Accepted01Date = null,

                IsAccepted02 = false,
                Accepted02ByUser = null,
                Accepted02Date = null,

                CreatedByUserId = _currentUser.UserId,
                CreatedDate = _dateTimeService.Now,

                ModifiedByUser = null,
                ModifiedDate = null,

                ProductQty = routeVersion.ProductQty,

                ProductCategoryId = routeVersion.ProductCategoryId

            };


            _context.RouteVersions.Add(newRouteVersion);
            
            await CopyRouteVersionPositions(request, newProduct, routeVersion, newRouteVersion);

        }
    }
    private async Task CopyRouteVersionPositions(CopyProductCommand request, Product newProduct, RouteVersion routeVersion, RouteVersion newRouteVersion)
    {
        var routePositionsToCopy = await _context.ManufactoringRoutes
                    .Where(x => x.RouteVersionId == routeVersion.Id)
                    .AsNoTracking()
                    .ToListAsync();

        foreach (var position in routePositionsToCopy)
        {

            var newRoute = new ManufactoringRoute
            {
                RouteVersion = newRouteVersion,
                OrdinalNumber = position.OrdinalNumber,

                WorkCenterId = position.WorkCenterId,

                ResourceId = position.ResourceId,
                ResourceQty = position.ResourceQty,

                OperationId = position.OperationId,
                OperationLabourConsumption = position.OperationLabourConsumption,
                OperationMachineConsumption = position.OperationMachineConsumption,


                ChangeOverResourceId = position.ChangeOverResourceId,
                ChangeOverLabourConsumption = position.ChangeOverLabourConsumption,
                ChangeOverMachineConsumption = position.ChangeOverMachineConsumption,
                ChangeOverNumberOfEmployee = position.ChangeOverNumberOfEmployee,
                ProductCategoryId = position.ProductCategoryId,

                MoveTime = position.MoveTime,
                Overlap = position.Overlap,
                ToolQuantity = position.ToolQuantity,
                RoutingToolId = position.RoutingToolId,
            };

            _context.ManufactoringRoutes.Add(newRoute);

        }
    }
    
    #endregion


    #region Copy Product Bom Version
    private async Task CopyProductVersions(CopyProductCommand request, Product newProduct)
    {
        var productVersionsToCopy = await _context.ProductVersions
                    .Where(x => x.ProductId == request.Id)
                    .AsNoTracking()
                    .ToListAsync();

        // .Where(x => x.ProductId == request.Id && x.DefaultVersion)

        foreach (var productVersion in productVersionsToCopy)
        {

            var newProductVersion = new ProductVersion
            {
                Product = newProduct,
                VersionNumber = productVersion.VersionNumber,
                AlternativeNo = productVersion.AlternativeNo,
                Name = productVersion.Name,
                Description = productVersion.Description,
                DefaultVersion = productVersion.DefaultVersion,
                IfsDefaultVersion = false,
                ComarchDefaultVersion = false,
                ToIfs = productVersion.ToIfs,

                IsAccepted01 = false,
                Accepted01ByUserId = null,
                Accepted01Date = null,

                IsAccepted02 = false,
                Accepted02ByUser = null,
                Accepted02Date = null,

                CreatedByUserId = _currentUser.UserId,
                CreatedDate = _dateTimeService.Now,

                ModifiedByUser = null,
                ModifiedDate = null,

                ProductQty = productVersion.ProductQty,
                ProductWeight = productVersion.ProductWeight,
            };


            _context.ProductVersions.Add(newProductVersion);
            await CopyProductVersionBoms(request, newProduct, productVersion, newProductVersion);

        }
    }
    private async Task CopyProductVersionBoms(CopyProductCommand request, Product newProduct, ProductVersion productVersion, ProductVersion newProductVersion)
    {
        var productBomsToCopy = await _context.Boms
                    .Where(x => x.SetId == request.Id && x.SetVersionId == productVersion.Id)
                    .AsNoTracking()
                    .ToListAsync();

        foreach (var bom in productBomsToCopy)
        {

            var newBom = new Bom
            {
                Set = newProduct,
                SetVersion = newProductVersion,
                PartId = bom.PartId,
                PartQty = bom.PartQty,
                Excess = bom.Excess,
                OnProductionOrder = bom.OnProductionOrder,
                OrdinalNumber = bom.OrdinalNumber,
                DoNotExportToIfs = bom.DoNotExportToIfs,
                DoNotIncludeInTkw = bom.DoNotIncludeInTkw,
                DoNotIncludeInWeight = bom.DoNotIncludeInWeight,
                Layer = bom.Layer
            };

            _context.Boms.Add(newBom);

        }
    }

    #endregion
}
