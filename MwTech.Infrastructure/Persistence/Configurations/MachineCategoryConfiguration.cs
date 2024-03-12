
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;


namespace MwTech.Infrastructure.Persistence.Configurations;

public class MachineCategoryConfiguration : IEntityTypeConfiguration<MachineCategory>
{
    public void Configure(EntityTypeBuilder<MachineCategory> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired();

        
        // relacja z kategorią produktu
        builder.HasOne<ProductCategory>(p => p.ProductCategory)
        .WithMany(u => u.MachineCategories)
        .HasForeignKey(p => p.ProductCategoryId)
        .OnDelete(DeleteBehavior.Restrict);

    }
}
