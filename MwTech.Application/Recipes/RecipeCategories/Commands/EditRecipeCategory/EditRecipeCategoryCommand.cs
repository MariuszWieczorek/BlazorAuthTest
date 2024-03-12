using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MwTech.Application.Recipes.RecipeCategories.Commands.EditRecipeCategory;
public class EditRecipeCategoryCommand : IRequest
{
    public int Id { get; set; }

    [Display(Name = "Lp")]
    public int OrdinalNumber { get; set; }

    [Required]
    [Display(Name = "Nazwa")]
    public string Name { get; set; }

    [Display(Name = "Symbol")]
    public string CategoryNumber { get; set; }

    public string Description { get; set; }


}
