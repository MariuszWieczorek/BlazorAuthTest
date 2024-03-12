using MwTech.Application.Recipes.RecipeStages.Commands.AddRecipeStage;
using MwTech.Application.Resources.Queries.GetResources;

namespace MwTech.Application.Recipes.RecipeStages.Queries.GetAddRecipeStageViewModel;

public class AddRecipeStageViewModel
{
    public AddRecipeStageCommand AddRecipeStageCommand { get; set; }
    public GetResourcesViewModel ResourcesViewModel { get; set; }

}
