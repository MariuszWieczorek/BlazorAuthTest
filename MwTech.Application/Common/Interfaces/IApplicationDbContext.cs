using MwTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Unit = MwTech.Domain.Entities.Unit;
using MwTech.Domain.Entities.Ifs;
using MwTech.Domain.Entities.Recipes;
using MwTech.Domain.Entities.Measurements;
using EFCore.BulkExtensions;
using MwTech.Domain.Entities.Tyres;

namespace MwTech.Application.Common.Interfaces;

public interface IApplicationDbContext : IDisposable
{
    DbSet<Bom> Boms { get; set; }
    DbSet<ManufactoringRoute> ManufactoringRoutes { get; set; }
    DbSet<Operation> Operations { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<ProductCategory> ProductCategories { get; set; }
    DbSet<Machine> Machines { get; set; }
    DbSet<MachineCategory> MachineCategories { get; set; }
    DbSet<ProductVersion> ProductVersions { get; set; }
    DbSet<RouteVersion> RouteVersions { get; set; }
    DbSet<Resource> Resources { get; set; }
    DbSet<Unit> Units { get; set; }
    DbSet<BomTree> BomTrees { get; set; }
    DbSet<Property> Properties { get; set; }
    DbSet<ProductVersionProperty> ProductVersionProperties { get; set; }
    DbSet<ProductProperty> ProductProperties { get; set; }
    DbSet<SettingCategory> SettingCategories { get; set; }
    DbSet<Setting> Settings { get; set; }
    DbSet<ProductSettingVersion> ProductSettingVersions { get; set; }
    DbSet<ProductPropertyVersion> ProductPropertyVersions { get; set; }
    DbSet<ProductSettingVersionPosition> ProductSettingVersionPositions { get; set; }
    DbSet<AccountingPeriod> AccountingPeriods { get; set; }
    DbSet<ProductCost> ProductCosts { get; set; }
    DbSet<Currency> Currencies { get; set; }
    DbSet<CurrencyRate> CurrencyRates { get; set; }
    DbSet<AppSettingPosition> AppSettingsPositions { get; set; }
    DbSet<AppSetting> AppSettings { get; set; }
    DbSet<PropertiesProductCategoriesMap> PropertiesProductCategoriesMaps { get; set; }
    DbSet<IfsWorkCenterMaterialRequest> IfsWorkCentersMaterialsRequests { get; set; }
    DbSet<Recipe> Recipes { get; set; }
    DbSet<RecipeVersion> RecipeVersions { get; set; }
    DbSet<RecipeCategory> RecipeCategories { get; set; }
    DbSet<RecipeStage> RecipeStages { get; set; }
    DbSet<RecipePositionsPackage> RecipePositionsPackages { get; set; }
    DbSet<RecipeManual> RecipeManuals { get; set; }
    DbSet<RecipePosition> RecipePositions { get; set; }
    DbSet<MeasurementHeader> MeasurementHeaders { get; set; }
    DbSet<MeasurementPosition> MeasurementPositions { get; set; }
    DbSet<Temp> Temps { get; set; }
    DbSet<ApplicationUser> Users { get; set; }
    DbSet<IfsRoute> IfsRoutes { get; set; }
    DbSet<IfsProductStructure> IfsProductStructures { get; set; }
    DbSet<IfsProductRecipe> IfsProductRecipes { get; set; }
    DbSet<RoutingTool> RoutingTools { get; set; }

    DbSet<Tyre> Tyres { get; set; }


    // Views
    DbSet<ComparedStructureIfsVsMwTech>  ComparedStructuresIfsVsMwTech { get; set; }
    DbSet<ComparedRecipeIfsVsMwTech>  ComparedRecipesIfsVsMwTech { get; set; }
    DbSet<ComparedRouteIfsVsMwTech>  ComparedRoutesIfsVsMwTech { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task BulkInsertAsync<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where T : class;

    void BulkInsert<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where T : class;


    void Clear();
}
