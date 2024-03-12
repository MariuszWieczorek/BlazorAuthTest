
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;


namespace MwTech.Infrastructure.Persistence.Configurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired();



        // relacja z kategorią produktu
        builder.HasOne<ProductCategory>(p => p.ProductCategory)
        .WithMany(u => u.Properties)
        .HasForeignKey(p => p.ProductCategoryId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z kategorią produktu
        builder.HasOne<Unit>(p => p.Unit)
        .WithMany(u => u.Properties)
        .HasForeignKey(p => p.UnitId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.ProductCategories2)
            .WithMany(x => x.Properties2)
            .UsingEntity<PropertiesProductCategoriesMap>(
                x => x.HasOne(x => x.ProductCategory).WithMany().HasForeignKey(x => x.ProductCategoryId),
                x => x.HasOne(x => x.Property).WithMany().HasForeignKey(x => x.PropertyId)
                );



    }
}
