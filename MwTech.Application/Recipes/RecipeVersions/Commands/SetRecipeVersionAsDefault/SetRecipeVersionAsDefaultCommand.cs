using MediatR;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.SetRecipeVersionAsDefault;

public class SetRecipeVersionAsDefaultCommand : IRequest
{
    public int RecipeVersionId { get; set; }
    public int RecipeId { get; set; }
}
