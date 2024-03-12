using MwTech.Application.Recipes.RecipeManuals.Commands.EditRecipeManual;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.RecipeManuals.Queries.GetEditRecipeManualViewModel;

public class EditRecipeManualViewModel
{
    public EditRecipeManualCommand EditRecipeManualCommand { get; set; }
    public List<RecipeStage> RecipeStages { get; set; }

}
