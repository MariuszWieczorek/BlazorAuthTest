using MediatR;
using System.ComponentModel.DataAnnotations;


namespace MwTech.Application.Machines.Commands.AddMachine;

public class AddMachineCommand : IRequest
{
    [Required(ErrorMessage = "Pole 'Symbol' jest wymagane")]
    [Display(Name = "Symbol")]
    public string MachineNumber { get; set; }

    [Required(ErrorMessage = "Pole 'Nazwa' jest wymagane")]
    [Display(Name = "Nazwa")]
    public string Name { get; set; }

    [Display(Name = "Numer ST")]
    public string? ReferenceNumber { get; set; }

    [Required(ErrorMessage = "Pole 'Opis' jest wymagane")]
    [Display(Name = "Opis")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Pole 'Kategoria' jest wymagane")]
    [Display(Name = "Kategoria")]
    public int MachineCategoryId { get; set; }

}
