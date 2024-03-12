using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Operations.Commands.EditOperation;

public class EditOperationCommandHandler : IRequestHandler<EditOperationCommand>
{
    private readonly IApplicationDbContext _context;

    public EditOperationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EditOperationCommand request, CancellationToken cancellationToken)
    {
        var operation = await _context.Operations.FirstOrDefaultAsync(x => x.Id == request.Id);
        
        operation.OperationNumber = request.OperationNumber;
        operation.Name = request.Name;
        operation.Description = request.Description;
        operation.ProductCategoryId = request.ProductCategoryId;
        operation.UnitId = request.UnitId;
        operation.No = request.No;
                
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
