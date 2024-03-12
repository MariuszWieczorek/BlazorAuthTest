
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;


namespace MwTech.Infrastructure.Persistence.Configurations;

public class ProductPropertyVersionConfiguration : IEntityTypeConfiguration<ProductPropertyVersion>
{
    public void Configure(EntityTypeBuilder<ProductPropertyVersion> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired();

        builder
            .Property(x => x.IsActive);

        // relacja z produktem
        builder.HasOne<Product>(p => p.Product)
        .WithMany(u => u.ProductPropertyVersions)
        .HasForeignKey(p => p.ProductId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z użytkownikami
        builder.HasOne(p => p.CreatedByUser)
        .WithMany(u => u.CreatedProductPropertyVersions)
        .HasForeignKey(p => p.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.ModifiedByUser)
        .WithMany(u => u.ModifiedProductPropertyVersions)
        .HasForeignKey(p => p.ModifiedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Accepted01ByUser)
        .WithMany(u => u.Accepted01ProductPropertyVersions)
        .HasForeignKey(p => p.Accepted01ByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Accepted02ByUser)
        .WithMany(u => u.Accepted02ProductPropertyVersions)
        .HasForeignKey(p => p.Accepted02ByUserId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
