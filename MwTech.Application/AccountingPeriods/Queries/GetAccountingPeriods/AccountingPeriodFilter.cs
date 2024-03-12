using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Application.AccountingPeriods.Queries.GetAccountingPeriods;

public class AccountingPeriodFilter
{
    [Display(Name = "Nazwa")]
    public string? Name { get; set; }


    [Display(Name = "Symbol")]
    public string? PeriodNumber { get; set; }

}
