using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class CurrencyRate
{
    public int Id { get; set; }
    public int AccountingPeriodId { get; set; }
    public AccountingPeriod? AccountingPeriod { get; set; }
    public int FromCurrencyId { get; set; }
    public Currency? FromCurrency { get; set; }
    public decimal Rate { get; set; }
    public decimal EstimatedRate { get; set; }

}
