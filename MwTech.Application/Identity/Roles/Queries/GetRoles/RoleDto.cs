namespace MwTech.Application.Identity.Roles.Queries.GetRoles;
public class RoleDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string[] IdsToAdd { get; set; }
    public string[] IdsToDelete { get; set; }
}
