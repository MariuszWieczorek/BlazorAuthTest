
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MwTech.Domain.Entities;


namespace MwTech.Infrastructure.Persistence.Configurations;

public class CurrencyRateConfiguration : IEntityTypeConfiguration<CurrencyRate>
{
    public void Configure(EntityTypeBuilder<CurrencyRate> builder)
    {
      
        builder
           .Property(x => x.Rate)
           .HasColumnType("decimal")
           .HasPrecision(12, 4)
           .IsRequired();

        builder
         .Property(x => x.EstimatedRate)
         .HasColumnType("decimal")
         .HasPrecision(12, 4);
         

        // relacja z Currency
        builder.HasOne<Currency>(p => p.FromCurrency)
        .WithMany(u => u.CurrencyRates)
        .HasForeignKey(p => p.FromCurrencyId)
        .OnDelete(DeleteBehavior.Restrict);

        // relacja z Period
        builder.HasOne<AccountingPeriod>(p => p.AccountingPeriod)
        .WithMany(u => u.CurrencyRates)
        .HasForeignKey(p => p.AccountingPeriodId)
        .OnDelete(DeleteBehavior.Restrict);

    }
}
