using MwTech.Domain.Entities;
namespace MwTech.Application.CurrencyRates.Queries.GetCurrencyRates;

public class CurrencyRatesViewModel
{
    public IEnumerable<ShortCurrencyRate> CurrencyRates { get; set; }
    public CurrencyRateFilter CurrencyRateFilter { get; set; }
    public IEnumerable<AccountingPeriod> AccountingPeriods { get; set; }
    public IEnumerable<Currency> Currencies { get; set; }

}
