using MwTech.Application.Recipes.RecipeVersions.Commands.EditRecipeVersion;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.RecipeVersions.Queries.GetEditRecipeVersionViewModel;

public class EditRecipeVersionViewModel
{
    public EditRecipeVersionCommand EditRecipeVersionCommand { get; set; }
    public IEnumerable<RecipeStage> RecipeStages { get; set; }
    public IEnumerable<RecipeManual> RecipeManuals { get; set; }
}
