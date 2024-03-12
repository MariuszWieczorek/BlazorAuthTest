using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Units.Commands.DeleteUnit;

public class DeleteUnitCommandHandler : IRequestHandler<DeleteUnitCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteUnitCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
    {
        
        var unitToDelete = _context.Units.Single(x => x.Id == request.Id);
        _context.Units.Remove(unitToDelete);
        await _context.SaveChangesAsync();

        return;

    }
}
