using Microsoft.AspNetCore.Identity;
using MwTech.Domain.Entities;

namespace MwTech.Infrastructure.Identity;

/* Zaimplementowanie od zera własnej polityki kontroli poprawności hasła
 * zamiast klasy, która jest domyślnie używana do tego celu, czyli PasswordValidator
 * Funkcjonalność kontroli poprawności hasła jest zdefiniowana przez interfejs
 * IPasswordValidator<T> z przestrzeni nazw Microsoft.AspNetCore.Identity */
public class CustomUserValidator : IUserValidator<ApplicationUser>
{
    public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
    {
        
            if (user.Email.ToLower().EndsWith("@example.com"))
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "EmailDomainError",
                    Description = "Only example.com email addresses are allowed"
                }));
            }

        
    }
}
