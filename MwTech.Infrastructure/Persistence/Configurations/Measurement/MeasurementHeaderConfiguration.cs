
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Measurements;


namespace MwTech.Infrastructure.Persistence.Configurations;

public class MeasurementHeaderConfiguration : IEntityTypeConfiguration<MeasurementHeader>
{
    public void Configure(EntityTypeBuilder<MeasurementHeader> builder)
    {


        // relacja z produktami
        builder.HasOne<Product>(p => p.Product)
        .WithMany(u => u.MeasurementHeaders)
        .HasForeignKey(p => p.ProductId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z użytkownikami
        builder.HasOne<ApplicationUser>(p => p.CreatedByUser)
        .WithMany(u => u.CreatedMeasurementHeaders)
        .HasForeignKey(p => p.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<ApplicationUser>(p => p.ModifiedByUser)
        .WithMany(u => u.ModifiedMeasurementHeaders)
        .HasForeignKey(p => p.ModifiedByUserId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
