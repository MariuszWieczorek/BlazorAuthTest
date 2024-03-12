using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.CurrencyRates.Commands.AddCurrencyRate;

public class AddCurrencyRateCommandHandler : IRequestHandler<AddCurrencyRateCommand>
{
    private readonly IApplicationDbContext _context;

    public AddCurrencyRateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddCurrencyRateCommand request, CancellationToken cancellationToken)
    {
        var currencyRate = new CurrencyRate();

        currencyRate.AccountingPeriodId = request.AccountingPeriodId;
        currencyRate.FromCurrencyId = request.FromCurrencyId;
        currencyRate.Rate = request.Rate;
        currencyRate.EstimatedRate = request.EstimatedRate;


        await _context.CurrencyRates.AddAsync(currencyRate);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
