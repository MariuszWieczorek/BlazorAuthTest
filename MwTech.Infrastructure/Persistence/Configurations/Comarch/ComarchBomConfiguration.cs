
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities.Comarch;

namespace MwTech.Infrastructure.Persistence.Configurations.Comarch;

public class ComarchBomConfiguration : IEntityTypeConfiguration<ComarchBom>
{
    public void Configure(EntityTypeBuilder<ComarchBom> builder)
    {

        builder
            .HasNoKey()
            .ToView("ComarchBoms");

        builder
        .Property(x => x.waga_netto)
        .HasColumnType("decimal")
        .HasPrecision(12, 5);


        builder
        .Property(x => x.ilosc_ewidencyjna)
        .HasColumnType("decimal")
        .HasPrecision(12, 5);


        builder
        .Property(x => x.ilosc_skladnika)
        .HasColumnType("decimal")
        .HasPrecision(12, 5);

        builder
        .Property(x => x.waga_brutto)
        .HasColumnType("decimal")
        .HasPrecision(12, 5);

        builder
        .Property(x => x.koszt_ewidencyjny)
        .HasColumnType("decimal")
        .HasPrecision(12, 5);



    }
}
