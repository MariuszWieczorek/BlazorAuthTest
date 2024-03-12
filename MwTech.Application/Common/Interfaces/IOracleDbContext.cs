using Microsoft.EntityFrameworkCore;
using Unit = MwTech.Domain.Entities.Unit;
using MwTech.Domain.Entities.Ifs;

namespace MwTech.Application.Common.Interfaces;

public interface IOracleDbContext : IDisposable 
{
    DbSet<IfsScadaOperation> IfsScadaOperations { get; set; }
    DbSet<IfsScadaMaterial> IfsScadaMaterials { get; set; }
    DbSet<IfsWorkCenterOperation> IfsWorkCenterOperations { get; set; }
    DbSet<IfsWorkCenterMaterial> IfsWorkCenterMaterials { get; set; }
    DbSet<IfsActiveSeparateUiv> IfsActiveSeparateUivs { get; set; }
    DbSet<IfsInventoryPartInStock> IfsInventoryPartsInStock { get; set; }
    DbSet<IfsWorkCenterOperationsReport> IfsWorkCentersOperationsReports { get; set; }
    DbSet<IfsWorkCenterOperationsByMonthDepart> IfsWorkCenterOperationsByMonthDepartReports { get; set; }
    DbSet<IfsSourceRoute> IfsSourceRoutes { get; set; }
    DbSet<IfsRoutingTool> IfsRoutingTools { get; set; }
    DbSet<IfsSourceProductRecipe> IfsSourceProductRecipes { get; set; }
    DbSet<IfsSourceProductStructure> IfsSourceProductStructures { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
