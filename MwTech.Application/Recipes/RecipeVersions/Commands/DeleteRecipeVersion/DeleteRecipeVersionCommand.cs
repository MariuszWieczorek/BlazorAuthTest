using MediatR;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.DeleteRecipeVersion;

public class DeleteRecipeVersionCommand : IRequest
{
    public int RecipeVersionId { get; set; }
    public int RecipeId { get; set; }
}
