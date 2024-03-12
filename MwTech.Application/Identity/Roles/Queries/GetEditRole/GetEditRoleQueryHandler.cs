using MwTech.Application.Common.Interfaces;
using MwTech.Application.Identity.Roles.Commands.EditRole;
using MediatR;
using MwTech.Domain.Entities;

namespace MwTech.Application.Identity.Roles.Queries.GetEditRole;
public class GetEditRoleQueryHandler : IRequestHandler<GetEditRoleQuery, EditRoleViewModel>
{
    private readonly IRoleManagerService _roleManagerService;
    private readonly IUserManagerService _userManager;
    private readonly IApplicationDbContext _context;

    public GetEditRoleQueryHandler(IRoleManagerService roleManagerService, IUserManagerService userManager, IApplicationDbContext context)
    {
        _roleManagerService = roleManagerService;
        _userManager = userManager;
        _context = context;
    }

    public async Task<EditRoleViewModel> Handle(GetEditRoleQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleManagerService.FindByIdAsync(request.Id);

        List<ApplicationUser> members = new List<ApplicationUser>();
        List<ApplicationUser> nonMembers = new List<ApplicationUser>();
        var users = _context.Users;

        foreach (ApplicationUser user in users)
        {
            var list = await _userManager.IsInRoleAsync(user, role.Name)
            ? members : nonMembers;

            list.Add(user);
        }

        var vm = new EditRoleViewModel
        {
            EditRoleCommand = new EditRoleCommand { Id = role.Id, Name = role.Name },
            Members = members,
            NonMembers = nonMembers
        };


        return vm;
    }
}
