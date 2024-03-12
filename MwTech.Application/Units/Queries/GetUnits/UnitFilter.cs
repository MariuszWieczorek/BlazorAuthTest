using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Units.Queries.GetUnits;
public class UnitFilter
{
    [Display(Name = "Nazwa jednostki miary")]
    public string? Name { get; set; }

    [Display(Name = "Kod jednostki miary")]
    public string? UnitCode { get; set; }
}
