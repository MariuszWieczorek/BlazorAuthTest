using System.ComponentModel.DataAnnotations;

namespace MwTech.Application.Recipes.RecipeCategories.Queries.GetRecipeCategories;

public class RecipeCategoryFilter
{
    [Display(Name = "Nazwa")]
    public string Name { get; set; }

    [Display(Name = "Symbol")]
    public string RecipeCategoryNumber { get; set; }

}
