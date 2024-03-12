using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class AccountingPeriod
{
    public int Id { get; set; }


    [Display(Name = "Symbol")]
    public string PeriodNumber { get; set; }

    [Display(Name = "Nazwa")]
    public string Name { get; set; }


    [Display(Name = "Data Od")]
    public DateTime StartDate { get; set; }


    [Display(Name = "Data Do")]
    public DateTime EndDate { get; set; }

    [Display(Name = "Aktywny")]
    public bool IsActive { get; set; }

    [Display(Name = "Domyślny")]
    public bool IsDefault { get; set; }

    [Display(Name = "Zamknięty")]
    public bool IsClosed { get; set; }

    [Display(Name = "Opis")]
    public string? Description { get; set; }

    public ICollection<ProductCost> ProductCosts { get; set; } = new HashSet<ProductCost>();
    public ICollection<CurrencyRate> CurrencyRates { get; set; } = new HashSet<CurrencyRate>();
}
