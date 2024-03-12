
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Recipes;


namespace MwTech.Infrastructure.Persistence.Configurations;

public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired();

        builder
        .HasIndex(u => u.RecipeNumber)
        .IsUnique();


        // Relacja z Kategorią
        builder.HasOne<RecipeCategory>(x=>x.RecipeCategory)
            .WithMany(x=>x.Recipes)
            .HasForeignKey(x=>x.RecipeCategoryId)
            .OnDelete(DeleteBehavior.Restrict);


        // relacja z użytkownikami
        builder.HasOne<ApplicationUser>(p => p.CreatedByUser)
        .WithMany(u => u.CreatedRecipies)
        .HasForeignKey(p => p.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<ApplicationUser>(p => p.ModifiedByUser)
        .WithMany(u => u.ModifiedRecipies)
        .HasForeignKey(p => p.ModifiedByUserId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
