using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.MachineCategories.Commands.AddMachineCategory;

public class AddMachineCategoryCommandHandler : IRequestHandler<AddMachineCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public AddMachineCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddMachineCategoryCommand request, CancellationToken cancellationToken)
    {
        var machineCategory = new MachineCategory();


        machineCategory.Name = request.Name;
        machineCategory.MachineCategoryNumber = request.MachineCategoryNumber;
        machineCategory.Description = request.Description;
        machineCategory.ProductCategoryId = request.ProductCategoryId;

        await _context.MachineCategories.AddAsync(machineCategory);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
