using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Machines.Commands.AddMachine;

public class AddMachineCommandHandler : IRequestHandler<AddMachineCommand>
{
    private readonly IApplicationDbContext _context;

    public AddMachineCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddMachineCommand request, CancellationToken cancellationToken)
    {
        var operation = new Machine();
        
        operation.MachineNumber = request.MachineNumber;
        operation.Name = request.Name;
        operation.Description = request.Description;
        operation.MachineCategoryId = request.MachineCategoryId;
        operation.ReferenceNumber = request.ReferenceNumber;


        await _context.Machines.AddAsync(operation);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
