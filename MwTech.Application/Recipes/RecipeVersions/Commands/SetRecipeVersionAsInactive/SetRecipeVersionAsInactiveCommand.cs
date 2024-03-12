using MediatR;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.SetRecipeVersionAsInactive;

public class SetRecipeVersionAsInactiveCommand : IRequest
{
    public int RecipeVersionId { get; set; }
    public int RecipeId { get; set; }
}
