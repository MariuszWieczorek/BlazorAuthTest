using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.AccountingPeriods.Commands.AddAccountingPeriod;

public class AddAccountingPeriodCommandHandler : IRequestHandler<AddAccountingPeriodCommand>
{
    private readonly IApplicationDbContext _context;

    public AddAccountingPeriodCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddAccountingPeriodCommand request, CancellationToken cancellationToken)
    {
        var accountingPeriod = new AccountingPeriod();
        
        accountingPeriod.PeriodNumber = request.PeriodNumber;
        accountingPeriod.Name = request.Name;
        accountingPeriod.Description = request.Description;
        accountingPeriod.StartDate = request.StartDate;
        accountingPeriod.EndDate = request.EndDate;
        accountingPeriod.IsActive = request.IsActive;
        accountingPeriod.IsClosed = request.IsClosed;


        await _context.AccountingPeriods.AddAsync(accountingPeriod);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
