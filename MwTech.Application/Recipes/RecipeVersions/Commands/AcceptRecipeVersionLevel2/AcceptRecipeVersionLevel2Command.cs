using MediatR;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.AcceptRecipeVersionLevel2;

public class AcceptRecipeVersionLevel2Command : IRequest
{
    public int RecipeVersionId { get; set; }
    public int RecipeId { get; set; }
}
