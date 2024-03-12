using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Extensions;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;

namespace MwTech.Application.Identity.Users.Commands.EditUser;

public class EditUserCommandHandler : IRequestHandler<EditUserCommand, List<string>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserManagerService _userManagerService;
    private readonly IUserRoleManagerService _userRoleManagerService;
    private readonly IRoleManagerService _roleManagerService;
    private readonly IDateTimeService _dateTimeService;
    private readonly UserManager<ApplicationUser> _userManager;

    public EditUserCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IUserManagerService userManagerService
        , IUserRoleManagerService userRoleManagerService
        , IRoleManagerService roleManagerService
        , IDateTimeService dateTimeService
        , UserManager<ApplicationUser> userManager
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _userManagerService = userManagerService;
        _userRoleManagerService = userRoleManagerService;
        _roleManagerService = roleManagerService;
        _dateTimeService = dateTimeService;
        _userManager = userManager;
    }

    public async Task<List<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        // zwracamy listę z błędami
        // dzięki temu zostaną one wyświetlone na widoku

        var errors = new List<string>();

        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        user.UserName = request.UserName;
        user.Email = request.Email;


        IdentityResult validEmail = await _userManagerService.UserValidateAsync(user);
        if (!validEmail.Succeeded)
        {
            foreach (IdentityError error in validEmail.Errors)
            {
                errors.Add(error.Description);
            }
        }

        IdentityResult validPass = null;
        if (!string.IsNullOrEmpty(request.Password))
        {
            validPass = await _userManagerService.PasswordValidateAsync(user, request.Password);
            if (validPass.Succeeded)
            {
                user.PasswordHash = _userManagerService.HashPassword(user, request.Password);
            }
            else
            {

                foreach (IdentityError error in validPass.Errors)
                {
                    errors.Add(error.Description);
                }
            }

        }


        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Possition = request.Possition;
        user.PhoneNumber = request.PhoneNumber;
        user.ReferenceNumber = request.ReferenceNumber;
        user.AdminRights = request.AdminRights;
        user.SuperAdminRights = request.SuperAdminRights;
        user.EmailConfirmed = request.EmailConfirmed;
        user.Rfid = request.Rfid;


        await _context.SaveChangesAsync(cancellationToken);


        if (request.RolesIdList != null && request.RolesIdList.Any())
            await UpdateRoles(request.RolesIdList, request.Id);

        return errors;
    }

    private async Task UpdateRoles(List<string> newRoleIds, string userId)
    {
        var roles = _roleManagerService.GetRoles().Select(x => new IdentityRole { Id = x.Id, Name = x.Name });

        var oldRoles = await GetOldRoles(userId);
        var newRoles = GetNewRoles(newRoleIds, roles);

        await RemoveRoles(userId, oldRoles, newRoles);

        await AddNewRoles(userId, oldRoles, newRoles);
    }

    private async Task AddNewRoles(string userId, List<IdentityRole> oldRoles, List<IdentityRole> newRoles)
    {
        var roleToAdd = newRoles.Except(oldRoles, x => x.Id);

        foreach (var role in roleToAdd)
            await _userRoleManagerService.AddToRoleAsync(userId, role.Name);
    }

    private async Task RemoveRoles(string userId, List<IdentityRole> oldRoles, List<IdentityRole> newRoles)
    {
        var roleToRemove = oldRoles.Except(newRoles, x => x.Id);

        foreach (var role in roleToRemove)
            await _userRoleManagerService.RemoveFromRoleAsync(userId, role.Name);
    }

    private List<IdentityRole> GetNewRoles(List<string> newRoleIds, IEnumerable<IdentityRole> roles)
    {
        var newRoles = new List<IdentityRole>();

        foreach (var roleId in newRoleIds)
            newRoles.Add(new IdentityRole { Id = roleId, Name = roles.FirstOrDefault(x => x.Id == roleId).Name });

        return newRoles;
    }

    private async Task<List<IdentityRole>> GetOldRoles(string userId)
    {
        return (await _userRoleManagerService.GetRolesAsync(userId)).ToList();
    }

    /*
    private void AddErrorsFromResult(IdentityResult result)
    {
        foreach (IdentityError error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }
    }
    */
}
