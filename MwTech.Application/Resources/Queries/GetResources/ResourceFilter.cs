using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Application.Resources.Queries.GetResources;

public class ResourceFilter
{
    [Display(Name = "Nazwa")]
    public string? Name { get; set; }

    [Display(Name = "Symbol")]
    public string? ResourceNumber { get; set; }

    [Display(Name = "Kategoria Produktu")]
    public int ProductCategoryId { get; set; }

}
