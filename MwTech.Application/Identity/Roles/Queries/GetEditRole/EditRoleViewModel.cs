using Microsoft.AspNetCore.Identity;
using MwTech.Application.Identity.Roles.Commands.EditRole;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Identity.Roles.Queries.GetEditRole;

public class EditRoleViewModel
{
    public EditRoleCommand EditRoleCommand { get; set; }
    public List<ApplicationUser> Members { get; set; }
    public List<ApplicationUser> NonMembers { get; set; }

}
