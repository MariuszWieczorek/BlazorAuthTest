using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class ShortCurrencyRate
{
    public int? Id { get; set; }

    [Display(Name = "Okres")]
    public int AccountingPeriodId { get; set; }

    [Display(Name = "Okres")]
    public AccountingPeriod? AccountingPeriod { get; set; }

    [Display(Name = "Waluta")]
    public int FromCurrencyId { get; set; }

    [Display(Name = "Waluta")]
    public Currency? FromCurrency { get; set; }

    [Display(Name = "Kurs")]
    public decimal? Rate { get; set; }

    [Display(Name = "Kurs")]
    public decimal? EstimatedRate { get; set; }

}
