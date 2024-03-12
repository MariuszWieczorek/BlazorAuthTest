using System.ComponentModel.DataAnnotations;

namespace MwTech.Shared.Currencies.Dtos;

public class CurrencyFilter
{
    [Display(Name = "Nazwa")]
    public string? Name { get; set; }

    [Display(Name = "Kod waluty")]
    public string? CurrencyCode { get; set; }

}
