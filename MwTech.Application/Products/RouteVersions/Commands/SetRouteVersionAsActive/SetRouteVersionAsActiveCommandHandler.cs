using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.RouteVersions.Commands.SetRouteVersionAsActive;


public class SetRouteVersionAsActiveCommandHandler : IRequestHandler<SetRouteVersionAsActiveCommand>
{
    private readonly IApplicationDbContext _context;

    public SetRouteVersionAsActiveCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(SetRouteVersionAsActiveCommand request, CancellationToken cancellationToken)
    {
        var routeVersion = _context.RouteVersions
            .SingleOrDefault(x => x.Id == request.RouteVersionId && x.ProductId == request.ProductId);
                        
        routeVersion.IsActive = true;

        await _context.SaveChangesAsync();

        return;
    }
}
