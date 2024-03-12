using MediatR;

namespace MwTech.Application.Identity.Roles.Commands.DeleteRole;
public class DeleteRoleCommand : IRequest
{
    public string Id { get; set; }
}
