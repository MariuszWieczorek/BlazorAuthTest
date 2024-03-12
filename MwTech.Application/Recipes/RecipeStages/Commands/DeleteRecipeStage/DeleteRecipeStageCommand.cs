using MediatR;

namespace MwTech.Application.Recipes.RecipeStages.Commands.DeleteRecipeStage;

public class DeleteRecipeStageCommand : IRequest
{
    public int RecipeId { get; set; }
    public int RecipeStageId { get; set; }
    public int RecipeVersionId { get; set; }
}
