using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Common;

public class ProductFilter
{

    public int Id { get; set; }

    [Display(Name = "Nazwa Produktu")]
    public string? Name { get; set; }


    [Display(Name = "Aktywne")]
    public int IsActive { get; set; } = 1;

    [Display(Name = "Test")]
    public int IsTest { get; set; } = 0;

    [Display(Name = "Indeks Produktu")]
    public string? ProductNumber { get; set; }

    [Display(Name = "Indeks Komponentu")]
    public string? ComponentProductNumber { get; set; }


[Display(Name = "Kategoria Produktu")]
    public int ProductCategoryId { get; set; }


    [Display(Name = "KT")]
    public int? TechCardNumber { get; set; }


    [Display(Name = "KT Komponentu")]
    public int? ComponentTechCardNumber { get; set; }

    [Display(Name = "Stary Indeks")]
    public string? OldProductNumber { get; set; }

    [Display(Name = "Indeks 1")]
    public string? Idx01 { get; set; }

    [Display(Name = "Indeks 2")]
    public string? Idx02 { get; set; }
}
