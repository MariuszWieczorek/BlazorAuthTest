using Microsoft.AspNetCore.Identity;
using MwTech.Domain.Entities;

namespace MwTech.Infrastructure.Identity;

/* Zaimplementowanie własnej polityki kontroli poprawności hasła na podstawie wbudowanej klasy */
/* Która jest domyślnie używana do tego celu, czyli PasswordValidator z przestrzeni nazw Microsoft.AspNetCore.Identity */
/* Nasza klasa tym razem dziedziczy po klasie domyślnej i wykorzystuje oferowaną przez nią funkcjonalność */
public class CustomPasswordValidator2 : PasswordValidator<ApplicationUser>
{
    public override async Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string password)
    {

        IdentityResult result = await base.ValidateAsync(manager,user,password);

        List<IdentityError> errors = result.Succeeded ? new List<IdentityError>() : result.Errors.ToList();

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


        return errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
    }
}
