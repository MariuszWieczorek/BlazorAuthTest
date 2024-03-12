using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.ProductSettingsVersions.Queries.GetProductsSettingsVersions;

public class ProductSettingVersionFilter
{
    [Display(Name = "Indeks Produktu")]
    public string ProductNumber { get; set; }
    [Display(Name = "Symbol Maszyny")]
    public string MachineNumber { get; set; }
    [Display(Name = "Symbol Gniazda")]
    public string WorkCenterNumber { get; set; }
    [Display(Name = "Kategoria Produktu")]
    public int ProductCategoryId { get; set; }
    [Display(Name = "Kategoria Maszyny")]
    public int MachineCategoryId { get; set; }

    [Display(Name = "aktywne")]
    public bool IsActive { get; set; } = true;

    [Display(Name = "brak 1 akcept")]
    public bool NoFirstAcceptation { get; set; } = false;

    [Display(Name = "brak 2 akcept")]
    public bool NoSecondAcceptation { get; set; } = false;

    [Display(Name = "brak 3 akcept")]
    public bool NoThirdAcceptation { get; set; } = false;

    [Display(Name = "Nazwa wersji")]
    public string Name { get; set; }

    [Display(Name = "KT")]
    public int? TechCardNumber { get; set; }
}
