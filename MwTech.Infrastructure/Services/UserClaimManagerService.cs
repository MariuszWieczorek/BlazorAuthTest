using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Linq.Dynamic.Core;

namespace MwTech.Infrastructure.Services;
public class UserClaimManagerService : IUserClaimManagerService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserClaimManagerService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    

    public IEnumerable<Claim> GetClaims(ClaimsPrincipal user)
    {

        var claims = user.Claims.OrderBy(x => x.Type).ToList();

        return claims;
    }

}
