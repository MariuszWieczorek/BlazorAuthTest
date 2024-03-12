using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Application.Props.Queries.GetProperties;

public class PropertyFilter
{
    [Display(Name = "Nazwa")]
    public string? Name { get; set; }

    [Display(Name = "Symbol")]
    public string? PropertyNumber { get; set; }

    [Display(Name = "Kategoria Produktu")]
    public int ProductCategoryId { get; set; }

}
