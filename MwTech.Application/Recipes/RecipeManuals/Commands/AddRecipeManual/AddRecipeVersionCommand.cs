using MediatR;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Recipes;
using System.ComponentModel.DataAnnotations;

namespace MwTech.Application.Recipes.RecipeManuals.Commands.AddRecipeManual;

public class AddRecipeManualCommand : IRequest
{

    [Required(ErrorMessage = "Pole 'RecipeId ' jest wymagane")]
    public int RecipeId { get; set; }

    [Required(ErrorMessage = "Pole 'RecipeVersionId ' jest wymagane")]
    public int RecipeVersionId { get; set; }


    [Required(ErrorMessage = "Pole 'RecipeStageId ' jest wymagane")]
    public int RecipeStageId { get; set; }

    [Display(Name = "Lp")]
    public int PositionNo { get; set; }

    [Display(Name = "Opis")]
    public string Description { get; set; }

    [Display(Name = "Czas [s]")]
    public decimal? Duration { get; set; }

    [Display(Name = "Tekst")]
    public string TextValue { get; set; }


}
