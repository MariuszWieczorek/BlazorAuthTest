using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Application.Settings.Queries.GetSettings;

public class SettingFilter
{

    public int Id { get; set; }
    [Display(Name = "Nazwa")]
    public string? Name { get; set; }

    [Display(Name = "Symbol")]
    public string? SettingNumber { get; set; }

    [Display(Name = "Kategoria Ustawień")]
    public int SettingCategoryId { get; set; }

    [Display(Name = "Kategoria Maszyny")]
    public int MachineCategoryId { get; set; }
}
