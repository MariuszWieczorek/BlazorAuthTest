using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MwTech.Application.Identity.Users.Commands.EditUser;

public class EditUserCommand : IRequest<List<string>>
{
    public string Id { get; set; }

    [Required]
    [Display(Name = "Imię")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Nazwisko")]
    public string LastName { get; set; }


    [Display(Name = "Hasło")]
    public string Password { get; set; }

    [Display(Name = "Nazwa Użytkownika")]
    public string UserName { get; set; }

    [Display(Name = "Email")]
    public string Email { get; set; }


    [Display(Name = "Stanowisko")]
    public string Possition { get; set; }

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


    [Display(Name = "Wybrane role")]
    public List<string> RolesIdList { get; set; } = new List<string>();

}