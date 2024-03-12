using MediatR;

namespace MwTech.Application.Recipes.Recipes.Commands.CopyRecipe;

public class CopyRecipeCommand : IRequest
{
    public int RecipeId { get; set; }
}
