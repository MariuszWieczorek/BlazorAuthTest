using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Recipes.RecipeVersions.Commands.CreateProduct;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.CreateProducts;

public class CreateProductsFromRecipeCommandHandler : IRequestHandler<CreateProductsFromRecipeCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IRecipeService _recipeService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;
    private readonly IProductService _productService;
    private readonly decimal _bagQty = 0.06M;

    public CreateProductsFromRecipeCommandHandler(
        IApplicationDbContext context,
        IRecipeService recipeService,
        ICurrentUserService currentUserService,
        IDateTimeService dateTimeService,
        IProductService productService
        )
    {
        _context = context;
        _recipeService = recipeService;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
        _productService = productService;
    }

    public async Task Handle(CreateProductsFromRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipeVersion = await _context.RecipeVersions
            .Include(x => x.Accepted01ByUser)
            .Include(x => x.Accepted02ByUser)
            .Include(x => x.CreatedByUser)
            .Include(x => x.Recipe)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == request.RecipeVersionId);


        var recipeStages = await _recipeService.GetRecipeVersionStages(request.RecipeVersionId);

        var recipeProducts = await GetRecipeProducts(recipeStages, recipeVersion);

        await AddNewProducts(recipeProducts);

        // Bom
        
        await AddNewProductsVersions(recipeProducts, recipeVersion);

        await DeleteComponentsFromVersions(recipeProducts, recipeVersion);

        await AddComponentsToVersions(recipeProducts, recipeVersion, recipeStages);

        await CalculateProductsVersionWeight(recipeProducts, recipeVersion);

        // Marszruty 

        await AddRouteVersions(recipeProducts, recipeVersion);

        await DeletePositionsFromRouteVersions(recipeProducts, recipeVersion);

        await AddOperationsToVersions(recipeVersion, recipeStages);

    }

    // -------------------------------------------------------------------------------------------------------------------

    public async Task<List<RecipeProduct>> GetRecipeProducts(IEnumerable<RecipeStage> recipeStages
                , RecipeVersion recipeVersion)
    {
        List<RecipeProduct> recipeProducts = new List<RecipeProduct>();
        await GetProductsFromStages(recipeStages, recipeVersion, recipeProducts);
        await GetProductsFromPackages(recipeStages, recipeVersion, recipeProducts);

        return recipeProducts;

    }

    private async Task GetProductsFromStages(IEnumerable<RecipeStage> recipeStages
                , RecipeVersion recipeVersion
                , List<RecipeProduct> recipeProducts)
    {
        foreach (var recipeStage in recipeStages)
        {
            var product = await _context.Products
                .SingleOrDefaultAsync(x => x.ProductNumber == recipeStage.ProductNumber);


            var categoryForNewProduct = await _context.ProductCategories
                .SingleOrDefaultAsync(x => x.CategoryNumber == "MIE-1");

            var unit = await _context.Units.SingleOrDefaultAsync(x => x.UnitCode == "kg");

            var recipeProduct = new RecipeProduct
            {
                ProductNumber = recipeStage.ProductNumber,
                Name = recipeStage.ProductName,
                ProductCategoryId = product == null ? categoryForNewProduct.Id : product.ProductCategoryId,
                ProductQty = recipeStage.TotalQty2.GetValueOrDefault(),
                UnitId = unit.Id,
                ProductInDb = product != null,
                StageId = recipeStage.Id,
                VersionNumber = recipeVersion.VersionNumber,
                AlternativeNo = recipeVersion.AlternativeNo,
                CalculateWeightOrder = recipeStage.StageNo

            };

            recipeProducts.Add(recipeProduct);

        }
    }

    private async Task GetProductsFromPackages(IEnumerable<RecipeStage> recipeStages
                , RecipeVersion recipeVersion
                , List<RecipeProduct> recipeProducts)
    {
        var categoryForNewProduct = await _context.ProductCategories
            .SingleOrDefaultAsync(x => x.CategoryNumber == "NAW");

        var unit = await _context.Units.SingleOrDefaultAsync(x => x.UnitCode == "kg");

        foreach (var recipeStage in recipeStages)
        {

            foreach (var package in recipeStage.RecipePositionsPackages)
            {
                var product = await _context.Products.SingleOrDefaultAsync(x => x.ProductNumber == package.ProductNumber);


                var recipeProduct = new RecipeProduct
                {
                    ProductNumber = package.ProductNumber,
                    Name = package.ProductName,
                    ProductCategoryId = product == null ? categoryForNewProduct.Id : product.ProductCategoryId,
                    UnitId = unit.Id,
                    ProductQty = package.TotalQty.GetValueOrDefault(),
                    ProductInDb = product != null,
                    RecipeId = recipeStage.Id,
                    PackageId = package.Id,
                    VersionNumber = recipeVersion.VersionNumber,
                    AlternativeNo = recipeVersion.AlternativeNo,
                    CalculateWeightOrder = 0
                };

                recipeProducts.Add(recipeProduct);
            }
        }
    }


    private async Task AddNewProducts(List<RecipeProduct> recipeProducts)
    {
        _context.Clear();
        var currentUserId = _currentUserService.UserId;

        foreach (var product in recipeProducts)
        {
            if (!product.ProductInDb)
            {
                var productToAdd = new Product
                {
                    ProductNumber = product.ProductNumber,
                    Name = product.Name,
                    UnitId = product.UnitId,
                    ProductCategoryId = product.ProductCategoryId,
                    IsActive = true,
                    IsTest = false,
                    CreatedByUserId = currentUserId,
                    CreatedDate = _dateTimeService.Now,
                };

                _context.Products.Add(productToAdd);
            }

        }
        await _context.SaveChangesAsync();
    }

    private async Task AddNewProductsVersions(List<RecipeProduct> productsToCreate, RecipeVersion recipeVersion)
    {
        _context.Clear();
        var currentUserId = _currentUserService.UserId;


        foreach (var product in productsToCreate)
        {

            var productX = await _context.Products
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ProductNumber == product.ProductNumber);

            var productVersion = await _context.ProductVersions
                .Include(x => x.Product)
                .SingleOrDefaultAsync(x =>
                    x.Product.ProductNumber == product.ProductNumber
                 && x.VersionNumber == recipeVersion.VersionNumber
                 && x.AlternativeNo == recipeVersion.AlternativeNo);

            var defaultProductVersion = await _context.ProductVersions
                .Include(x => x.Product)
                .AsNoTracking()
                .SingleOrDefaultAsync(x =>
                    x.Product.ProductNumber == product.ProductNumber
                 && x.DefaultVersion == true
                 && x.IsActive == true
                 );

            if (productVersion == null)
            {
                var productVersionToAdd = new ProductVersion
                {
                    ProductId = productX.Id,
                    Name = recipeVersion.Name,
                    VersionNumber = recipeVersion.VersionNumber,
                    AlternativeNo = recipeVersion.AlternativeNo,
                    IsActive = true,
                    DefaultVersion = defaultProductVersion == null,
                    CreatedByUserId = currentUserId,
                    CreatedDate = _dateTimeService.Now,
                    ProductQty = product.ProductQty
                };

                _context.ProductVersions.Add(productVersionToAdd);
            }
            else
            {
                productVersion.IsActive = true;
                productVersion.Name = recipeVersion.Name;
                productVersion.ModifiedByUserId = currentUserId;
                productVersion.ModifiedDate = _dateTimeService.Now;

            }

        }
        await _context.SaveChangesAsync();

    }

    private async Task DeleteComponentsFromVersions(List<RecipeProduct> productsToCreate, RecipeVersion recipeVersion)
    {
        _context.Clear();
        var currentUserId = _currentUserService.UserId;


        foreach (var product in productsToCreate)
        {

            var productVersion = await _context.ProductVersions
                .Include(x => x.Product)
                .SingleOrDefaultAsync(x =>
                    x.Product.ProductNumber == product.ProductNumber
                 && x.VersionNumber == recipeVersion.VersionNumber
                 && x.AlternativeNo == recipeVersion.AlternativeNo);

            var bomsToDelete = await _context.Boms
                .Where(x =>
                   x.SetId == productVersion.ProductId
                && x.SetVersionId == productVersion.Id
                ).ToListAsync();

            foreach (var bom in bomsToDelete)
            {
                _context.Boms.Remove(bom);
            }


        }
        await _context.SaveChangesAsync();

    }

    private async Task AddComponentsToVersions(List<RecipeProduct> productsToCreate, RecipeVersion recipeVersion, IEnumerable<RecipeStage> recipeStages)
    {
        _context.Clear();
        var currentUserId = _currentUserService.UserId;

        foreach (var stage in recipeStages)
        {

            var stageProductVersion = await _context.ProductVersions
                    .Include(x => x.Product)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x =>
                        x.VersionNumber == recipeVersion.VersionNumber
                    && x.AlternativeNo == recipeVersion.AlternativeNo
                    && x.Product.ProductNumber == stage.ProductNumber
                    );

            int stagePositionNo = 1;

            // dodanie surowców do cyklu
            stagePositionNo = await AddMaterialsToStage(stage, stageProductVersion, stagePositionNo);

            // Dodanie naważek do Cyklu
            stagePositionNo = await AddPackagesToStage(stage, stageProductVersion, stagePositionNo);

            // Dodanie surowców do naważek
            await AddMaterialsToPackages(recipeVersion, stage);

        }

        await _context.SaveChangesAsync();
    }

    


    private async Task<int> AddPackagesToStage(RecipeStage stage, ProductVersion stageProductVersion, int stagePositionNo)
    {
        foreach (var package in stage.RecipePositionsPackages)
        {

            var component = _context.Products
                .AsNoTracking()
                .SingleOrDefault(x =>
                x.ProductNumber == package.ProductNumber
                );

            if (component != null)
            {
                decimal packageQty = package.TotalQty.GetValueOrDefault();

                // #3396
                // Zmiana przy generowaniu indeksu, tam gdzie ilość naważki
                // podajemy razem z masą worka MPW014 – w składzie mieszanki.

                if (package.BagIsIncluded)
                {
                    packageQty += _bagQty;
                }

                await AddPositionToBom(stageProductVersion.Id, stageProductVersion.ProductId, component.Id, packageQty, stagePositionNo++);
            }

        }

        // dadajemy preparat
        var prep = await _context.Products.SingleOrDefaultAsync(x => x.ProductNumber == "MPW016");
        if (prep != null)
        {
            decimal prepQty = stage.TotalQty2.GetValueOrDefault() * 0.0004M;
            await AddPositionToBom(stageProductVersion.Id, stageProductVersion.ProductId, prep.Id, prepQty, stagePositionNo++, false);
        }

        return stagePositionNo;
    }
    private async Task<int> AddMaterialsToStage(RecipeStage stage, ProductVersion stageProductVersion, int stagePositionNo)
    {
        foreach (var item in stage.RecipePositions)
        {


            if (item.RecipePositionPackage == null)
            {


                int componentId = item.ProductId;

                if (componentId == 0)
                {
                    var component = _context.Products
                            .AsNoTracking()
                            .SingleOrDefault(x =>
                            x.ProductNumber == item.Product.ProductNumber
                        );
                    if (component != null)
                        componentId = component.Id;
                }

                await AddPositionToBom(stageProductVersion.Id, stageProductVersion.ProductId, componentId, item.PartQty2, stagePositionNo++);
            }
        }



        return stagePositionNo;
    }

    private async Task AddMaterialsToPackages(RecipeVersion recipeVersion, RecipeStage stage)
    {
        foreach (var package in stage.RecipePositionsPackages)
        {

            var packageProductVersion = await _context.ProductVersions
                .Include(x => x.Product)
                .AsNoTracking()
                .SingleOrDefaultAsync(x =>
                    x.VersionNumber == recipeVersion.VersionNumber
                && x.AlternativeNo == recipeVersion.AlternativeNo
                && x.Product.ProductNumber == package.ProductNumber
                );

            int packagePositionNo = 1;


            foreach (var item in package.RecipePositions)
            {
                await AddPositionToBom(packageProductVersion.Id, packageProductVersion.ProductId, item.ProductId, item.PartQty2, packagePositionNo++, true);
            }


            // dadajemy worek
            if (package.BagIsIncluded)
            {
                var bag = await _context.Products.SingleOrDefaultAsync(x => x.ProductNumber == "MPW014");
                if (bag != null)
                {
                    await AddPositionToBom(packageProductVersion.Id, packageProductVersion.ProductId, bag.Id, _bagQty, packagePositionNo++, true);
                }
            }

        }

    }



    private async Task CalculateProductsVersionWeight(List<RecipeProduct> productsToCreate, RecipeVersion recipeVersion)
    {
        var naws = productsToCreate.Where(x => x.ProductNumber.Contains("NAW")).ToList();
        await CalculateWeight(naws, recipeVersion);
        var mie = productsToCreate
            .Where(x => x.ProductNumber.Contains("MIE"))
            .OrderBy(x=>x.CalculateWeightOrder)
            .ToList();
        await CalculateWeight(mie, recipeVersion);
    }

    private async Task CalculateWeight(List<RecipeProduct> productsToCreate, RecipeVersion recipeVersion)
    {
        foreach (var product in productsToCreate)
        {
            var prod = await _context.Products
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ProductNumber == product.ProductNumber);


            if (prod != null)
            {
                var ver = await _context.ProductVersions
                    .SingleOrDefaultAsync(x =>
                       x.ProductId == prod.Id
                    && x.VersionNumber == recipeVersion.VersionNumber
                    && x.AlternativeNo == recipeVersion.AlternativeNo
                    );


                if (ver != null)
                {
                    var weight = await _productService.CalculateWeight(prod.Id, ver.Id);
                    ver.ProductQty = weight;
                    await _context.SaveChangesAsync();
                }
            }

        }
    }



    private async Task AddPositionToBom(int versionId, int productId, int componentId, decimal? partQty2, int positionNo, bool consumed = true)
    {
        var bom = new Bom()
        {
            SetVersionId = versionId,
            SetId = productId,
            PartId = componentId,
            PartQty = partQty2.GetValueOrDefault(),
            OnProductionOrder = consumed,
            OrdinalNumber = positionNo
        };

        await _context.Boms.AddAsync(bom);
    }



    private async Task AddRouteVersions(List<RecipeProduct> productsToCreate, RecipeVersion recipeVersion)
    {
        _context.Clear();
        var currentUserId = _currentUserService.UserId;


        foreach (var product in productsToCreate)
        {

            var productX = await _context.Products
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ProductNumber == product.ProductNumber);

            var routeVersion = await _context.RouteVersions
                .Include(x => x.Product)
                .SingleOrDefaultAsync(x =>
                    x.Product.ProductNumber == product.ProductNumber
                 && x.VersionNumber == recipeVersion.VersionNumber
                 && x.AlternativeNo == recipeVersion.AlternativeNo);

            var defaultRouteVersion = await _context.RouteVersions
                .Include(x => x.Product)
                .AsNoTracking()
                .SingleOrDefaultAsync(x =>
                    x.Product.ProductNumber == product.ProductNumber
                 && x.DefaultVersion == true
                 && x.IsActive == true
                 );

            if (routeVersion == null)
            {
                var routeVersionToAdd = new RouteVersion
                {
                    ProductId = productX.Id,
                    Name = recipeVersion.Name,
                    VersionNumber = recipeVersion.VersionNumber,
                    AlternativeNo = recipeVersion.AlternativeNo,
                    IsActive = true,
                    DefaultVersion = defaultRouteVersion == null,
                    CreatedByUserId = currentUserId,
                    CreatedDate = _dateTimeService.Now,
                    ProductQty = 1
                };

                _context.RouteVersions.Add(routeVersionToAdd);
            }
            else
            {
                routeVersion.IsActive = true;
                routeVersion.Name = recipeVersion.Name;
                routeVersion.ModifiedByUserId = currentUserId;
                routeVersion.ModifiedDate = _dateTimeService.Now;

            }

        }
        await _context.SaveChangesAsync();

    }


    private async Task DeletePositionsFromRouteVersions(List<RecipeProduct> productsToCreate, RecipeVersion recipeVersion)
    {
        _context.Clear();
        var currentUserId = _currentUserService.UserId;


        foreach (var product in productsToCreate)
        {

            var routeVersion = await _context.RouteVersions
                .Include(x => x.Product)
                .SingleOrDefaultAsync(x =>
                    x.Product.ProductNumber == product.ProductNumber
                 && x.VersionNumber == recipeVersion.VersionNumber
                 && x.AlternativeNo == recipeVersion.AlternativeNo);

            var routesToDelete = await _context.ManufactoringRoutes
                .Include(x=>x.RouteVersion)
                .Where(x =>
                   x.RouteVersion.ProductId == routeVersion.ProductId
                   &&
                   x.RouteVersionId == routeVersion.Id
                ).ToListAsync();

            foreach (var route in routesToDelete)
            {
                _context.ManufactoringRoutes.Remove(route);
            }


        }
        await _context.SaveChangesAsync();

    }


    private async Task AddOperationsToVersions(RecipeVersion recipeVersion, IEnumerable<RecipeStage> recipeStages)
    {
        _context.Clear();
        var currentUserId = _currentUserService.UserId;

        foreach (var stage in recipeStages)
        {
            await AddStageOperationsToVersions(recipeVersion, stage);

            foreach (var package in stage.RecipePositionsPackages)
            {
                await AddPackageOperationsToVersions(recipeVersion, package);
            }

            
        }

        await _context.SaveChangesAsync();
    }

    private async Task AddPackageOperationsToVersions(RecipeVersion recipeVersion, RecipePositionsPackage package)
    {
        var routeVersion = await _context.RouteVersions
            .Include(x => x.Product)
            //.AsNoTracking()
            .SingleOrDefaultAsync(x =>
                x.VersionNumber == recipeVersion.VersionNumber
            && x.AlternativeNo == recipeVersion.AlternativeNo
            && x.Product.ProductNumber == package.ProductNumber
            );

        var workCenter = await _context.Resources
            .SingleOrDefaultAsync(x => x.Id == package.WorkCenterId);

        if ( workCenter != null )
        {
            routeVersion.Name = workCenter.ResourceNumber; 
        }
        

        decimal unitPerHour = 0;

        if (package.TimeInSeconds != 0)
        {
            unitPerHour = (package.TotalQty.GetValueOrDefault() / package.TimeInSeconds) * 3600;
        }


        var route = new ManufactoringRoute()
        {
            RouteVersionId = routeVersion.Id,
            WorkCenterId = package.WorkCenterId.GetValueOrDefault(),
            ResourceId = package.LabourClassId.GetValueOrDefault(),
            ChangeOverResourceId = package.LabourClassId.GetValueOrDefault(),
            OperationId = _context.Operations.SingleOrDefault(x => x.OperationNumber == "PM.010.NAWAZANIE").Id,
            OperationLabourConsumption = unitPerHour,
            OperationMachineConsumption = unitPerHour,
            ResourceQty = package.CrewSize.GetValueOrDefault(),
            ChangeOverNumberOfEmployee = package.CrewSize.GetValueOrDefault(),
            OrdinalNumber = 10
        };

        await _context.ManufactoringRoutes.AddAsync(route);
    }

    private async Task AddStageOperationsToVersions(RecipeVersion recipeVersion, RecipeStage stage)
    {
        var routeVersion = await _context.RouteVersions
        .Include(x => x.Product)
        .AsNoTracking()
        .SingleOrDefaultAsync(x =>
            x.VersionNumber == recipeVersion.VersionNumber
        && x.AlternativeNo == recipeVersion.AlternativeNo
        && x.Product.ProductNumber == stage.ProductNumber
        );



        decimal unitPerHour = 0;

        if (stage.StageTimeInSeconds != 0)
        {
            unitPerHour = (stage.TotalQty2.GetValueOrDefault() / stage.StageTimeInSeconds) * 3600;
        }


        var route = new ManufactoringRoute()
        {
            RouteVersionId = routeVersion.Id,
            WorkCenterId = stage.WorkCenterId.GetValueOrDefault(),
            ResourceId = stage.LabourClassId.GetValueOrDefault(),
            ChangeOverResourceId = stage.LabourClassId.GetValueOrDefault(),
            OperationId = _context.Operations.SingleOrDefault(x => x.OperationNumber == "PM.020_MIESZANIE").Id,
            OperationLabourConsumption = unitPerHour,
            OperationMachineConsumption = unitPerHour,
            ResourceQty = stage.CrewSize.GetValueOrDefault(),
            ChangeOverNumberOfEmployee = stage.CrewSize.GetValueOrDefault(),
            OrdinalNumber = 10
        };

        await _context.ManufactoringRoutes.AddAsync(route);
    }
   

}




