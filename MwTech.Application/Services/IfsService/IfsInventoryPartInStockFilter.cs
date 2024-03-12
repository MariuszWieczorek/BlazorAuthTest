using System.ComponentModel.DataAnnotations;

namespace MwTech.Application.Services.IfsService;

public class IfsInventoryPartInStockFilter
{
    
    [Display(Name = "Indeks Produktu")]
    public string? PartNo { get; set; }

}
