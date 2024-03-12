using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Unit = MwTech.Domain.Entities.Unit;
using MwTech.Infrastructure.Persistence.Extensions;
using MwTech.Domain.Entities.Ifs;

namespace MwTech.Infrastructure.Persistence;
public class OracleDbContext : IdentityDbContext<ApplicationUser>, IOracleDbContext
{

    public OracleDbContext(DbContextOptions<OracleDbContext> options) : base(options)
    {

    }
    public DbSet<IfsScadaOperation> IfsScadaOperations { get; set; }
    public DbSet<IfsScadaMaterial> IfsScadaMaterials { get; set; }
    public DbSet<IfsWorkCenterOperation> IfsWorkCenterOperations { get; set; }
    public DbSet<IfsWorkCenterMaterial> IfsWorkCenterMaterials { get; set; }
    public DbSet<IfsActiveSeparateUiv> IfsActiveSeparateUivs { get; set; }
    public DbSet<IfsInventoryPartInStock> IfsInventoryPartsInStock { get; set; }
    public DbSet<IfsWorkCenterOperationsReport> IfsWorkCentersOperationsReports { get; set; }
    public DbSet<IfsWorkCenterOperationsByMonthDepart> IfsWorkCenterOperationsByMonthDepartReports { get; set; }
    public DbSet<IfsSourceRoute> IfsSourceRoutes { get; set; }
    public DbSet<IfsSourceProductRecipe> IfsSourceProductRecipes { get; set; }
    public DbSet<IfsSourceProductStructure> IfsSourceProductStructures { get; set; }
    public DbSet<IfsRoutingTool> IfsRoutingTools { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
