using MediatR;

namespace MwTech.Application.Units.Commands.DeleteUnit;

public class DeleteUnitCommand : IRequest
{
    public int Id { get; set; }
}
