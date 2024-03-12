using MwTech.Application.Identity.Roles.Queries.GetRoles;

namespace MwTech.Application.Common.Interfaces;
public interface IRoleManagerService
{
    IEnumerable<RoleDto> GetRoles();
    Task CreateAsync(string roleName);
    Task UpdateAsync(RoleDto role);
    Task<RoleDto> FindByIdAsync(string id);
    Task DeleteAsync(string id);
}
