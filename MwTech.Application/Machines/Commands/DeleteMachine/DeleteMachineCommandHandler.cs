using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Machines.Commands.DeleteMachine;

public class DeleteMachineCommandHandler : IRequestHandler<DeleteMachineCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteMachineCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteMachineCommand request, CancellationToken cancellationToken)
    {
        
        var operationToDelete = await _context.Machines.SingleOrDefaultAsync(x => x.Id == request.Id);
        _context.Machines.Remove(operationToDelete);
        await _context.SaveChangesAsync();

        return;

    }
}
