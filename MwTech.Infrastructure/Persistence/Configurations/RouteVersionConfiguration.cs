
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;


namespace MwTech.Infrastructure.Persistence.Configurations;

public class RouteVersionConfiguration : IEntityTypeConfiguration<RouteVersion>
{
    public void Configure(EntityTypeBuilder<RouteVersion> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired();



        builder
           .Property(x => x.ProductQty)
           .HasColumnType("decimal")
           .HasPrecision(12, 2)
           .IsRequired();


        builder
           .HasIndex(u => u.VersionNumber)
           .IsClustered(false);

        builder
           .HasIndex(u => u.AlternativeNo)
           .IsClustered(false);

        builder
            .Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder
            .Property(x => x.ToIfs)
            .HasDefaultValue(true);

        // relacja z produktem
        builder.HasOne<Product>(p => p.Product)
        .WithMany(u => u.RouteVersions)
        .HasForeignKey(p => p.ProductId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z użytkownikami
        builder.HasOne<ApplicationUser>(p => p.CreatedByUser)
        .WithMany(u => u.CreatedRouteVersions)
        .HasForeignKey(p => p.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<ApplicationUser>(p => p.ModifiedByUser)
        .WithMany(u => u.ModifiedRouteVersions)
        .HasForeignKey(p => p.ModifiedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<ApplicationUser>(p => p.Accepted01ByUser)
        .WithMany(u => u.Accepted01RouteVersions)
        .HasForeignKey(p => p.Accepted01ByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<ApplicationUser>(p => p.Accepted02ByUser)
        .WithMany(u => u.Accepted02RouteVersions)
        .HasForeignKey(p => p.Accepted02ByUserId)
        .OnDelete(DeleteBehavior.Restrict);


        // relacja z kategorią produktu
        builder.HasOne<ProductCategory>(p => p.ProductCategory)
        .WithMany(u => u.RouteVersions)
        .HasForeignKey(p => p.ProductCategoryId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
