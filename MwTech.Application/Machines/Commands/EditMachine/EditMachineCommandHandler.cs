using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Machines.Commands.EditMachine;

public class EditMachineCommandHandler : IRequestHandler<EditMachineCommand>
{
    private readonly IApplicationDbContext _context;

    public EditMachineCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EditMachineCommand request, CancellationToken cancellationToken)
    {
        var operation = await _context.Machines.FirstOrDefaultAsync(x => x.Id == request.Id);
        
        operation.MachineNumber = request.MachineNumber;
        operation.Name = request.Name;
        operation.Description = request.Description;
        operation.MachineCategoryId = request.MachineCategoryId;
        operation.ReferenceNumber = request.ReferenceNumber;
                
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
