using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;


namespace MwTech.Infrastructure.Persistence.Configurations;
public class SettingCategoryConfiguration : IEntityTypeConfiguration<SettingCategory>
{
    public void Configure(EntityTypeBuilder<SettingCategory> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired();

        builder
            .Property(x => x.SettingCategoryNumber)
            .IsRequired();


        // relacja z kategorią maszyny
        builder.HasOne<MachineCategory>(p => p.MachineCategory)
        .WithMany(u => u.SettingCategories)
        .HasForeignKey(p => p.MachineCategoryId)
        .OnDelete(DeleteBehavior.Restrict);

    }
}
