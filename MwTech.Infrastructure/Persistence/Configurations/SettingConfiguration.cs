using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;


namespace MwTech.Infrastructure.Persistence.Configurations;
public class SettingConfiguration : IEntityTypeConfiguration<Setting>
{
    public void Configure(EntityTypeBuilder<Setting> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired();

        builder
            .Property(x => x.SettingNumber)
            .IsRequired();

        builder
        .Property(x => x.UnitId)
        .HasDefaultValue(1);

        builder
         .Property(x => x.MachineCategoryId)
         .HasDefaultValue(1);


        builder
                .Property(x => x.Value)
                .HasColumnType("decimal")
                .HasPrecision(12, 5);
                

        builder
                .Property(x => x.MinValue)
                .HasColumnType("decimal")
                .HasPrecision(12, 5);
                

        builder
                .Property(x => x.MaxValue)
                .HasColumnType("decimal")
                .HasPrecision(12, 5);


        // relacja z kategorią maszyn - szablonem
        builder.HasOne<MachineCategory>(p => p.MachineCategory)
        .WithMany(u => u.Settings)
        .HasForeignKey(p => p.MachineCategoryId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z kategorią ustawień
        builder.HasOne<SettingCategory>(p => p.SettingCategory)
        .WithMany(u => u.Settings)
        .HasForeignKey(p => p.SettingCategoryId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z jednostką miary
        builder.HasOne<Unit>(p => p.Unit)
        .WithMany(u => u.Settings)
        .HasForeignKey(p => p.UnitId)
        .OnDelete(DeleteBehavior.Restrict);

    }
}
