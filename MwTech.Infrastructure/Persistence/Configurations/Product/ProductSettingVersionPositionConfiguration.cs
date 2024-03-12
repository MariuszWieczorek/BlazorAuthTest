
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;


namespace MwTech.Infrastructure.Persistence.Configurations
{
    public class ProductSettingVersionPositionConfiguration : IEntityTypeConfiguration<ProductSettingVersionPosition>
    {
        public void Configure(EntityTypeBuilder<ProductSettingVersionPosition> builder)
        {

            builder.ToTable("ProductSettingVersionPositions");

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

            // relacja z wersją ustawień
            // pozwalam usunąć pozycje usuwając nagłówek
            builder.HasOne(p => p.ProductSettingVersion)
            .WithMany(u => u.ProductSettingVersionPositions)
            .HasForeignKey(p => p.ProductSettingVersionId)
            .OnDelete(DeleteBehavior.Cascade);

            // relacja z ustawieniem
            builder.HasOne(p => p.Setting)
            .WithMany(u => u.ProductSettingVersionPositions)
            .HasForeignKey(p => p.SettingId)
            .OnDelete(DeleteBehavior.Restrict);

            // relacja z użytkownikami
            builder.HasOne(p => p.ModifiedByUser)
            .WithMany(u => u.ModifiedProductSettingVersionsPositions)
            .HasForeignKey(p => p.ModifiedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
