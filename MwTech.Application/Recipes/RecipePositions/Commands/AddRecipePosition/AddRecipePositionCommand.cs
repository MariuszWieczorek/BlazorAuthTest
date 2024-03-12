using MediatR;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Recipes;
using System.ComponentModel.DataAnnotations;

namespace MwTech.Application.Recipes.RecipePositions.Commands.AddRecipePosition;

public class AddRecipePositionCommand : IRequest
{

    [Required(ErrorMessage = "Pole 'RecipeStageId ' jest wymagane")]
    public int RecipeStageId { get; set; }

    [Required(ErrorMessage = "Pole 'RecipeVersionId ' jest wymagane")]
    public int RecipeVersionId { get; set; }

    [Required(ErrorMessage = "Pole 'RecipeId ' jest wymagane")]
    public int RecipeId { get; set; }


    [Display(Name = "Lp")]
    public int PositionNo { get; set; }

    [Display(Name = "Naw")]
    public int PacketNo { get; set; }

    [Display(Name = "Produkt")]
    [Required(ErrorMessage = "Pole 'ProductId' jest wymagane")]
    public int ProductId { get; set; }

    public Product Product { get; set; }

    [Display(Name = "Ilość")]
    [Required(ErrorMessage = "Pole 'Ilość' jest wymagane")]
    public decimal ProductQty { get; set; }

    [Display(Name = "Naważka")]
    public int? RecipePositionPackageId { get; set; }

    [Display(Name = "Opis")]
    public string Description { get; set; }

    [Display(Name = "Nawrót")]
    public bool ReturnFromProcessing { get; set; }

}
