using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Application.MachineCategories.Queries.GetMachineCategories;

public class MachineCategoryFilter
{
    [Display(Name = "Nazwa")]
    public string? Name { get; set; }

    [Display(Name = "Symbol")]
    public string? MachineCategoryNumber { get; set; }

}
