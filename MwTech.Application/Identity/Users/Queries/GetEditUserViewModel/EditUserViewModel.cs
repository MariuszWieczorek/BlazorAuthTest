using MwTech.Application.Identity.Roles.Queries.GetRoles;
using MwTech.Application.Identity.Users.Commands.EditUser;

namespace MwTech.Application.Identity.Users.Queries.GetEditUserViewModel;

public class EditUserViewModel
{
    public EditUserCommand EditUserCommand { get; set; }
    public List<RoleDto> AvailableRoles { get; set; }

}
