using MediatR;
using MwTech.Shared.Authentication.Dtos;
using System.ComponentModel.DataAnnotations;

namespace MwTech.Shared.Authentication.Commands;

public class LoginUserCommand : IRequest<LoginUserDto>
{
    [Required(ErrorMessage = "Pole 'E-mail' jest wymagane.")]
    [EmailAddress(ErrorMessage = "Pole 'E-mail' musi być prawidłowym adresem e-mail.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Pole 'Hasło' jest wymagane.")]
    public string Password { get; set; }
}
