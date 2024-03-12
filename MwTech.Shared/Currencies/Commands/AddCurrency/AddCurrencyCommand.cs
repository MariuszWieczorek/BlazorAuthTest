using MediatR;
using System.ComponentModel.DataAnnotations;


namespace MwTech.Shared.Currencies.Commands.AddCurrency;

public class AddCurrencyCommand : IRequest
{
    [Display(Name = "Kod")]
    public string CurrencyCode { get; set; }

    [Display(Name = "Nazwa")]
    public string Name { get; set; }
}
