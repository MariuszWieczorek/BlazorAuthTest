using FluentValidation.Results;
using MwTech.Application.Common.Exceptions;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MwTech.Infrastructure.Services;
public class UserManagerService : IUserManagerService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationDbContext _context;
    private readonly IUserStore<ApplicationUser> _userStore;
    private readonly IPasswordValidator<ApplicationUser> _passwordValidator;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
    private readonly IUserValidator<ApplicationUser> _userValidator;
    private readonly IUserEmailStore<ApplicationUser> _userEmailStore;

    public UserManagerService(
        UserManager<ApplicationUser> userManager,
        IApplicationDbContext context,
        IUserStore<ApplicationUser> userStore,
        IPasswordValidator<ApplicationUser> passwordValidator,
        IPasswordHasher<ApplicationUser> passwordHasher,
        IUserValidator<ApplicationUser> userValidator
        )
    {
        _userManager = userManager;
        _context = context;
        _userStore = userStore;
        _passwordValidator = passwordValidator;
        _passwordHasher = passwordHasher;
        _userValidator = userValidator;
        _userEmailStore = GetEmailStore();
    }

    public async Task<string> CreateAsync(string name, string email, string password, string role)
    {
        var user = new ApplicationUser();
        var userName = name ?? email;


        await _userStore.SetUserNameAsync(user, userName, CancellationToken.None);

        await _userEmailStore.SetEmailAsync(user, email, CancellationToken.None);

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            foreach (var item in result.Errors)
            {
                throw new ValidationException(new List<ValidationFailure>
                {
                    new ValidationFailure(item.Code, item.Description)
                });
            }
        }

        if (!string.IsNullOrWhiteSpace(role))
            await _userManager.AddToRoleAsync(user, role);

        return await _userManager.GetUserIdAsync(user);
    }

    private IUserEmailStore<ApplicationUser> GetEmailStore()
    {
        if (!_userManager.SupportsUserEmail)
            throw new NotSupportedException("The default UI requires a user store with email supprt");

        return (IUserEmailStore<ApplicationUser>)_userStore;
    }

    public async Task<IdentityResult> ResetPasswordAsync(string id)
    {

        string password = "mwtech";
        var user = await _userManager.FindByIdAsync(id);
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, password);
        return result;
    }


    public async Task<IdentityResult> PasswordValidateAsync(ApplicationUser user, string password)
    {
        IdentityResult result = null;
        result = await _passwordValidator.ValidateAsync(_userManager,user, password);
        return result;
    }

    public async Task<IdentityResult> UserValidateAsync(ApplicationUser user)
    {
        IdentityResult result = null;
        result = await _userValidator.ValidateAsync(_userManager, user);
        return result;
    }

    public string HashPassword(ApplicationUser user, string password)
    {
        var result = _passwordHasher.HashPassword(user,password);
        return result;
    }


    public ApplicationUser GetUserByRfid(string rfid)
    {
        
        if (rfid.StartsWith(";"))
        {
            rfid = rfid.Substring(1);
        }
        var user = _context.Users.FirstOrDefault(x => x.Rfid == rfid);
        return user;
    }

    public async Task<bool> IsInRoleAsync(ApplicationUser user, string RoleName)
    {
        return await _userManager.IsInRoleAsync(user, RoleName);
    }

}
