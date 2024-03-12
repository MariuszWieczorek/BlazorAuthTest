using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Props.Commands.DeleteProperty;

public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand>
{
    private readonly IApplicationDbContext _context;

    public DeletePropertyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
    {
        
        var propertyToDelete = await _context.Properties.SingleOrDefaultAsync(x => x.Id == request.Id);
        _context.Properties.Remove(propertyToDelete);
        await _context.SaveChangesAsync();

        return;

    }
}
