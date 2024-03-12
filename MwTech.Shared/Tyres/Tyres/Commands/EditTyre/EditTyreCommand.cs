using MediatR;
using System.ComponentModel.DataAnnotations;


namespace MwTech.Shared.Tyres.Tyres.Commands.EditTyre;

public class EditTyreCommand : IRequest
{
    [Display(Name = "Id")]
    public int Id { get; set; }

    [Display(Name = "Indeks")]
    public string TyreNumber { get; set; }

    [Display(Name = "Nazwa")]
    public string Name { get; set; }

    [Display(Name = "Opis")]
    public string? Description { get; set; }

    [Display(Name = "Średnica felgi w calach")]
    public decimal RimDiameterInInches { get; set; }

    [Display(Name = "Nośność - Indeks")]
    public int LoadIndex { get; set; }

    [Display(Name = "Nośność PR")]
    public int PlyRating { get; set; }
}
