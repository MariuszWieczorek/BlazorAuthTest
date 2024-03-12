using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductCosts.Queries.GetProductCosts;

public class ProductCostFilter
{


    [Display(Name = "Okres")]
    public int AccountingPeriodId { get; set; }

    [Display(Name = "Kategoria Produktu")]
    public int ProductCategoryId { get; set; }

    [Display(Name = "Waluta")]
    public int CurrencyId { get; set; }

    [Display(Name = "Indeks Produktu")]
    public string? ProductNumber { get; set; }

    public bool ShowNoSavedPositions { get; set; } 

}
