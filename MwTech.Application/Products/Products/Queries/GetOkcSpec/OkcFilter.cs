using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Products.Queries.GetOkcSpec;

public class OkcFilter
{
    public int Id { get; set; }

    [Display(Name = "Nazwa Produktu")]
    public string? Name { get; set; }

    [Display(Name = "Tylko Aktywne")]
    public bool IsActive { get; set; } = true;

    [Display(Name = "Indeks Produktu")]
    public string? ProductNumber { get; set; }

    [Display(Name = "KT")]
    public int? TechCardNumber { get; set; }

    [Display(Name = "Indeks 1")]
    public string? Idx01 { get; set; }

    [Display(Name = "Indeks 2")]
    public string? Idx02 { get; set; }

    [Display(Name = "Brak Szerokości")]
    public bool NoOkcSzerokosc { get; set; } = false;

    [Display(Name = "Brak Kąta")]
    public bool NoOkcKatCiecia { get; set; } = false;
}
