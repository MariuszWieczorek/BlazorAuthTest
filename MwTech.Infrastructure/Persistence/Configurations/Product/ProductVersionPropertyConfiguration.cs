
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;


namespace MwTech.Infrastructure.Persistence.Configurations
{
    public class ProductVersionPropertyConfiguration : IEntityTypeConfiguration<ProductVersionProperty>
    {
        public void Configure(EntityTypeBuilder<ProductVersionProperty> builder)
        {

            builder
             .Property(x => x.Value)
             .HasColumnType("decimal")
             .HasPrecision(16, 6);


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
            .WithMany(u => u.ProductVersionProperties)
            .HasForeignKey(p => p.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);

            // relacja z ProductVersion
            // pozwalam usunąć nagłówek wraz z atrybutami
            builder.HasOne(p => p.ProductVersion)
            .WithMany(u => u.ProductVersionProperties)
            .HasForeignKey(p => p.ProductVersionId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
