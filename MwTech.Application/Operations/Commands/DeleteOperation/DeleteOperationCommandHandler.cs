using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Operations.Commands.DeleteOperation;

public class DeleteOperationCommandHandler : IRequestHandler<DeleteOperationCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteOperationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteOperationCommand request, CancellationToken cancellationToken)
    {
        
        var operationToDelete = await _context.Operations.SingleOrDefaultAsync(x => x.Id == request.Id);
        _context.Operations.Remove(operationToDelete);
        await _context.SaveChangesAsync();

        return;

    }
}
