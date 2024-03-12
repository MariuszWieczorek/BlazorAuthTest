using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.MachineCategories.Commands.EditMachineCategory;

namespace MwTech.Application.MachineCategories.Queries.GetEditMachineCategoryViewModel;

public class GetEditMachineCategoryViewModelQueryHandler : IRequestHandler<GetEditMachineCategoryViewModelQuery, EditMachineCategoryViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetEditMachineCategoryViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EditMachineCategoryViewModel> Handle(GetEditMachineCategoryViewModelQuery request, CancellationToken cancellationToken)
    {

        var machineCategory =  _context.MachineCategories.Single(x => x.Id == request.Id);

        var editMachineCategoryCommand = new EditMachineCategoryCommand
        {
            Id = machineCategory.Id,
            Name = machineCategory.Name,
            MachineCategoryNumber = machineCategory.MachineCategoryNumber,
            Description = machineCategory.Description,
            ProductCategoryId = machineCategory.ProductCategoryId
        };

        return new EditMachineCategoryViewModel()
        {
            EditMachineCategoryCommand = editMachineCategoryCommand,
            ProductCategories = await _context.ProductCategories.ToListAsync()
        };

    }
    
}
