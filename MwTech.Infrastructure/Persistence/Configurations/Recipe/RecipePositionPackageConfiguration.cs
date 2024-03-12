
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Infrastructure.Persistence.Configurations;

public class RecipePositionPackageConfiguration : IEntityTypeConfiguration<RecipePositionsPackage>
{
    public void Configure(EntityTypeBuilder<RecipePositionsPackage> builder)
    {
        builder
            .Property(x => x.PackageNumber)
            .IsRequired();

        builder
            .Property(x => x.ProductName)
            .HasMaxLength(50);

        builder
            .Property(x => x.ProductNumber)
            .HasMaxLength(35);

        // relacja z cyklem
        builder.HasOne<RecipeStage>(p => p.RecipeStage)
        .WithMany(u => u.RecipePositionsPackages)
        .HasForeignKey(p => p.RecipeStageId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z gniazdem roboczym
        builder.HasOne<Resource>(p => p.WorkCenter)
        .WithMany(u => u.PackageWorkCenters)
        .HasForeignKey(p => p.WorkCenterId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z kategorią zaszeregowania
        builder.HasOne<Resource>(p => p.LabourClass)
        .WithMany(u => u.PackageLabourClasses)
        .HasForeignKey(p => p.LabourClassId)
        .OnDelete(DeleteBehavior.Restrict);

    }
}
