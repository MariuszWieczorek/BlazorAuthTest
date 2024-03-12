using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Application.SettingCategories.Queries.GetSettingCategories;

public class SettingCategoryFilter
{
    public int Id { get; set; }
    [Display(Name = "Nazwa")]
    public string? Name { get; set; }

    [Display(Name = "Symbol")]
    public string? SettingCategoryNumber { get; set; }

    [Display(Name = "Kategoria Maszyny")]
    public int MachineCategoryId { get; set; }

}
