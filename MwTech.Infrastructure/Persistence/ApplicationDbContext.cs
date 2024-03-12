using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Unit = MwTech.Domain.Entities.Unit;
using MwTech.Infrastructure.Persistence.Extensions;
using MwTech.Domain.Entities.Ifs;
using System.Data;
using MwTech.Domain.Entities.Recipes;
using MwTech.Domain.Entities.Measurements;
using EFCore.BulkExtensions;
using MwTech.Domain.Entities.Tyres;

namespace MwTech.Infrastructure.Persistence;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<Bom> Boms { get; set; }
    public DbSet<ManufactoringRoute> ManufactoringRoutes { get; set; }
    public DbSet<Operation> Operations { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Machine> Machines { get; set; }
    public DbSet<MachineCategory> MachineCategories { get; set; }
    public DbSet<ProductVersion> ProductVersions { get; set; }
    public DbSet<RouteVersion> RouteVersions { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<BomTree> BomTrees { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<ProductVersionProperty> ProductVersionProperties { get; set; }
    public DbSet<ProductProperty> ProductProperties { get; set; }
    public DbSet<SettingCategory> SettingCategories { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<ProductSettingVersion> ProductSettingVersions { get; set; }
    public DbSet<ProductPropertyVersion> ProductPropertyVersions { get; set; }
    public DbSet<ProductSettingVersionPosition> ProductSettingVersionPositions { get; set; }
    public DbSet<AccountingPeriod> AccountingPeriods { get; set; }
    public DbSet<ProductCost> ProductCosts { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<CurrencyRate> CurrencyRates { get; set; }
    public DbSet<AppSettingPosition> AppSettingsPositions { get; set; }
    public DbSet<AppSetting> AppSettings { get; set; }
    public DbSet<PropertiesProductCategoriesMap> PropertiesProductCategoriesMaps { get; set; }
    public DbSet<IfsWorkCenterMaterialRequest> IfsWorkCentersMaterialsRequests { get; set; }
    public DbSet<Temp> Temps { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipeVersion> RecipeVersions { get; set; }
    public DbSet<RecipeCategory> RecipeCategories { get; set; }
    public DbSet<RecipeStage> RecipeStages { get; set; }
    public DbSet<RecipePositionsPackage> RecipePositionsPackages { get; set; }
    public DbSet<RecipeManual> RecipeManuals { get; set; }
    public DbSet<RecipePosition> RecipePositions { get; set; }
    public DbSet<MeasurementHeader> MeasurementHeaders { get; set; }
    public DbSet<MeasurementPosition> MeasurementPositions { get; set; }
    public DbSet<IfsRoute> IfsRoutes { get; set; }
    public DbSet<IfsProductStructure> IfsProductStructures { get; set; }
    public DbSet<IfsProductRecipe> IfsProductRecipes { get; set; }
    public DbSet<RoutingTool> RoutingTools { get; set; }

    public DbSet<Tyre> Tyres { get; set; }


    // Views

    public DbSet<ComparedStructureIfsVsMwTech>  ComparedStructuresIfsVsMwTech { get; set; }
    public DbSet<ComparedRecipeIfsVsMwTech>  ComparedRecipesIfsVsMwTech { get; set; }
    public DbSet<ComparedRouteIfsVsMwTech>  ComparedRoutesIfsVsMwTech { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

           modelBuilder.SeedAppSettings();
           modelBuilder.SeedAppSettingPositions();
           modelBuilder.SeedRoles();
           modelBuilder.SeedUsers();


        base.OnModelCreating(modelBuilder);

        
    }

    public void BulkInsert<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where T : class
    {
        this.BulkInsertOrUpdate(entities, bulkConfig, progress);
    }

    public async Task BulkInsertAsync<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where T : class
    {
        await ((DbContext)this).BulkInsertAsync<T>(entities, bulkConfig, progress);
    }

    public void Clear()
    {
        this.ChangeTracker.Clear();
    }

}
