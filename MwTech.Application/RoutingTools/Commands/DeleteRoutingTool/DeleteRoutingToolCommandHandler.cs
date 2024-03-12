using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.RoutingTools.Commands.DeleteRoutingTool;

public class DeleteRoutingToolCommandHandler : IRequestHandler<DeleteRoutingToolCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteRoutingToolCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteRoutingToolCommand request, CancellationToken cancellationToken)
    {
        
        var RoutingToolToDelete = await _context.RoutingTools.SingleOrDefaultAsync(x => x.Id == request.Id);
        _context.RoutingTools.Remove(RoutingToolToDelete);
        await _context.SaveChangesAsync();

        return;

    }
}
