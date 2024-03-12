using MediatR;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.CopyRecipeVersion;

public class CopyRecipeVersionCommand : IRequest
{
    public int RecipeId { get; set; }
    public int RecipeVersionId { get; set; }
}
