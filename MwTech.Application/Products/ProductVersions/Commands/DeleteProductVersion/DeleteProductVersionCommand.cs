using MediatR;

namespace MwTech.Application.Products.ProductVersions.Commands.DeleteProductVersion;

public class DeleteProductVersionCommand : IRequest
{
    public int ProductVersionId { get; set; }
    public int ProductId { get; set; }
}
