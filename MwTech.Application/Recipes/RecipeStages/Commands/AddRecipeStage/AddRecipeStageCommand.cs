using MediatR;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Recipes;
using System.ComponentModel.DataAnnotations;

namespace MwTech.Application.Recipes.RecipeStages.Commands.AddRecipeStage;

public class AddRecipeStageCommand : IRequest
{

    [Required(ErrorMessage = "Pole 'RecipeId ' jest wymagane")]
    public int RecipeId { get; set; }

    [Required(ErrorMessage = "Pole 'RecipeVersionId ' jest wymagane")]
    public int RecipeVersionId { get; set; }

    [Display(Name = "Numer Cyklu")]
    [Range(0, int.MaxValue, ErrorMessage = "Wersja musi być liczbą większą od zera")]
    [RegularExpression("([0-9]+)", ErrorMessage = "Wersja musi być liczbą.")]
    public int StageNo { get; set; }

    [Display(Name = "Nazwa Cyklu")]
    public string StageName { get; set; }

    [Display(Name = "Indeks Produktu")]
    public string ProductNumber { get; set; }

    [Display(Name = "Nazwa Produktu")]
    public string ProductName { get; set; }

    [Display(Name = "Gniazdo Robocze")]
    public int? WorkCenterId { get; set; }

    [Display(Name = "Gniazdo")]
    public Resource WorkCenter { get; set; }

    [Display(Name = "Kategoria zaszeregowania")]
    public int? LabourClassId { get; set; }

    [Display(Name = "Kategoria zaszeregowania")]
    public Resource LabourClass { get; set; }

    [Display(Name = "Opis")]
    public string Description { get; set; }

    [Display(Name = "Objętość Miksera")]
    public decimal MixerVolume { get; set; }

    [Display(Name = "Ilość z poprzedniego Cyklu")]
    public decimal? PrevStageQty { get; set; }

    [Display(Name = "Wielkość Brygady")]
    public decimal? CrewSize { get; set; }


    [Display(Name = "Pracochłonność")]
    public decimal? LabourRunFactor { get; set; }


    [Display(Name = "Czas w sekundach")]
    public int StageTimeInSeconds { get; set; }

}
