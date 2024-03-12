using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.MachineCategories.Commands.AddMachineCategory;

namespace MwTech.Application.MachineCategories.Queries.GetAddMachineCategoryViewModel;

public class GetAddMachineCategoryViewModelQueryHandler : IRequestHandler<GetAddMachineCategoryViewModelQuery, AddMachineCategoryViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetAddMachineCategoryViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AddMachineCategoryViewModel> Handle(GetAddMachineCategoryViewModelQuery request, CancellationToken cancellationToken)
    {

        return new AddMachineCategoryViewModel
        {
            AddMachineCategoryCommand = new AddMachineCategoryCommand(),
            ProductCategories = await _context.ProductCategories.ToListAsync()
        };
    }
    
}
