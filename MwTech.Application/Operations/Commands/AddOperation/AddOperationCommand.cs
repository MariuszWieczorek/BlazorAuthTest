using MediatR;
using System.ComponentModel.DataAnnotations;


namespace MwTech.Application.Operations.Commands.AddOperation;

public class AddOperationCommand : IRequest
{
    [Required(ErrorMessage = "Pole 'Symbol' jest wymagane")]
    [Display(Name = "Symbol")]
    public string OperationNumber { get; set; }

    [Required(ErrorMessage = "Pole 'Nazwa' jest wymagane")]
    [Display(Name = "Nazwa")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Pole 'Opis' jest wymagane")]
    [Display(Name = "Opis")]
    public string? Description { get; set; }


    [Required(ErrorMessage = "Pole 'Kategoria' jest wymagane")]
    [Display(Name = "Kategoria")]
    public int ProductCategoryId { get; set; }


    [Required(ErrorMessage = "Pole 'JM' jest wymagane")]
    [Display(Name = "JM")]
    public int UnitId { get; set; }


    [Display(Name = "Numer Operacji")]
    public int No { get; set; }

}
