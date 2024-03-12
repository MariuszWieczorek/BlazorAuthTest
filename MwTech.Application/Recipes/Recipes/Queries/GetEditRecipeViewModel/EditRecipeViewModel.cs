using MwTech.Application.Recipes.Recipes.Commands.EditRecipe;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.Recipes.Queries.GetEditRecipeViewModel;

public class EditRecipeViewModel
{
    public EditRecipeCommand EditRecipeCommand { get; set; }
    public List<RecipeCategory> RecipeCategories { get; set; }
    public List<RecipeVersion> RecipeVersions { get; set; }
    public List<Unit> Units { get; set; }

}
