using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Machines.Commands.EditMachine;

namespace MwTech.Application.Machines.Queries.GetEditMachineViewModel;

public class GetEditMachineViewModelQueryHandler : IRequestHandler<GetEditMachineViewModelQuery, EditMachineViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetEditMachineViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<EditMachineViewModel> Handle(GetEditMachineViewModelQuery request, CancellationToken cancellationToken)
    {

        var operation = await _context.Machines.SingleAsync(x => x.Id == request.Id);
        
        var editMachineCommand = new EditMachineCommand
        {
            Id = operation.Id,
            Name = operation.Name,
            MachineNumber = operation.MachineNumber,
            Description = operation.Description,
            MachineCategoryId = operation.MachineCategoryId,
            ReferenceNumber = operation.ReferenceNumber
        };
        

        var vm = new EditMachineViewModel()
        {
            MachineCategories = await _context.MachineCategories.AsNoTracking().ToListAsync(),
            EditMachineCommand = editMachineCommand
        };

        return vm;
    }
}
