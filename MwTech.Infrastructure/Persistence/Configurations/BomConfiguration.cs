
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;

namespace MwTech.Infrastructure.Persistence.Configurations
{
    public class BomConfiguration : IEntityTypeConfiguration<Bom>
    {
        public void Configure(EntityTypeBuilder<Bom> builder)
        {
            builder
                .Property(x => x.SetId)
                .IsRequired();

            builder
                .Property(x => x.PartId)
                .IsRequired();

            builder
                .Property(x => x.PartQty)
                .HasColumnType("decimal")
                .HasPrecision(12, 5)
                .IsRequired();

            builder
           .Property(x => x.Excess)
           .HasColumnType("decimal")
           .HasPrecision(12, 5)
           .IsRequired();


            // relacja z wersją produktu
            // mogę usunąć wersję produktu wraz z ustawieniami
            builder.HasOne<ProductVersion>(p => p.SetVersion)
            .WithMany(u => u.BomSetVersions)
            .HasForeignKey(p => p.SetVersionId)
            .OnDelete(DeleteBehavior.Cascade);

            // relacja z wersją produktu dla zestawu
            builder.HasOne<Product>(p => p.Set)
            .WithMany(u => u.BomSets)
            .HasForeignKey(p => p.SetId)
            .OnDelete(DeleteBehavior.Restrict);


            // relacja z wersją produktu dla części
            builder.HasOne<Product>(p => p.Part)
            .WithMany(u => u.BomParts)
            .HasForeignKey(p => p.PartId)
            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
