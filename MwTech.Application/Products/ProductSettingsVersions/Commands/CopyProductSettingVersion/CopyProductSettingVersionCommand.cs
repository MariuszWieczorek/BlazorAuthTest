using MediatR;

namespace MwTech.Application.ProductSettingsVersions.Commands.CopyProductSettingVersion;

public class CopyProductSettingVersionCommand : IRequest
{
    public int ProductSettingVersionId { get; set; }
    public int ProductId { get; set; }

}
