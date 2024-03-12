using Microsoft.AspNetCore.Identity;
using MwTech.Domain.Entities;

namespace MwTech.Infrastructure.Identity;

/* Zaimplementowanie od zera własnej polityki kontroli poprawności hasła
 * zamiast klasy, która jest domyślnie używana do tego celu, czyli PasswordValidator
 * Funkcjonalność kontroli poprawności hasła jest zdefiniowana przez interfejs
 * IPasswordValidator<T> z przestrzeni nazw Microsoft.AspNetCore.Identity */
public class CustomUserValidator2 : UserValidator<ApplicationUser>
{
    public override async Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
    {

        IdentityResult result = await base.ValidateAsync(manager, user);

        List<IdentityError> errors = result.Succeeded ? new List<IdentityError>() : result.Errors.ToList();

        /*
        if (!user.Email.ToLower().EndsWith("@example.com"))
        {
            errors.Add(new IdentityError
            {
                Code = "EmailDomainError",
                Description = "Only example.com email addresses are allowed"
            });
        }
        */

        return errors.Count == 0 ? IdentityResult.Success: IdentityResult.Failed(errors.ToArray());

    }
}
