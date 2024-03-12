using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.RouteVersions.Commands.SetRouteVersionAsDefault;


public class SetRouteVersionAsDefaultCommandHandler : IRequestHandler<SetRouteVersionAsDefaultCommand>
{
    private readonly IApplicationDbContext _context;

    public SetRouteVersionAsDefaultCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(SetRouteVersionAsDefaultCommand request, CancellationToken cancellationToken)
    {


        var routeVersion = _context.RouteVersions
            .SingleOrDefault(x => x.Id == request.RouteVersionId);




        if (routeVersion != null)
        {

            var routeVersions = _context.RouteVersions
                    .Where(x => x.ProductId == request.ProductId && x.ProductCategoryId == routeVersion.ProductCategoryId);

            foreach (var item in routeVersions)
            {
                item.DefaultVersion = (item.Id == request.RouteVersionId);
            }
        }

        await _context.SaveChangesAsync();

        return;
    }
}
