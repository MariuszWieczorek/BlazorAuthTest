using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.CurrencyRates.Commands.AddCurrencyRate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.CurrencyRates.Queries.GetAddCurrencyRateViewModel;

public class GetAddCurrencyRateViewModelQueryHandler : IRequestHandler<GetAddCurrencyRateViewModelQuery, AddCurrencyRateViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetAddCurrencyRateViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<AddCurrencyRateViewModel> Handle(GetAddCurrencyRateViewModelQuery request, CancellationToken cancellationToken)
    {
        var vm = new AddCurrencyRateViewModel()
        {
            AccountingPeriods = await _context.AccountingPeriods.AsNoTracking().ToListAsync(),
            Currencies = await _context.Currencies.AsNoTracking().ToListAsync(),
            AddCurrencyRateCommand = new AddCurrencyRateCommand { FromCurrencyId = request.CurrencyId, AccountingPeriodId = request.PeriodId }
        };

        return vm;
    }
}
