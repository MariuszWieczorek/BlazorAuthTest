
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;


namespace MwTech.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .Property(x => x.Name)
                .IsRequired();

            builder
            .HasIndex(u => u.ProductNumber)
            .IsUnique();

            builder
           .Property(x => x.ContentsOfRubber)
           .HasColumnType("decimal")
           .HasPrecision(12, 5)
           .HasDefaultValue(0);

            builder
          .Property(x => x.Density)
          .HasColumnType("decimal")
          .HasPrecision(12, 5)
          .HasDefaultValue(0);

            builder
            .Property(x => x.WeightTolerance)
            .HasColumnType("decimal")
            .HasPrecision(12, 5);

            builder
                .Property(x => x.Ean13Code)
                .HasMaxLength(13);


            // relacja z jednostką miary
            builder.HasOne<Unit>(p => p.Unit)
            .WithMany(u => u.Products)
            .HasForeignKey(p => p.UnitId)
            .OnDelete(DeleteBehavior.Restrict);


            // relacja z kategorią produktu
            builder.HasOne<ProductCategory>(p => p.ProductCategory)
            .WithMany(u => u.Products)
            .HasForeignKey(p => p.ProductCategoryId)
            .OnDelete(DeleteBehavior.Restrict);


            // relacja z użytkownikami
            builder.HasOne<ApplicationUser>(p => p.CreatedByUser)
            .WithMany(u => u.CreatedProducts)
            .HasForeignKey(p => p.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<ApplicationUser>(p => p.ModifiedByUser)
            .WithMany(u => u.ModifiedProducts)
            .HasForeignKey(p => p.ModifiedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
