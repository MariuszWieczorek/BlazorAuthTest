using MediatR;

namespace MwTech.Application.Props.Commands.DeleteProperty;

public class DeletePropertyCommand : IRequest
{
    public int Id { get; set; }
}
