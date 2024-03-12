using Microsoft.AspNetCore.Identity;
using MwTech.Domain.Entities;
using System.Security.Claims;

namespace MwTech.Application.Common.Interfaces;

public interface IAuthenticationService
{
    Task<string> GetToken(ApplicationUser user);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
