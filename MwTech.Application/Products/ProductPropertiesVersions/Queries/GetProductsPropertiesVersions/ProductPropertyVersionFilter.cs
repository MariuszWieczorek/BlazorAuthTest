using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.GetProductsPropertiesVersions.Queries.GetProductsPropertiesVersions;

public class ProductPropertyVersionFilter
{
    [Display(Name = "Indeks Produktu")]
    public string ProductNumber { get; set; }
    
    [Display(Name = "Kategoria Produktu")]
    public int ProductCategoryId { get; set; }
    
    [Display(Name = "aktywne")]
    public bool IsActive { get; set; } = true;

    [Display(Name = "brak 1 akcept")]
    public bool NoFirstAcceptation { get; set; } = true;

    [Display(Name = "brak 2 akcept")]
    public bool NoSecondAcceptation { get; set; } = true;

    [Display(Name = "Nazwa wersji")]
    public string Name { get; set; }

    [Display(Name = "KT")]
    public int? TechCardNumber { get; set; }
}
