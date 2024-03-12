using MediatR;
using MwTech.Domain.Entities;

namespace MwTech.Application.IfsSourceRoutes.Command.UpdateRoutesInMwTech;

public class UpdateRoutesInMwTechCommand : IRequest
{
    public IEnumerable<ComparedRouteIfsVsMwTech> ComparedRoutes { get; set; }
}
