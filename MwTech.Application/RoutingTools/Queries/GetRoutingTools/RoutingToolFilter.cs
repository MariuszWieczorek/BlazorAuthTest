using System.ComponentModel.DataAnnotations;

namespace MwTech.Application.RoutingTools.Queries.GetRoutingTools;

public class RoutingToolFilter
{
    [Display(Name = "Nazwa")]
    public string? Name { get; set; }

    [Display(Name = "Symbol")]
    public string? ToolNumber { get; set; }

    [Display(Name = "Kategoria Produktu")]
    public int ProductCategoryId { get; set; }

}
