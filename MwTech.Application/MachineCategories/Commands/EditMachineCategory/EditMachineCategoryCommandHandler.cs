using MwTech.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MwTech.Application.MachineCategories.Commands.EditMachineCategory;
public class EditMachineCategoryCommandHandler : IRequestHandler<EditMachineCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public EditMachineCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EditMachineCategoryCommand request, CancellationToken cancellationToken)
    {
        var machineCategoryToUpdate = await _context.MachineCategories
            .SingleAsync(x => x.Id == request.Id);

        machineCategoryToUpdate.Name = request.Name;
        machineCategoryToUpdate.MachineCategoryNumber = request.MachineCategoryNumber;
        machineCategoryToUpdate.Description = request.Description;  
        machineCategoryToUpdate.ProductCategoryId = request.ProductCategoryId;

        _context.MachineCategories.Update(machineCategoryToUpdate);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
