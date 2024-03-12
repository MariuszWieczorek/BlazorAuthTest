using MediatR;

namespace MwTech.Application.Settings.Commands.DeleteSetting;

public class DeleteSettingCommand : IRequest
{
    public int Id { get; set; }
}
