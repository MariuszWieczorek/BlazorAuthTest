using MwTech.Application.Identity.Roles.Commands.EditRole;
using MediatR;


namespace MwTech.Application.Identity.Roles.Queries.GetEditRole;
public class GetEditRoleQuery : IRequest<EditRoleViewModel>
{
    public string Id { get; set; }
}
