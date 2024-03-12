using MediatR;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.AcceptRecipeVersionLevel1;

public class AcceptRecipeVersionLevel1Command : IRequest
{
    public int RecipeVersionId { get; set; }
    public int RecipeId { get; set; }
}
