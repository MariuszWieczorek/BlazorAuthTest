
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities.Ifs;

namespace MwTech.Infrastructure.Persistence.Configurations.Ifs;

public class IfsProductStructureConfiguration : IEntityTypeConfiguration<IfsProductStructure>
{
    public void Configure(EntityTypeBuilder<IfsProductStructure> builder)
    {


        builder
            .Property(x => x.PartNo)
            .HasMaxLength(25)
            .IsRequired();

        builder
            .Property(x => x.AlternativeNo)
            .HasMaxLength(10)
            .IsRequired();
        
        builder
            .Property(x => x.AlternativeState)
            .HasMaxLength(50);

        builder
            .Property(x => x.AlternativeDescription)
            .HasMaxLength(100);

        builder
            .Property(x => x.RevisionNo)
            .HasMaxLength(10)
            .IsRequired();

        builder
            .Property(x => x.RevisionName)
            .HasMaxLength(100);

        builder
            .Property(x => x.ComponentPart)
            .HasMaxLength(25)
            .IsRequired();

        builder
            .Property(x => x.PartStatus)
            .HasMaxLength(50);
        
        builder
            .Property(x => x.PrintUnit)
            .HasMaxLength(50);

        builder
            .Property(x => x.ConsumptionItemDb)
            .HasMaxLength(50);

        builder
            .Property(x => x.QtyPerAssembly)
            .HasColumnType("decimal")
            .HasPrecision(12, 5)
            .IsRequired();

        builder
            .Property(x => x.ShrinkageFactor)
            .HasColumnType("decimal")
            .HasPrecision(12, 5);

        builder
            .Property(x => x.ComponentScrap)
            .HasColumnType("decimal")
            .HasPrecision(12, 5);


    }
}
