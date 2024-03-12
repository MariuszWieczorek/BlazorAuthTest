
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;


namespace MwTech.Infrastructure.Persistence.Configurations;

public class ProductPropertyConfiguration : IEntityTypeConfiguration<ProductProperty>
{
    public void Configure(EntityTypeBuilder<ProductProperty> builder)
    {

        builder
        .Property(x => x.ProductPropertiesVersionId)
        .HasDefaultValue(1);

        builder
        .Property(x => x.Value)
        .HasColumnType("decimal")
        .HasPrecision(12, 5);



        builder
        .Property(x => x.MinValue)
        .HasColumnType("decimal")
        .HasPrecision(12, 5);



        builder
        .Property(x => x.MaxValue)
        .HasColumnType("decimal")
        .HasPrecision(12, 5);


        // relacja z Property
        builder.HasOne(p => p.Property)
        .WithMany(u => u.ProductProperties)
        .HasForeignKey(p => p.PropertyId)
        .OnDelete(DeleteBehavior.Restrict);


        // relacja z wersją atrybutów
        // pozwalam usunąć pozycje usuwając nagłówek
        builder.HasOne(p => p.ProductPropertiesVersion)
        .WithMany(u => u.ProductProperties)
        .HasForeignKey(p => p.ProductPropertiesVersionId)
        .OnDelete(DeleteBehavior.Cascade);

    }
}
