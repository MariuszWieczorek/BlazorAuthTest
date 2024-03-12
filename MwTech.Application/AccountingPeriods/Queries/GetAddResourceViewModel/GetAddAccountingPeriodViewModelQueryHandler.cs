using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.AccountingPeriods.Commands.AddAccountingPeriod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.AccountingPeriods.Queries.GetAddAccountingPeriodViewModel;

public class GetAddAccountingPeriodViewModelQueryHandler : IRequestHandler<GetAddAccountingPeriodViewModelQuery, AddAccountingPeriodViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetAddAccountingPeriodViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<AddAccountingPeriodViewModel> Handle(GetAddAccountingPeriodViewModelQuery request, CancellationToken cancellationToken)
    {
        var vm = new AddAccountingPeriodViewModel()
        {
            AddAccountingPeriodCommand = new AddAccountingPeriodCommand()
        };

        return vm;
    }
}
