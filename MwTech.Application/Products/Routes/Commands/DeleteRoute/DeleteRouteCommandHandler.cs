using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.Routes.Commands.DeleteRoute;

public class DeleteRouteCommandHandler : IRequestHandler<DeleteRouteCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteRouteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    // todo p.ProductId == request.ProductId &&
    public async Task Handle(DeleteRouteCommand request, CancellationToken cancellationToken)
    {
        var routeToDelete = _context.ManufactoringRoutes
            .SingleOrDefault(p =>  p.RouteVersionId == request.RouteVersionId && p.Id == request.Id);

        _context.ManufactoringRoutes.Remove(routeToDelete);
        
        await _context.SaveChangesAsync();

        return;
    }
}
