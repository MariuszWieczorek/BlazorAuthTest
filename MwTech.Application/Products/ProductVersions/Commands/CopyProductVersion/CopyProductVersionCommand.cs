using MediatR;

namespace MwTech.Application.Products.ProductVersions.Commands.CopyProductVersion;

public class CopyProductVersionCommand : IRequest
{
    public int ProductId { get; set; }
    public int ProductVersionId { get; set; }
}
