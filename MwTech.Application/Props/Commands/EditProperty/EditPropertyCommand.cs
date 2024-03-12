using MediatR;
using System.ComponentModel.DataAnnotations;


namespace MwTech.Application.Props.Commands.EditProperty;

public class EditPropertyCommand : IRequest
{
    public int Id { get; set; }

    [Display(Name = "Lp")]
    public int OrdinalNo { get; set; }

    [Display(Name = "Symbol")]
    public string PropertyNumber { get; set; }

    [Required]
    [Display(Name = "Scada Symbol")]
    public string ScadaPropertyNumber { get; set; }

    [Display(Name = "Nazwa")]
    public string Name { get; set; }


    [Display(Name = "Opis")]
    public string? Description { get; set; }


    [Display(Name = "Kategoria")]
    public int ProductCategoryId { get; set; }

    [Display(Name = "Atrybut Ogólny")]
    public bool IsGeneralProperty { get; set; }


    [Display(Name = "Atrybut Wersji Produktu")]
    public bool IsVersionProperty { get; set; }

    [Display(Name = "Jm")]
    public int UnitId { get; set; }

    [Display(Name = "Ile miejsc dziesiętnych")]
    public int? DecimalPlaces { get; set; }

    [Display(Name = "Ukryj na wydruku")]
    public bool HideOnReport { get; set; }

}
