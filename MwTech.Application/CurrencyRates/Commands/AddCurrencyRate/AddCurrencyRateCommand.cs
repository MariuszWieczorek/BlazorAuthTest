using MediatR;
using System.ComponentModel.DataAnnotations;


namespace MwTech.Application.CurrencyRates.Commands.AddCurrencyRate;

public class AddCurrencyRateCommand : IRequest
{
    public int Id { get; set; }

    [Display(Name = "Okres")]
    public int AccountingPeriodId { get; set; }

    [Display(Name = "Waluta")]
    public int FromCurrencyId { get; set; }

    [Display(Name = "Kurs")]
    public decimal Rate { get; set; }

    [Display(Name = "Szacowany Kurs")]
    public decimal EstimatedRate { get; set; }

}
