
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;


namespace MwTech.Infrastructure.Persistence.Configurations;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired();

        builder
                .Property(x => x.ResourceNumber)
                .HasMaxLength(50);

        builder
        .Property(x => x.Contract)
        .HasMaxLength(5 );

        builder
        .Property(x => x.LaborClassNo)
        .HasMaxLength(10);

        builder.Property(x => x.ProductCategoryId)
            .HasDefaultValue(1);

        builder.Property(x => x.UnitId)
        .HasDefaultValue(1);


        builder
           .HasIndex(u => u.ResourceNumber)
           .IsClustered(false)
           .IsUnique();

        builder
            .Property(x => x.Cost)
            .HasColumnType("decimal")
            .HasPrecision(12, 5)
            .IsRequired();

        builder
            .Property(x => x.Markup)
            .HasColumnType("decimal")
            .HasPrecision(12, 5);
        


        builder
            .Property(x => x.EstimatedCost)
            .HasColumnType("decimal")
            .HasPrecision(12, 5);

        builder
            .Property(x => x.EstimatedMarkup)
            .HasColumnType("decimal")
            .HasPrecision(12, 5);



        // relacja z jednostką miary
        builder.HasOne<Unit>(p => p.Unit)
        .WithMany(u => u.Resources)
        .HasForeignKey(p => p.UnitId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z kategorią produktu
        builder.HasOne<ProductCategory>(p => p.ProductCategory)
        .WithMany(u => u.Resources)
        .HasForeignKey(p => p.ProductCategoryId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja zasobu workcenter z zasobem LabourClass
         builder.HasOne<Resource>(p => p.LabourClass)
        .WithMany(u => u.WorkCenters)
        .HasForeignKey(p => p.LabourClassId)
        .OnDelete(DeleteBehavior.Restrict);

    }
}
