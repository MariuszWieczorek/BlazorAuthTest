
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Infrastructure.Persistence.Configurations;

public class RecipeManualConfiguration : IEntityTypeConfiguration<RecipeManual>
{
    public void Configure(EntityTypeBuilder<RecipeManual> builder)
    {

        builder
            .Property(x => x.Duration)
            .HasColumnType("decimal")
            .HasPrecision(12, 5);



        // relacja z wersją produktu
        builder.HasOne<RecipeVersion>(p => p.RecipeVersion)
        .WithMany(u => u.RecipeManuals)
        .HasForeignKey(p => p.RecipeVersionId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z cyklem
        builder.HasOne<RecipeStage>(p => p.RecipeStage)
        .WithMany(u => u.RecipeManuals)
        .HasForeignKey(p => p.RecipeStageId)
        .OnDelete(DeleteBehavior.Restrict);


    }
}
