using MediatR;
using System.ComponentModel.DataAnnotations;


namespace MwTech.Application.Recipes.RecipeCategories.Commands.AddRecipeCategory;

public class AddRecipeCategoryCommand : IRequest
{
    [Display(Name = "Lp")]
    public int OrdinalNumber { get; set; }

    [Required]
    [Display(Name = "Nazwa")]
    public string Name { get; set; }

    [Display(Name = "Symbol")]
    public string CategoryNumber { get; set; }


    [Display(Name = "Opis")]
    public string Description { get; set; }

}
