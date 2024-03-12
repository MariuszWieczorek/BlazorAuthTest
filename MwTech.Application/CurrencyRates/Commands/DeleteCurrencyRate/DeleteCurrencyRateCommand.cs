using MediatR;

namespace MwTech.Application.CurrencyRates.Commands.DeleteCurrencyRate;

public class DeleteCurrencyRateCommand : IRequest
{
    public int Id { get; set; }
}
