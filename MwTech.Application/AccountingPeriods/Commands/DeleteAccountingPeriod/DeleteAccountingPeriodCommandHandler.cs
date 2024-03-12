using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.AccountingPeriods.Commands.DeleteAccountingPeriod;

public class DeleteAccountingPeriodCommandHandler : IRequestHandler<DeleteAccountingPeriodCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteAccountingPeriodCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteAccountingPeriodCommand request, CancellationToken cancellationToken)
    {
        
        var accountingPeriodToDelete = await _context.AccountingPeriods.SingleOrDefaultAsync(x => x.Id == request.Id);
        _context.AccountingPeriods.Remove(accountingPeriodToDelete);
        await _context.SaveChangesAsync();

        return;

    }
}
