using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.CurrencyRates.Queries.GetCurrencyRates;

public class GetCurrencyRatesQueryHandler : IRequestHandler<GetCurrencyRatesQuery, CurrencyRatesViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetCurrencyRatesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<CurrencyRatesViewModel> Handle(GetCurrencyRatesQuery request, CancellationToken cancellationToken)
    {


        var currencyRates = _context.Currencies.Where(x => x.CurrencyCode.Trim() != "PLN")
           .Join(_context.AccountingPeriods, c => true, a => true, (c, a) => new { c, a })
           .ToList()
           .GroupJoin(_context.CurrencyRates,
            ca => new { p1 = ca.c.Id, p2 = ca.a.Id },
            c => new { p1 = c.FromCurrencyId, p2 = c.AccountingPeriodId },
            (ca, cr) => new { ca, cr })
           .SelectMany(x => x.cr.DefaultIfEmpty(),
           (p, cr) => new ShortCurrencyRate
           {
               Id = cr?.Id,
               FromCurrency = p.ca.c,
               FromCurrencyId = p.ca.c.Id,
               AccountingPeriod = p.ca.a,
               AccountingPeriodId = p.ca.a.Id,
               Rate = cr?.Rate,
               EstimatedRate = cr?.EstimatedRate
           }
           )
           .AsQueryable();

        if (request.CurrencyRateFilter == null)
        {
            var period = _context.AccountingPeriods.SingleOrDefault(x => x.IsDefault);
            int periodId = period != null ? period.Id : 0;

            request.CurrencyRateFilter = new CurrencyRateFilter
            {
                AccountingPeriodId = periodId
            };
        }

        currencyRates = Filter(currencyRates, request.CurrencyRateFilter);

        var currencyRatesList = currencyRates.ToList();

        var vm = new CurrencyRatesViewModel
        {
            CurrencyRates = currencyRatesList,
            CurrencyRateFilter = request.CurrencyRateFilter,
            AccountingPeriods = await _context.AccountingPeriods.ToListAsync(),
            Currencies = await _context.Currencies.Where(x => x.CurrencyCode.Trim() != "PLN").ToListAsync()
        };

        return vm;

    }

    public IQueryable<ShortCurrencyRate> Filter(IQueryable<ShortCurrencyRate> currencyRates, CurrencyRateFilter currencyRateFilter)
    {


        if (currencyRateFilter != null)
        {
            if (currencyRateFilter.AccountingPeriodId != 0)
                currencyRates = currencyRates.Where(x => x.AccountingPeriodId == currencyRateFilter.AccountingPeriodId);

            if (currencyRateFilter.CurrencyId != 0)
                currencyRates = currencyRates.Where(x => x.FromCurrencyId == currencyRateFilter.CurrencyId);

            if (!string.IsNullOrWhiteSpace(currencyRateFilter.CurrencyCode))
                currencyRates = currencyRates.Where(x => x.FromCurrency.CurrencyCode.Contains(currencyRateFilter.CurrencyCode));
        }


        return currencyRates;
    }
}
