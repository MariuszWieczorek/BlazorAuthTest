using MediatR;

namespace MwTech.Application.Products.ProductPropertiesVersions.Commands.DeleteProductPropertiesVersion;

public class DeleteProductPropertiesVersionCommand : IRequest
{
    public int ProductId { get; set; }
    public int ProductPropertiesVersionId { get; set; }
    
}
