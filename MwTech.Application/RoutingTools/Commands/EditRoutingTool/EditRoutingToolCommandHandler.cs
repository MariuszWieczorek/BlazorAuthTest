using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;

namespace MwTech.Application.RoutingTools.Commands.EditRoutingTool;

public class EditRoutingToolCommandHandler : IRequestHandler<EditRoutingToolCommand>
{
    private readonly IApplicationDbContext _context;

    public EditRoutingToolCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EditRoutingToolCommand request, CancellationToken cancellationToken)
    {
        var RoutingTool = await _context.RoutingTools.FirstOrDefaultAsync(x => x.Id == request.Id);
        
        RoutingTool.ToolNumber = request.ToolNumber;
        RoutingTool.Name = request.Name;


        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
