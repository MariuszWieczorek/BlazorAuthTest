using MediatR;

namespace MwTech.Application.Recipes.RecipePositions.Commands.DeleteRecipePosition;

public class DeleteRecipePositionCommand : IRequest
{
    public int RecipeId { get; set; }
    public int RecipeVersionId { get; set; }
    public int RecipeStageId { get; set; }
    public int RecipePositionId { get; set; }
}
