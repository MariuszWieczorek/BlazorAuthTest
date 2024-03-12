

using Microsoft.AspNetCore.Identity;
using MwTech.Domain.Entities;

namespace MwTech.Application.Common.Interfaces;
public interface IUserManagerService
{
    Task<string> CreateAsync(string name, string email, string password, string role);
    Task<IdentityResult> ResetPasswordAsync(string id);
    ApplicationUser GetUserByRfid(string rfid);
    Task<IdentityResult> PasswordValidateAsync(ApplicationUser user, string password);
    Task<IdentityResult> UserValidateAsync(ApplicationUser user);
    Task<bool> IsInRoleAsync(ApplicationUser user, string RoleName);
    string HashPassword(ApplicationUser user, string password);
}
