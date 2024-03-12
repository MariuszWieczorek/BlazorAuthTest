using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.IfsSourceRoutes.Command.UpdateRoutesInMwTech;

public class UpdateRoutesInMwTechCommandHandler : IRequestHandler<UpdateRoutesInMwTechCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateRoutesInMwTechCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateRoutesInMwTechCommand request, CancellationToken cancellationToken)
    {


        var routes = request.ComparedRoutes;

        _context.Clear();

        foreach (var route in routes)
        {
            if (route.RoutePositionId != 0)
            {
                

                var routeToUpdate = await _context.ManufactoringRoutes
                    .SingleOrDefaultAsync(x => x.Id == route.RoutePositionId);

                var resource = await _context.Resources
                    .SingleOrDefaultAsync(x => x.ResourceNumber == route.IfsSetupLaborClassNo);

                var tool = await _context.RoutingTools
                    .SingleOrDefaultAsync(x => x.ToolNumber == route.IfsToolId);

             /*
                if (resource != null && routeToUpdate != null)
                {
                    routeToUpdate.ChangeOverResourceId = resource.Id;
                    routeToUpdate.ChangeOverNumberOfEmployee = route.IfsSetupCrewSize.GetValueOrDefault();
                    routeToUpdate.MoveTime = route.IfsMoveTime.GetValueOrDefault();
                }
             */

                if (tool != null && routeToUpdate != null)
                {
                    routeToUpdate.RoutingToolId = tool.Id;
                    routeToUpdate.ToolQuantity = route.IfsToolQuantity.GetValueOrDefault();
                }


            }
            
            

        }

        await _context.SaveChangesAsync();

    }


}
