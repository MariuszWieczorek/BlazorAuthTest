
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;

namespace MwTech.Infrastructure.Persistence.Configurations;

public class ComparedRecipeIfsVsMwTechConfiguration : IEntityTypeConfiguration<ComparedRecipeIfsVsMwTech>
{
    public void Configure(EntityTypeBuilder<ComparedRecipeIfsVsMwTech> builder)
    {
        builder
            .HasNoKey().ToView("ComparedRecipesIfsVsMwTech");

        

    }
}
