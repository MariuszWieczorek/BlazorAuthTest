using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Operations.Commands.AddOperation;

public class AddOperationCommandHandler : IRequestHandler<AddOperationCommand>
{
    private readonly IApplicationDbContext _context;

    public AddOperationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddOperationCommand request, CancellationToken cancellationToken)
    {
        var operation = new Operation();
        
        operation.OperationNumber = request.OperationNumber;
        operation.Name = request.Name;
        operation.Description = request.Description;
        operation.ProductCategoryId = request.ProductCategoryId;
        operation.UnitId = request.UnitId;
        operation.No = request.No;


        await _context.Operations.AddAsync(operation);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
