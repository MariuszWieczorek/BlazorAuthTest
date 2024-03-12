using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.AccountingPeriods.Commands.EditAccountingPeriod;

namespace MwTech.Application.AccountingPeriods.Queries.GetEditAccountingPeriodViewModel;

public class GetEditAccountingPeriodViewModelQueryHandler : IRequestHandler<GetEditAccountingPeriodViewModelQuery, EditAccountingPeriodViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetEditAccountingPeriodViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<EditAccountingPeriodViewModel> Handle(GetEditAccountingPeriodViewModelQuery request, CancellationToken cancellationToken)
    {

        var accountingPeriod = await _context.AccountingPeriods.SingleAsync(x => x.Id == request.Id);
        
        var editAccountingPeriodCommand = new EditAccountingPeriodCommand
        {
            Id = accountingPeriod.Id,
            Name = accountingPeriod.Name,
            PeriodNumber = accountingPeriod.PeriodNumber,
            Description = accountingPeriod.Description,
            StartDate = accountingPeriod.StartDate,
            EndDate = accountingPeriod.EndDate,
            IsClosed = accountingPeriod.IsClosed,
            IsActive = accountingPeriod.IsActive,
            IsDefault =  accountingPeriod.IsDefault
            
        };
        

        var vm = new EditAccountingPeriodViewModel()
        {
            ProductCategories = await _context.ProductCategories.AsNoTracking().ToListAsync(),
            Units = await _context.Units.AsNoTracking().ToListAsync(),
            EditAccountingPeriodCommand = editAccountingPeriodCommand
        };

        return vm;
    }
}
