using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Resources.Commands.DeleteResource;

public class DeleteResourceCommandHandler : IRequestHandler<DeleteResourceCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteResourceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
    {
        
        var resourceToDelete = await _context.Resources.SingleOrDefaultAsync(x => x.Id == request.Id);
        _context.Resources.Remove(resourceToDelete);

        await _context.SaveChangesAsync();

        return;

    }
}
