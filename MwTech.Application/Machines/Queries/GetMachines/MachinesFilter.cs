using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Application.Machines.Queries.GetMachines;

public class MachineFilter
{
    [Display(Name = "Id")]
    public int Id { get; set; }

    [Display(Name = "Nazwa")]
    public string? Name { get; set; }

    [Display(Name = "Symbol")]
    public string? MachineNumber { get; set; }

    [Display(Name = "Kategoria Produktu")]
    public int MachineCategoryId { get; set; }

}
