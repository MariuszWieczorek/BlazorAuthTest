using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MwTech.Application.Identity.Users.Commands.AddUser;

public class AddUserCommand : IRequest
{

    [Required]
    [Display(Name = "Imię")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Nazwisko")]
    public string LastName { get; set; }


    [Display(Name = "Nazwa Użytkownika")]
    public string UserName { get; set; }

    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Display(Name = "Stanowisko")]
    public string Possition { get; set; }

    [Required(ErrorMessage = "Pole 'Hasło' jest wymagane.")]
    [DataType(DataType.Password)]
    [Display(Name = "Hasło")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "Hasło i Potwierdzone hasło są różne.")]
    public string ConfirmPassword { get; set; }


    [Display(Name = "Numer ewidencyjny")]
    public string ReferenceNumber { get; set; }

    [Display(Name = "Numer telefonu")]
    public string PhoneNumber { get; set; }

    [Display(Name = "Kod RFID")]
    public string Rfid { get; set; }

    [Display(Name = "Admin")]
    public bool AdminRights { get; set; }

    [Display(Name = "Super Admin")]
    public bool SuperAdminRights { get; set; }

    [Display(Name = "Potwierdzony")]
    public bool EmailConfirmed { get; set; }

}

// [StringLength(100, ErrorMessage = "Hasło musi mięć co najmniej {2} znaków i nie więcej niż {1} znaków długości.", MinimumLength = 8)]