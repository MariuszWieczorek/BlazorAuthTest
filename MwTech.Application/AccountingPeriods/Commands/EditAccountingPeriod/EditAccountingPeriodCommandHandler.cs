using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.AccountingPeriods.Commands.EditAccountingPeriod;

public class EditAccountingPeriodCommandHandler : IRequestHandler<EditAccountingPeriodCommand>
{
    private readonly IApplicationDbContext _context;

    public EditAccountingPeriodCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EditAccountingPeriodCommand request, CancellationToken cancellationToken)
    {
        var accountingPeriod = await _context.AccountingPeriods.FirstOrDefaultAsync(x => x.Id == request.Id);
        
        accountingPeriod.PeriodNumber = request.PeriodNumber;
        accountingPeriod.Name = request.Name;
        accountingPeriod.Description = request.Description;
        accountingPeriod.StartDate = request.StartDate;
        accountingPeriod.EndDate = request.EndDate;
        accountingPeriod.IsActive = request.IsActive;
        accountingPeriod.IsClosed = request.IsClosed;

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
