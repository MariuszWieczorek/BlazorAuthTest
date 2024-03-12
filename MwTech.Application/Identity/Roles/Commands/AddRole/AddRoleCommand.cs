using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace MwTech.Application.Identity.Roles.Commands.AddRole;
public class AddRoleCommand : IRequest
{
    [Required(ErrorMessage = "Pole 'Nazwa' jest wymagane.")]
    [DisplayName("Nazwa")]
    public string Name { get; set; }
}
