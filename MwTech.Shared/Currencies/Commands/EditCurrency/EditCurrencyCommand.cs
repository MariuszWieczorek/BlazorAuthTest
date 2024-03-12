using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MwTech.Shared.Currencies.Commands.EditCurrency;
public class EditCurrencyCommand : IRequest
{
    public int Id { get; set; }


    [Required(ErrorMessage = "Pole 'Kod Waluty' jest wymagane.")]
    [DisplayName("Kod Waluty")]
    public string CurrencyCode { get; set; }

    [Required(ErrorMessage = "Pole 'Nazwa' jest wymagane.")]
    [DisplayName("Nazwa")]
    public string Name { get; set; }
}
