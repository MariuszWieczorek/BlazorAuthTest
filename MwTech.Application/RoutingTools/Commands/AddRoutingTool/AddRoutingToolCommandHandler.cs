using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;

namespace MwTech.Application.RoutingTools.Commands.AddRoutingTool;

public class AddRoutingToolCommandHandler : IRequestHandler<AddRoutingToolCommand>
{
    private readonly IApplicationDbContext _context;

    public AddRoutingToolCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddRoutingToolCommand request, CancellationToken cancellationToken)
    {
        var routingTool = new RoutingTool();
        
        routingTool.ToolNumber = request.ToolNumber;
        routingTool.Name = request.Name;

        await _context.RoutingTools.AddAsync(routingTool);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
