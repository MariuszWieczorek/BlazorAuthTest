
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;


namespace MwTech.Infrastructure.Persistence.Configurations
{
    public class ProductVersionConfiguration : IEntityTypeConfiguration<ProductVersion>
    {
        public void Configure(EntityTypeBuilder<ProductVersion> builder)
        {
            builder
                .Property(x => x.Name)
                .IsRequired();

            builder
               .Property(x => x.ProductQty)
               .HasColumnType("decimal")
               .HasPrecision(12, 6)
               .IsRequired();

            builder
               .Property(x => x.ProductWeight)
               .HasColumnType("decimal")
               .HasPrecision(12, 6)
               .IsRequired();

            // relacja z produktem
            builder.HasOne<Product>(p => p.Product)
            .WithMany(u => u.ProductVersions)
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

            // relacja z użytkownikami
            builder.HasOne(p => p.CreatedByUser)
            .WithMany(u => u.CreatedProductVersions)
            .HasForeignKey(p => p.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.ModifiedByUser)
            .WithMany(u => u.ModifiedProductVersions)
            .HasForeignKey(p => p.ModifiedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Accepted01ByUser)
            .WithMany(u => u.Accepted01ProductVersions)
            .HasForeignKey(p => p.Accepted01ByUserId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Accepted02ByUser)
            .WithMany(u => u.Accepted02ProductVersions)
            .HasForeignKey(p => p.Accepted02ByUserId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
