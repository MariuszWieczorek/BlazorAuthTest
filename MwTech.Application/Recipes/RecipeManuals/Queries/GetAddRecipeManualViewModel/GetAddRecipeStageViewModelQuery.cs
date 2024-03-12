using MediatR;

namespace MwTech.Application.Recipes.RecipeManuals.Queries.GetAddRecipeManualViewModel;

public class GetAddRecipeManualViewModelQuery : IRequest<AddRecipeManualViewModel>
{
    public int RecipeVersionId { get; set; }
    public string Tab { get; set; }
}
