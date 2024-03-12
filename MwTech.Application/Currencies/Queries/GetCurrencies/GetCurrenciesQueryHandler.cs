using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Extensions;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using MwTech.Shared.Currencies.Dtos;

namespace MwTech.Application.Currencies.Queries.GetCurrencies;

public class GetCurrenciesQueryHandler : IRequestHandler<GetCurrenciesQuery, CurrenciesViewModel>
{
    private readonly ILogger<GetCurrenciesQueryHandler> _logger;
    private readonly IApplicationDbContext _context;

    public GetCurrenciesQueryHandler(ILogger<GetCurrenciesQueryHandler> logger, IApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<CurrenciesViewModel> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var currencies = _context.Currencies
            .AsNoTracking()
            .AsQueryable();

        currencies = Filter(currencies, request.CurrencyFilter);
        
        var currenciesList = await currencies
            .OrderBy(x=>x.CurrencyCode)
            .ToListAsync();

        int pageNumber = request.PageNumber;
        int pageSize = 100;

        var vm = new CurrenciesViewModel
        {
            Currencies = currenciesList,
            Currencies2 = await currencies.PaginatedListAsync(pageNumber,pageSize),
            CurrencyFilter = request.CurrencyFilter
        };


        return  vm;
    }

    private IQueryable<Currency> Filter(IQueryable<Currency> currencies, CurrencyFilter currencyFilter)
    {
        if (currencyFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(currencyFilter.Name))
                currencies = currencies.Where(x => x.Name.Contains(currencyFilter.Name));

            if (!string.IsNullOrWhiteSpace(currencyFilter.CurrencyCode))
                currencies = currencies.Where(x => x.CurrencyCode.ToUpper().Contains(currencyFilter.CurrencyCode.ToUpper()));
          
        }

        return currencies;
    }


}
