using MwTech.Application.Recipes.Recipes.Commands.AddRecipe;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.Recipes.Queries.GetAddRecipeViewModel;

public class AddRecipeViewModel
{
    public AddRecipeCommand AddRecipeCommand { get; set; }
    public List<RecipeCategory> RecipeCategories { get; set; }
    public List<Unit> Units { get; set; }

}
