
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Infrastructure.Persistence.Configurations;

public class RecipeVersionConfiguration : IEntityTypeConfiguration<RecipeVersion>
{
    public void Configure(EntityTypeBuilder<RecipeVersion> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired();


        // relacja z produktem
        builder.HasOne<Recipe>(p => p.Recipe)
        .WithMany(u => u.RecipeVersions)
        .HasForeignKey(p => p.RecipeId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z użytkownikami
        builder.HasOne<ApplicationUser>(p => p.CreatedByUser)
        .WithMany(u => u.CreatedRecipeVersions)
        .HasForeignKey(p => p.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<ApplicationUser>(p => p.ModifiedByUser)
        .WithMany(u => u.ModifiedRecipeVersions)
        .HasForeignKey(p => p.ModifiedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<ApplicationUser>(p => p.Accepted01ByUser)
        .WithMany(u => u.Accepted01RecipeVersions)
        .HasForeignKey(p => p.Accepted01ByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<ApplicationUser>(p => p.Accepted02ByUser)
        .WithMany(u => u.Accepted02RecipeVersions)
        .HasForeignKey(p => p.Accepted02ByUserId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
