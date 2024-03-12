using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Application.CurrencyRates.Queries.GetCurrencyRates;

public class CurrencyRateFilter
{


    [Display(Name = "Okres")]
    public int AccountingPeriodId { get; set; }

    [Display(Name = "Waluta")]
    public int CurrencyId { get; set; }

    [Display(Name = "Kod Waluty")]
    public string? CurrencyCode { get; set; }

}
