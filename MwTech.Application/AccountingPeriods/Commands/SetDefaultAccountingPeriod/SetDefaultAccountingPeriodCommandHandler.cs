using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.AccountingPeriods.Commands.SetDefaultAccountingPeriod;

public class SetDefaultAccountingPeriodCommandHandler : IRequestHandler<SetDefaultAccountingPeriodCommand>
{
    private readonly IApplicationDbContext _context;

    public SetDefaultAccountingPeriodCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(SetDefaultAccountingPeriodCommand request, CancellationToken cancellationToken)
    {


        var periods = _context.AccountingPeriods;
        foreach (var item in periods)
        {
            item.IsDefault = (item.Id == request.Id);
        }

        await _context.SaveChangesAsync();

        return;
    }
}
