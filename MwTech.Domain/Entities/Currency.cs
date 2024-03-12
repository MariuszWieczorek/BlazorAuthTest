using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class Currency
{
    public int Id { get; set; }

    [Display(Name = "Kod")]
    public string CurrencyCode { get; set; }

    [Display(Name = "Nazwa")]
    public string Name { get; set; }

    public ICollection<ProductCost> ProductCosts { get; set; } = new HashSet<ProductCost>();
    public ICollection<CurrencyRate> CurrencyRates { get; set; } = new HashSet<CurrencyRate>();

}
