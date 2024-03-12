using MediatR;

namespace MwTech.Application.Products.ProductSettingVersions.Commands.DeleteProductSettingVersion;

public class DeleteProductSettingVersionCommand : IRequest
{
    public int ProductId { get; set; }
    public int ProductSettingVersionId { get; set; }

}
