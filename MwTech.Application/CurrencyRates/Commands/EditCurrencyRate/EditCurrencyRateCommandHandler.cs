using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.CurrencyRates.Commands.EditCurrencyRate;

public class EditCurrencyRateCommandHandler : IRequestHandler<EditCurrencyRateCommand>
{
    private readonly IApplicationDbContext _context;

    public EditCurrencyRateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EditCurrencyRateCommand request, CancellationToken cancellationToken)
    {
        var currencyRate = await _context.CurrencyRates.FirstOrDefaultAsync(x => x.Id == request.Id);
        
        currencyRate.AccountingPeriodId = request.AccountingPeriodId;
        currencyRate.FromCurrencyId = request.FromCurrencyId;
        currencyRate.Rate = request.Rate;
        currencyRate.EstimatedRate = request.EstimatedRate;

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
