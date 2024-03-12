using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Operations.Commands.EditOperation;

namespace MwTech.Application.Operations.Queries.GetEditOperationViewModel;

public class GetEditOperationViewModelQueryHandler : IRequestHandler<GetEditOperationViewModelQuery, EditOperationViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetEditOperationViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<EditOperationViewModel> Handle(GetEditOperationViewModelQuery request, CancellationToken cancellationToken)
    {

        var operation = await _context.Operations.SingleAsync(x => x.Id == request.Id);
        
        var editOperationCommand = new EditOperationCommand
        {
            Id = operation.Id,
            Name = operation.Name,
            OperationNumber = operation.OperationNumber,
            Description = operation.Description,
            UnitId = operation.UnitId,
            ProductCategoryId = operation.ProductCategoryId,
            No = operation.No
        };
        

        var vm = new EditOperationViewModel()
        {
            ProductCategories = await _context.ProductCategories.AsNoTracking().ToListAsync(),
            Units = await _context.Units.AsNoTracking().ToListAsync(),
            EditOperationCommand = editOperationCommand
        };

        return vm;
    }
}
