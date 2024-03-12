
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;


namespace MwTech.Infrastructure.Persistence.Configurations;

public class ManufactoringRouteConfiguration : IEntityTypeConfiguration<ManufactoringRoute>
{
    public void Configure(EntityTypeBuilder<ManufactoringRoute> builder)
    {
        builder
            .Property(x => x.RouteVersionId)
            .IsRequired();

        builder
            .Property(x => x.OperationId)
            .IsRequired();

        builder
            .Property(x => x.WorkCenterId)
            .HasDefaultValue(1)
            .IsRequired();


        builder
           .Property(x => x.OperationLabourConsumption)
           .HasColumnType("decimal")
           .HasPrecision(12, 6)
           .HasDefaultValue(0)
           .IsRequired();

        builder
           .Property(x => x.OperationMachineConsumption)
           .HasColumnType("decimal")
           .HasPrecision(12, 6)
           .HasDefaultValue(0)
           .IsRequired();

        builder
            .Property(x => x.ResourceQty)
            .HasColumnType("decimal")
            .HasPrecision(12, 3)
            .HasDefaultValue(1)
            .IsRequired();


        builder
           .Property(x => x.ChangeOverLabourConsumption)
           .HasColumnType("decimal")
           .HasPrecision(12, 6)
           .HasDefaultValue(0)
           .IsRequired();

        builder
           .Property(x => x.ChangeOverMachineConsumption)
           .HasColumnType("decimal")
           .HasPrecision(12, 6)
           .HasDefaultValue(0)
           .IsRequired();

        builder
           .Property(x => x.Overlap)
           .HasColumnType("decimal")
           .HasPrecision(12, 6)
           .HasDefaultValue(0)
           .IsRequired();

        builder
           .Property(x => x.MoveTime)
           .HasColumnType("decimal")
           .HasPrecision(12, 6)
           .HasDefaultValue(0)
           .IsRequired();

        builder
            .Property(x => x.ChangeOverNumberOfEmployee)
            .HasColumnType("decimal")
            .HasPrecision(12, 2)
            .HasDefaultValue(1)
            .IsRequired();

        // relacja z wersją marszruty
        // pozwala usunąć nagłówek wraz z marszrutą


        builder.HasOne<RouteVersion>(p => p.RouteVersion)
        .WithMany(u => u.ManufactoringRoutes)
        .HasForeignKey(p => p.RouteVersionId)
        .OnDelete(DeleteBehavior.Cascade);


        // relacja z operacjami
        builder.HasOne<Operation>(p => p.Operation)
        .WithMany(u => u.ManufactoringRoutes)
        .HasForeignKey(p => p.OperationId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z zasobami - pracownik operacja
        builder.HasOne<Resource>(p => p.Resource)
        .WithMany(u => u.ManufactoringRoutes)
        .HasForeignKey(p => p.ResourceId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z zasobami - gniazda 
        builder.HasOne<Resource>(p => p.WorkCenter)
        .WithMany(u => u.ManufactoringWorkCenters)
        .HasForeignKey(p => p.WorkCenterId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z zasobami - pracownik - przezbrojenia
        builder.HasOne<Resource>(p => p.ChangeOverResource)
        .WithMany(u => u.ManufactoringChangeOvers)
        .HasForeignKey(p => p.ChangeOverResourceId)
        .OnDelete(DeleteBehavior.Restrict);


        // relacja z kategorią produktu
        builder.HasOne<ProductCategory>(p => p.ProductCategory)
        .WithMany(u => u.ManufactoringRoutes)
        .HasForeignKey(p => p.ProductCategoryId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z narzędziem
        builder.HasOne<RoutingTool>(p => p.RoutingTool)
        .WithMany(u => u.ManufactoringRoutes)
        .HasForeignKey(p => p.RoutingToolId)
        .OnDelete(DeleteBehavior.Restrict);

    }
}
