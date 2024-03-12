using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.MachineCategories.Commands.DeleteMachineCategory;

public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteMachineCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCurrencyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(DeleteMachineCategoryCommand request, CancellationToken cancellationToken)
    {
        var machineCategoryToDelete = _context.MachineCategories.Single(x => x.Id == request.Id);
        _context.MachineCategories.Remove(machineCategoryToDelete);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
