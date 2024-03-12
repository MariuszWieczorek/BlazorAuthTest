
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;


namespace MwTech.Infrastructure.Persistence.Configurations;

public class ScadaReportConfiguration : IEntityTypeConfiguration<ScadaReport>
{
    public void Configure(EntityTypeBuilder<ScadaReport> builder)
    {

        builder
            .HasKey(x => x.SEQ_ID);

        builder
           .Property(x => x.QTY_ISSUED)
           .HasColumnType("decimal")
           .HasPrecision(10, 3);

        builder
           .Property(x => x.QTY_REPORTED)
           .HasColumnType("decimal")
           .HasPrecision(10, 3);

    }
}
