using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Products.Queries.GetProductsDataTable;

public class ProductDataTableFilter
{

    public int Id { get; set; }

    [Display(Name = "Nazwa Produktu")]
    public string? Name { get; set; }


    [Display(Name = "Tylko Aktywne")]
    public bool IsActive { get; set; } = true;

    [Display(Name = "Indeks Produktu")]
    public string? ProductNumber { get; set; }

    [Display(Name = "Indeks Komponentu")]
    public string? ComponentProductNumber { get; set; }


[Display(Name = "Kategoria Produktu")]
    public int ProductCategoryId { get; set; }


    [Display(Name = "KT")]
    public int? TechCardNumber { get; set; }
}
