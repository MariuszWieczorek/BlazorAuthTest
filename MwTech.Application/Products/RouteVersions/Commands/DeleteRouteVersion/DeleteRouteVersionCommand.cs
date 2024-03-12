using MediatR;

namespace MwTech.Application.Products.RouteVersions.Commands.DeleteRouteVersion;

public class DeleteRouteVersionCommand : IRequest
{
    public int RouteVersionId { get; set; }
    public int ProductId { get; set; }
}
