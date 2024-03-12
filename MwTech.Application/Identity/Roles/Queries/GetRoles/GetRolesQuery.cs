using MediatR;

namespace MwTech.Application.Identity.Roles.Queries.GetRoles;
public class GetRolesQuery : IRequest<IEnumerable<RoleDto>>
{
}
