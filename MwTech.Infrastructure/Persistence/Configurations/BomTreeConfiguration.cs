
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;

namespace MwTech.Infrastructure.Persistence.Configurations;

public class BomTreeConfiguration : IEntityTypeConfiguration<BomTree>
{
    public void Configure(EntityTypeBuilder<BomTree> builder)
    {
        builder
            .HasNoKey().ToView("BomTrees");

        builder
           .Property(x => x.PartProductQty)
           .HasColumnType("decimal")
           .HasPrecision(12, 5)
           .IsRequired();

        builder
           .Property(x => x.FinalPartProductQty)
           .HasColumnType("decimal")
           .HasPrecision(12, 5)
           .IsRequired();

        builder
           .Property(x => x.FinalPartProductWeight)
           .HasColumnType("decimal")
           .HasPrecision(12, 5)
           .IsRequired();

        builder
           .Property(x => x.SetProductQty)
           .HasColumnType("decimal")
           .HasPrecision(12, 5)
           .IsRequired();

        builder
           .Property(x => x.PartContentsOfRubber)
           .HasColumnType("decimal")
           .HasPrecision(12, 5)
           .IsRequired();

        builder
           .Property(x => x.PartDensity)
           .HasColumnType("decimal")
           .HasPrecision(12, 5)
           .IsRequired();

    }
}
