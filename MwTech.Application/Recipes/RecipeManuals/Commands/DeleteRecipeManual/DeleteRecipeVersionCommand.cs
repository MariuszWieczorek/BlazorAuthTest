using MediatR;

namespace MwTech.Application.Recipes.RecipeManuals.Commands.DeleteRecipeManual;

public class DeleteRecipeManualCommand : IRequest
{
    public int RecipeId { get; set; }
    public int RecipeManualId { get; set; }
    public int RecipeVersionId { get; set; }
}
