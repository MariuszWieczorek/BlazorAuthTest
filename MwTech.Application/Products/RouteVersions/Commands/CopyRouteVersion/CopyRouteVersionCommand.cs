using MediatR;

namespace MwTech.Application.Products.RouteVersions.Commands.CopyRouteVersion;

public class CopyRouteVersionCommand : IRequest
{
    public int ProductId { get; set; }
    public int RouteVersionId { get; set; }
}
