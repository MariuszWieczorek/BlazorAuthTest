using MwTech.Application.Recipes.RecipeManuals.Commands.AddRecipeManual;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.RecipeManuals.Queries.GetAddRecipeManualViewModel;

public class AddRecipeManualViewModel
{
    public AddRecipeManualCommand AddRecipeManualCommand { get; set; }

    public List<RecipeStage> RecipeStages { get; set; }

}
