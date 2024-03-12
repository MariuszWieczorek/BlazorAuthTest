
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Infrastructure.Persistence.Configurations;

public class RecipePositionConfiguration : IEntityTypeConfiguration<RecipePosition>
{
    public void Configure(EntityTypeBuilder<RecipePosition> builder)
    {
        builder
                 .Property(x => x.ProductId)
                 .IsRequired();

        builder
            .Property(x => x.ProductQty)
            .HasColumnType("decimal")
            .HasPrecision(12, 5)
            .IsRequired();


        // relacja z produktem
        builder.HasOne<Product>(p => p.Product)
        .WithMany(u => u.RecipePositions)
        .HasForeignKey(p => p.ProductId)
        .OnDelete(DeleteBehavior.Restrict);


        // relacja z cyklem
        builder.HasOne<RecipeStage>(p => p.RecipeStage)
        .WithMany(u => u.RecipePositions)
        .HasForeignKey(p => p.RecipeStageId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z naważką
        builder.HasOne<RecipePositionsPackage>(p => p.RecipePositionPackage)
        .WithMany(u => u.RecipePositions)
        .HasForeignKey(p => p.RecipePositionPackageId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
