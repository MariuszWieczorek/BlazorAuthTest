using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.RouteVersions.Commands.SetRouteVersionAsInactive;


public class SetRouteVersionAsInactiveCommandHandler : IRequestHandler<SetRouteVersionAsInactiveCommand>
{
    private readonly IApplicationDbContext _context;

    public SetRouteVersionAsInactiveCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(SetRouteVersionAsInactiveCommand request, CancellationToken cancellationToken)
    {
        var routeVersion = _context.RouteVersions
            .SingleOrDefault(x => x.Id == request.RouteVersionId && x.ProductId == request.ProductId);
                        
        routeVersion.IsActive = false;

        await _context.SaveChangesAsync();

        return;
    }
}
