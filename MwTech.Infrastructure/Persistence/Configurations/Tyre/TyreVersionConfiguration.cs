
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Tyres;

namespace MwTech.Infrastructure.Persistence.Configurations;

public class TyreVersionConfiguration : IEntityTypeConfiguration<TyreVersion>
{
    public void Configure(EntityTypeBuilder<TyreVersion> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired();

        // relacja z produktem
        builder.HasOne<Tyre>(p => p.Tyre)
        .WithMany(u => u.TyreVersions)
        .HasForeignKey(p => p.TyreId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z użytkownikami
        builder.HasOne<ApplicationUser>(p => p.CreatedByUser)
        .WithMany(u => u.CreatedTyreVersions)
        .HasForeignKey(p => p.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<ApplicationUser>(p => p.ModifiedByUser)
        .WithMany(u => u.ModifiedTyreVersions)
        .HasForeignKey(p => p.ModifiedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<ApplicationUser>(p => p.Accepted01ByUser)
        .WithMany(u => u.Accepted01TyreVersions)
        .HasForeignKey(p => p.Accepted01ByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<ApplicationUser>(p => p.Accepted02ByUser)
        .WithMany(u => u.Accepted02TyreVersions)
        .HasForeignKey(p => p.Accepted02ByUserId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
