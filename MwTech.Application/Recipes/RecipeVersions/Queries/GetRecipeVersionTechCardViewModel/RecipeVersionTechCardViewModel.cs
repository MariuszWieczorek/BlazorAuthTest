using MwTech.Application.Recipes.RecipeStages.Commands.EditRecipeStage;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.RecipeVersions.Queries.GetRecipeVersionTechCardViewModel;

public class RecipeVersionTechCardViewModel
{
    public IEnumerable<RecipeStage> RecipeStages { get; set; }
    public Recipe Recipe { get; set; }
    public RecipeVersion RecipeVersion { get; set; }

}
