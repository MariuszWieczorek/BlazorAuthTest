using MediatR;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.SetRecipeVersionAsActive;

public class SetRecipeVersionAsActiveCommand : IRequest
{
    public int RecipeVersionId { get; set; }
    public int RecipeId { get; set; }
}
