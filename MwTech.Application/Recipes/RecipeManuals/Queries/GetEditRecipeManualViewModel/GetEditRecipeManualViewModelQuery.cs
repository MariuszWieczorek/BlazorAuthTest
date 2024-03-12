using MediatR;

namespace MwTech.Application.Recipes.RecipeManuals.Queries.GetEditRecipeManualViewModel;

public class GetEditRecipeManualViewModelQuery : IRequest<EditRecipeManualViewModel>
{
    public int RecipeManualId { get; set; }
    public int RecipeVersionId { get; set; }
    public string Tab { get; set; }
}
