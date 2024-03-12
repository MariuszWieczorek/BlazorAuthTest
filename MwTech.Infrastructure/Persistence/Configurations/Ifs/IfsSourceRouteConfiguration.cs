
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities.Ifs;

namespace MwTech.Infrastructure.Persistence.Configurations.Ifs;

public class IfsSourceRouteConfiguration : IEntityTypeConfiguration<IfsSourceRoute>
{
    public void Configure(EntityTypeBuilder<IfsSourceRoute> builder)
    {

        builder
            .HasNoKey()
            .ToView("IfsSourceRoutes");

        builder
            .Property(x => x.PartNo)
            .HasMaxLength(25);

        builder
            .Property(x => x.AlternativeNo)
            .HasMaxLength(10);

        builder
            .Property(x => x.RevisionNo)
            .HasMaxLength(10);


        builder
            .Property(x => x.OperationNo)
            .HasMaxLength(10);

        builder
            .Property(x => x.WorkCenterNo)
            .HasMaxLength(10);

        builder
            .Property(x => x.LaborClassNo)
            .HasMaxLength(10);

        builder
            .Property(x => x.RunTimeCode)
            .HasMaxLength(25);

        builder
            .Property(x => x.OperationDescription)
            .HasMaxLength(255);

        builder
            .Property(x => x.AlternativeDescription)
            .HasMaxLength(255);

        builder
            .Property(x => x.SetupLaborClassNo)
            .HasMaxLength(25);

        builder
            .Property(x => x.MachRunFactor)
            .HasColumnType("decimal")
            .HasPrecision(12, 5);

        builder
            .Property(x => x.LaborRunFactor)
            .HasColumnType("decimal")
            .HasPrecision(12, 5);

        builder
            .Property(x => x.CrewSize)
            .HasColumnType("decimal")
            .HasPrecision(12, 5);

        builder
            .Property(x => x.MachSetupTime)
            .HasColumnType("decimal")
            .HasPrecision(12, 5);

        builder
            .Property(x => x.LaborSetupTime)
            .HasColumnType("decimal")
            .HasPrecision(12, 5);

        builder
            .Property(x => x.SetupCrewSize)
            .HasColumnType("decimal")
            .HasPrecision(12, 5);

        builder
            .Property(x => x.MoveTime)
            .HasColumnType("decimal")
            .HasPrecision(12, 5);

        builder
            .Property(x => x.Overlap)
            .HasColumnType("decimal")
            .HasPrecision(12, 5);


    }
}
