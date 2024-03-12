using Microsoft.AspNetCore.Identity;
using MwTech.Domain.Entities;

namespace MwTech.Infrastructure.Identity;

/* Zaimplementowanie od zera własnej polityki kontroli poprawności hasła
 * zamiast klasy, która jest domyślnie używana do tego celu, czyli PasswordValidator
 * Funkcjonalność kontroli poprawności hasła jest zdefiniowana przez interfejs
 * IPasswordValidator<T> z przestrzeni nazw Microsoft.AspNetCore.Identity */
public class CustomPasswordValidator : IPasswordValidator<ApplicationUser>
{
    public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string password)
    {
        List<IdentityError> errors = new List<IdentityError>();

        if (password.ToLower().Contains(user.UserName.ToLower()))
        {
            errors.Add(new IdentityError
            {
                Code = "PasswordContainsSequence",
                Description = "Hasło nie może zawierać nazwy użytkownika"
            });
        }

        if (password.Contains("12345"))
        {
            errors.Add(
                new IdentityError
                {
                    Code = "PasswordContainsSequence",
                    Description = "Hasło nie może zawierać sekwencji 12345."
                });

        }


        return Task.FromResult(errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
    }
}
