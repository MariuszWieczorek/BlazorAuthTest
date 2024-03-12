using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities.Comarch;

namespace MwTech.Infrastructure.Persistence.Configurations.Comarch;

public class ComarchTwrCostConfiguration : IEntityTypeConfiguration<ComarchTwrCost>
{
    public void Configure(EntityTypeBuilder<ComarchTwrCost> builder)
    {
        builder
            .HasNoKey()
            .ToView("ComarchTwrCost");

        builder
        .Property(x => x.Cost)
        .HasColumnType("decimal")
        .HasPrecision(12, 5);
    }
}
