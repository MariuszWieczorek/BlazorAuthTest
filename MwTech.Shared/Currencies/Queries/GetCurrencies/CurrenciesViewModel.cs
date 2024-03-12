using MwTech.Domain.Entities;
using MwTech.Shared.Common.Models;

namespace MwTech.Shared.Currencies.Dtos;

public class CurrenciesViewModel
{
    public  IEnumerable<Currency> Currencies { get; set; }

    public  PaginatedList<Currency> Currencies2 { get; set; }

    public CurrencyFilter CurrencyFilter { get; set; }

}
