using MediatR;
using MwTech.Domain.Entities;
using System.ComponentModel.DataAnnotations;


namespace MwTech.Application.Resources.Commands.EditResource;

public class EditResourceCommand : IRequest
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Pole 'Symbol' jest wymagane")]
    [Display(Name = "Symbol")]
    public string ResourceNumber { get; set; }

    
    [Required(ErrorMessage = "Pole 'Nazwa' jest wymagane")]
    [Display(Name = "Nazwa")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Pole 'Opis' jest wymagane")]
    [Display(Name = "Opis")]
    public string? Description { get; set; }


    [Required(ErrorMessage = "Pole 'Kategoria Produktu' jest wymagane")]
    [Display(Name = "Kategoria Produktu")]
    public int ProductCategoryId { get; set; }


    [Display(Name = "Koszt")]
    public decimal Cost { get; set; }

    [Display(Name = "Narzut %")]
    public Decimal Markup { get; set; }


    [Display(Name = "Szacowany Koszt")]
    public decimal EstimatedCost { get; set; }

    [Display(Name = "Szacowany %")]
    public Decimal EstimatedMarkup { get; set; }


    [Required(ErrorMessage = "Pole 'JM' jest wymagane")]
    [Display(Name = "Jm")]
    public int UnitId { get; set; }


    [Display(Name = "Umiejscowienie")]
    public string? Contract { get; set; }

    [Display(Name = "Domyślna Kategoria")]
    public int? LabourClassId { get; set; }

    [Display(Name = "Domyślna Kategoria")]
    public Resource? LabourClass { get; set; }

}
