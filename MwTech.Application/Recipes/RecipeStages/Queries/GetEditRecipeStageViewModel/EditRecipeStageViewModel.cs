using MwTech.Application.Recipes.RecipeStages.Commands.EditRecipeStage;
using MwTech.Application.Resources.Queries.GetResources;
using MwTech.Domain.Entities;

namespace MwTech.Application.Recipes.RecipeStages.Queries.GetEditRecipeStageViewModel;

public class EditRecipeStageViewModel
{
    public EditRecipeStageCommand EditRecipeStageCommand { get; set; }
    public GetResourcesViewModel ResourcesViewModel { get; set; }

}
