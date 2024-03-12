using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MwTech.Application.Identity.Roles.Commands.EditRole;
public class EditRoleCommand : IRequest
{
    public string Id { get; set; }

    [Required(ErrorMessage = "Pole 'Nazwa' jest wymagane.")]
    [DisplayName("Nazwa")]
    public string Name { get; set; }
    public string[] IdsToAdd { get; set; }
    public string[] IdsToDelete { get; set; }
}
