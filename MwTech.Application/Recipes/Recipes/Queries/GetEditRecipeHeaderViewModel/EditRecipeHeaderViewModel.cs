using MwTech.Application.Recipes.Recipes.Commands.EditRecipe;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.Recipes.Queries.GetEditRecipeHeaderViewModel;

public class EditRecipeHeaderViewModel
{
    public EditRecipeCommand EditRecipeCommand { get; set; }
    public List<RecipeCategory> RecipeCategories { get; set; }
    public Recipe Recipe { get; set; }

}
