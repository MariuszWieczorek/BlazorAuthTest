using MwTech.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MwTech.Application.Common.Interfaces;
public interface IUserClaimManagerService
{
    IEnumerable<Claim> GetClaims(ClaimsPrincipal user);
    
}
