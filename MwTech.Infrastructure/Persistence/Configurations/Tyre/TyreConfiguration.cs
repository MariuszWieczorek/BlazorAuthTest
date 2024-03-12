
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Tyres;


namespace MwTech.Infrastructure.Persistence.Configurations;

public class TyreConfiguration : IEntityTypeConfiguration<Tyre>
{
    public void Configure(EntityTypeBuilder<Tyre> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired();

        builder
        .HasIndex(u => u.TyreNumber)
        .IsUnique();




        // relacja z użytkownikami
        builder.HasOne<ApplicationUser>(p => p.CreatedByUser)
        .WithMany(u => u.CreatedTyres)
        .HasForeignKey(p => p.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<ApplicationUser>(p => p.ModifiedByUser)
        .WithMany(u => u.ModifiedTyres)
        .HasForeignKey(p => p.ModifiedByUserId)
        .OnDelete(DeleteBehavior.Restrict);
        
    }
}
