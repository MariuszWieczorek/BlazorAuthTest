using MediatR;

namespace MwTech.Application.Recipes.RecipePositionsPackages.Commands.DeleteRecipePositionsPackage;

public class DeleteRecipePositionsPackageCommand : IRequest
{
    public int RecipeId { get; set; }
    public int RecipeStageId { get; set; }
    public int RecipePositionsPackageId { get; set; }
    public int RecipeVersionId { get; set; }
}
