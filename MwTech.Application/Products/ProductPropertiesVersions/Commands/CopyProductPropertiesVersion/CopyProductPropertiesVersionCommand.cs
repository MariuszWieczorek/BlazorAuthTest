using MediatR;

namespace MwTech.Application.Products.ProductPropertiesVersions.Commands.CopyProductPropertiesVersion;

public class CopyProductPropertiesVersionCommand : IRequest
{
    public int ProductPropertiesVersionId { get; set; }
}
