using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Identity.Roles.Queries.GetRoles;
using MwTech.Application.Identity.Users.Commands.EditUser;

namespace MwTech.Application.Identity.Users.Queries.GetEditUserViewModel;

public class GetEditUserViewModelQueryHandler : IRequestHandler<GetEditUserViewModelQuery, EditUserViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserRoleManagerService _userRoleManagerService;
    private readonly IRoleManagerService _roleManagerService;

    public GetEditUserViewModelQueryHandler(
        IApplicationDbContext context,
        IUserRoleManagerService userRoleManagerService,
        IRoleManagerService roleManagerService
        )
    {
        _context = context;
        _userRoleManagerService = userRoleManagerService;
        _roleManagerService = roleManagerService;
    }

    public async Task<EditUserViewModel> Handle(GetEditUserViewModelQuery request, CancellationToken cancellationToken)
    {


        var userToUpdate = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == request.Id);


        var rolesIdList = (await _userRoleManagerService
                    .GetRolesAsync(request.Id))
                    .Select(x => x.Id).ToList();

        List<RoleDto> availableRoles = _roleManagerService.GetRoles().ToList();

        var editUserCommand = new EditUserCommand
        {
            Id = request.Id,
            FirstName = userToUpdate.FirstName,
            LastName = userToUpdate.LastName,
            Possition = userToUpdate.Possition,
            PhoneNumber = userToUpdate.PhoneNumber,
            ReferenceNumber = userToUpdate.ReferenceNumber,
            AdminRights = userToUpdate.AdminRights,
            SuperAdminRights = userToUpdate.SuperAdminRights,
            EmailConfirmed = userToUpdate.EmailConfirmed,
            Rfid = userToUpdate.Rfid,
            UserName = userToUpdate.UserName,
            Email = userToUpdate.Email,
            RolesIdList = rolesIdList,
        };

        var vm = new EditUserViewModel
        {
            EditUserCommand = editUserCommand,
            AvailableRoles = availableRoles
        };

        return vm;
    }
}




