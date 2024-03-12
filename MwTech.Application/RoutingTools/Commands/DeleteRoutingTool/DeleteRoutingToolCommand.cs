using MediatR;

namespace MwTech.Application.RoutingTools.Commands.DeleteRoutingTool;

public class DeleteRoutingToolCommand : IRequest
{
    public int Id { get; set; }
}
