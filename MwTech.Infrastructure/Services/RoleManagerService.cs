using FluentValidation.Results;
using MwTech.Application.Common.Exceptions;
using MwTech.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using MwTech.Domain.Entities;
using MwTech.Application.Identity.Roles.Queries.GetRoles;

namespace MwTech.Infrastructure.Services;
public class RoleManagerService : IRoleManagerService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public RoleManagerService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task CreateAsync(string roleName)
    {
        await ValidateRoleName(roleName);

        var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

        if (!result.Succeeded)
            throw new Exception(string.Join(". ", result.Errors.Select(x => x.Description)));
    }

    private async Task ValidateRoleName(string roleName)
    {
        if (await _roleManager.RoleExistsAsync(roleName))
            throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("Name", $"Rola o nazwie '{roleName}' już istnieje.") });
    }

    public IEnumerable<RoleDto> GetRoles()
    {
        return _roleManager.Roles.Select(x => new RoleDto { Id = x.Id, Name = x.Name }).ToList();
    }

    public async Task UpdateAsync(RoleDto role)
    {
        var roleDb = await _roleManager.FindByIdAsync(role.Id);

        if (roleDb.Name != role.Name)
            await ValidateRoleName(role.Name);

        roleDb.Name = role.Name;

        var result = await _roleManager.UpdateAsync(roleDb);

        foreach (string userId in role.IdsToAdd ?? new string[] { })
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                result = await _userManager.AddToRoleAsync(user, role.Name);
                if (!result.Succeeded)
                {
                }
            }
        }
        foreach (string userId in role.IdsToDelete ?? new string[] { })
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                result = await _userManager.RemoveFromRoleAsync(user, role.Name);

                if (!result.Succeeded)
                {
                }
            }
        }


        if (!result.Succeeded)
            throw new Exception(string.Join(". ", result.Errors.Select(x => x.Description)));
    }

    public async Task<RoleDto> FindByIdAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);

        if (role == null)
            throw new Exception($"Brak roli o podanym id: {id}.");

        return new RoleDto { Id = role.Id, Name = role.Name };
    }

    public async Task DeleteAsync(string id)
    {
        var roleDb = await _roleManager.FindByIdAsync(id);
        
        //await ValidateRoleToDelete(roleDb.Name);

        var result = await _roleManager.DeleteAsync(roleDb);

        if (!result.Succeeded)
            throw new Exception(string.Join(". ", result.Errors.Select(x => x.Description)));
    }


}
