
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities.Ifs;

namespace MwTech.Infrastructure.Persistence.Configurations.Ifs;

public class IfsSourceProductRecipeConfiguration : IEntityTypeConfiguration<IfsSourceProductRecipe>
{
    public void Configure(EntityTypeBuilder<IfsSourceProductRecipe> builder)
    {

        builder
            .HasNoKey()
            .ToView("IfsSourceProductRecipes");

    }
}
