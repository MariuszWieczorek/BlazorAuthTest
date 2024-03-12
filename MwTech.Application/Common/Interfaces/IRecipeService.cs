using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Common.Interfaces;

public interface IRecipeService
{
    Task<IEnumerable<RecipeStage>> GetRecipeVersionStages(int recipeVersionId);
    Task<RecipeSummary> GetRecipeSummary(int recipeId);
    Task<RecipeSummary> GetRecipeVersionSummary(int recipeId, int recipeVersionId);
}
