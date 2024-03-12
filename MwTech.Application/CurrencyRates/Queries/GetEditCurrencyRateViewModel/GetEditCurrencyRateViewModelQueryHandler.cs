using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.CurrencyRates.Commands.EditCurrencyRate;

namespace MwTech.Application.CurrencyRates.Queries.GetEditCurrencyRateViewModel;

public class GetEditCurrencyRateViewModelQueryHandler : IRequestHandler<GetEditCurrencyRateViewModelQuery, EditCurrencyRateViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetEditCurrencyRateViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<EditCurrencyRateViewModel> Handle(GetEditCurrencyRateViewModelQuery request, CancellationToken cancellationToken)
    {

        var currencyRate = await _context.CurrencyRates.SingleAsync(x => x.Id == request.Id);
        
        var editCurrencyRateCommand = new EditCurrencyRateCommand
        {
            Id = currencyRate.Id,
            FromCurrencyId = currencyRate.FromCurrencyId,
            AccountingPeriodId = currencyRate.AccountingPeriodId,
            Rate = currencyRate.Rate,
            EstimatedRate = currencyRate.EstimatedRate
        };
        

        var vm = new EditCurrencyRateViewModel()
        {
            AccountingPeriods = await _context.AccountingPeriods.AsNoTracking().ToListAsync(),
            Currencies = await _context.Currencies.Where(x=>x.CurrencyCode.Trim() != "PLN").AsNoTracking().ToListAsync(),
            EditCurrencyRateCommand = editCurrencyRateCommand
        };

        return vm;
    }
}
