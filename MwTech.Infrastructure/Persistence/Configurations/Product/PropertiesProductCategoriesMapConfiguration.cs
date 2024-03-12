
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;


namespace MwTech.Infrastructure.Persistence.Configurations;

public class PropertiesProductCategoriesMapConfiguration : IEntityTypeConfiguration<PropertiesProductCategoriesMap>
{
    public void Configure(EntityTypeBuilder<PropertiesProductCategoriesMap> builder)
    {
        builder
            .ToTable("PropertiesProductCategoriesMaps");
    }
}
