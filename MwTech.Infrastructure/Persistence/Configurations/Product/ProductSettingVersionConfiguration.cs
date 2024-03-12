
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;


namespace MwTech.Infrastructure.Persistence.Configurations;

public class ProductSettingVersionConfiguration : IEntityTypeConfiguration<ProductSettingVersion>
{
    public void Configure(EntityTypeBuilder<ProductSettingVersion> builder)
    {

        builder.ToTable("ProductSettingVersions");

        // relacja z Produktem
        builder.HasOne<Product>(p => p.Product)
        .WithMany(u => u.ProductSetingVersions)
        .HasForeignKey(p => p.ProductId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z Maszyną
        builder.HasOne(p => p.Machine)
        .WithMany(u => u.ProductSetingVersions)
        .HasForeignKey(p => p.MachineId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z Kategorią Maszyny
        builder.HasOne(p => p.MachineCategory)
        .WithMany(u => u.ProductSetingVersions)
        .HasForeignKey(p => p.MachineCategoryId)
        .OnDelete(DeleteBehavior.Restrict);


        // relacja z Gniazdem
        builder.HasOne(p => p.WorkCenter)
        .WithMany(u => u.ProductSettingVersions)
        .HasForeignKey(p => p.WorkCenterId)
        .OnDelete(DeleteBehavior.Restrict);


        // relacja z użytkownikami
        builder.HasOne(p => p.CreatedByUser)
        .WithMany(u => u.CreatedProductSettingVersions)
        .HasForeignKey(p => p.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Accepted01ByUser)
        .WithMany(u => u.Accepted01ProductSettingVersions)
        .HasForeignKey(p => p.Accepted01ByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Accepted02ByUser)
        .WithMany(u => u.Accepted02ProductSettingVersions)
        .HasForeignKey(p => p.Accepted02ByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Accepted03ByUser)
       .WithMany(u => u.Accepted03ProductSettingVersions)
       .HasForeignKey(p => p.Accepted03ByUserId)
       .OnDelete(DeleteBehavior.Restrict);


        builder
            .Property(x => x.WorkCenterId)
            .HasDefaultValue(1);

    }
}
