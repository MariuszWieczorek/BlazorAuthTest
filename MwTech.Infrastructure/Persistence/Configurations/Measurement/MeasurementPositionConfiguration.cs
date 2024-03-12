
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities.Measurements;

namespace MwTech.Infrastructure.Persistence.Configurations;

public class MeasurementPositionConfiguration : IEntityTypeConfiguration<MeasurementPosition>
{
    public void Configure(EntityTypeBuilder<MeasurementPosition> builder)
    {

        builder
            .Property(x => x.Value)
            .HasColumnType("decimal")
            .HasPrecision(12, 5)
            .IsRequired();


        // relacja z nagłówkiem
        builder.HasOne<MeasurementHeader>(p => p.MeasurementHeader)
        .WithMany(u => u.MeasurementPositions)
        .HasForeignKey(p => p.MeasurementHeaderId)
        .OnDelete(DeleteBehavior.Restrict);

    }
}
