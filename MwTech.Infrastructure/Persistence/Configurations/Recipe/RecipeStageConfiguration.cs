
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Infrastructure.Persistence.Configurations;

public class RecipeStageConfiguration : IEntityTypeConfiguration<RecipeStage>
{
    public void Configure(EntityTypeBuilder<RecipeStage> builder)
    {
        builder
            .Property(x => x.StageNo)
            .IsRequired();


        builder
            .Property(x => x.MixerVolume)
            .HasColumnType("decimal")
            .HasPrecision(12, 2)
            .IsRequired();

        builder
            .Property(x => x.DivideQtyBy)
            .HasColumnType("decimal")
            .HasPrecision(12, 5);

        builder
            .Property(x => x.MultiplyQtyBy)
            .HasColumnType("decimal")
            .HasPrecision(12, 5);

        builder
            .Property(x => x.PrevStageQty)
            .HasColumnType("decimal")
            .HasPrecision(12, 5);

        builder
            .Property(x => x.CrewSize)
            .HasColumnType("decimal")
            .HasPrecision(12, 5);

        builder
            .Property(x => x.LabourRunFactor)
            .HasColumnType("decimal")
            .HasPrecision(12, 5);


        builder
            .Property(x => x.ProductName)
            .HasMaxLength(100);

        builder
            .Property(x => x.ProductNumber)
            .HasMaxLength(35);

        // relacja z wersją produktu
        builder.HasOne<RecipeVersion>(p => p.RecipeVersion)
        .WithMany(u => u.RecipeStages)
        .HasForeignKey(p => p.RecipeVersionId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z gniazdem roboczym
        builder.HasOne<Resource>(p => p.WorkCenter)
        .WithMany(u => u.RecipeStagesWorkCenters)
        .HasForeignKey(p => p.WorkCenterId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z gniazdem roboczym
        builder.HasOne<Resource>(p => p.LabourClass)
        .WithMany(u => u.RecipeLabourClasses)
        .HasForeignKey(p => p.LabourClassId)
        .OnDelete(DeleteBehavior.Restrict);


    }
}
