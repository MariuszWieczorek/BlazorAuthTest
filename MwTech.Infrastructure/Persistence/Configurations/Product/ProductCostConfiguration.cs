
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;


namespace MwTech.Infrastructure.Persistence.Configurations;

public class ProductCostConfiguration : IEntityTypeConfiguration<ProductCost>
{
    public void Configure(EntityTypeBuilder<ProductCost> builder)
    {
        builder
        .Property(x => x.Cost)
        .HasColumnType("decimal")
        .HasPrecision(12, 5)
        .IsRequired();

        builder
        .Property(x => x.LabourCost)
        .HasColumnType("decimal")
        .HasPrecision(12, 5)
        .IsRequired();

        builder
        .Property(x => x.ProductLabourCost)
        .HasColumnType("decimal")
        .HasPrecision(12, 5)
        .IsRequired();

        builder
        .Property(x => x.MaterialCost)
        .HasColumnType("decimal")
        .HasPrecision(12, 5)
        .IsRequired();

        builder
        .Property(x => x.MarkupCost)
        .HasColumnType("decimal")
        .HasPrecision(12, 5)
        .IsRequired();

        // do celów symulacji

        builder
        .Property(x => x.EstimatedCost)
        .HasColumnType("decimal")
        .HasPrecision(12, 5);


        builder
        .Property(x => x.EstimatedLabourCost)
        .HasColumnType("decimal")
        .HasPrecision(12, 5);


        builder
        .Property(x => x.EstimatedProductLabourCost)
        .HasColumnType("decimal")
        .HasPrecision(12, 5);


        builder
        .Property(x => x.EstimatedMaterialCost)
        .HasColumnType("decimal")
        .HasPrecision(12, 5);


        builder
        .Property(x => x.EstimatedMarkupCost)
        .HasColumnType("decimal")
        .HasPrecision(12, 5);


        // relacja z  produktem
        builder.HasOne<Product>(p => p.Product)
        .WithMany(u => u.ProductCosts)
        .HasForeignKey(p => p.ProductId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z  okresem
        builder.HasOne(p => p.AccountingPeriod)
        .WithMany(u => u.ProductCosts)
        .HasForeignKey(p => p.AccountingPeriodId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z uzytkownikiem
        builder.HasOne(pc => pc.CreatedByUser)
            .WithMany(u => u.CreatedProductCosts)
            .HasForeignKey(pc => pc.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // relacja z uzytkownikiem
        builder.HasOne(pc => pc.ModifiedByUser)
            .WithMany(u => u.ModifiedProductCosts)
            .HasForeignKey(pc => pc.ModifiedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // relacja z walutą
        builder.HasOne(pc => pc.Currency)
            .WithMany(c => c.ProductCosts)
            .HasForeignKey(pc => pc.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
