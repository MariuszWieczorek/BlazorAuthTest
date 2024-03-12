using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.AccountingPeriods.Queries.GetAccountingPeriods;

public class GetAccountingPeriodsQueryHandler : IRequestHandler<GetAccountingPeriodsQuery, AccountingPeriodsViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetAccountingPeriodsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<AccountingPeriodsViewModel> Handle(GetAccountingPeriodsQuery request, CancellationToken cancellationToken)
    {
        var accountingPeriods = _context.AccountingPeriods
            .AsNoTracking()
            .AsQueryable();

        accountingPeriods = Filter(accountingPeriods, request.AccountingPeriodFilter);

        var AccountingPeriodsList = await accountingPeriods.ToListAsync();

        var vm = new AccountingPeriodsViewModel
            { 
              AccountingPeriods = AccountingPeriodsList,
              AccountingPeriodFilter = request.AccountingPeriodFilter
            };

        return vm;
           
    }

    public IQueryable<AccountingPeriod> Filter(IQueryable<AccountingPeriod> AccountingPeriods, AccountingPeriodFilter AccountingPeriodFilter)
    {
        if (AccountingPeriodFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(AccountingPeriodFilter.Name))
                AccountingPeriods = AccountingPeriods.Where(x => x.Name.Contains(AccountingPeriodFilter.Name));

            if (!string.IsNullOrWhiteSpace(AccountingPeriodFilter.PeriodNumber))
                AccountingPeriods = AccountingPeriods.Where(x => x.PeriodNumber.Contains(AccountingPeriodFilter.PeriodNumber));
        }

        return AccountingPeriods;
    }
}
