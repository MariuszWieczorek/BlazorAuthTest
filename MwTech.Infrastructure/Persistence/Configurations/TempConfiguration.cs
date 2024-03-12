
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;


namespace MwTech.Infrastructure.Persistence.Configurations;

public class TempConfiguration : IEntityTypeConfiguration<Temp>
{
    public void Configure(EntityTypeBuilder<Temp> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired();

               
        builder
        .HasIndex(x => new { x.Idx01, x.Idx02 })
        .IsUnique();


    }
}


/*
  CREATE UNIQUE NONCLUSTERED INDEX idx_idx02_idx03_notnull
   ON dbo.Temps(idx02,idx03)
   WHERE idx01 IS NOT NULL and idx02 is not null;
 */