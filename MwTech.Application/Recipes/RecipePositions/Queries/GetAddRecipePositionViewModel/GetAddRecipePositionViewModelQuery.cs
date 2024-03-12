using MediatR;
using MwTech.Application.Products.Common;

namespace MwTech.Application.Recipes.RecipePositions.Queries.GetAddRecipePositionViewModel;

public class GetAddRecipePositionViewModelQuery : IRequest<AddRecipePositionViewModel>
{
    public int RecipeStageId { get; set; }
    public string Tab { get; set; }
    public ProductFilter ProductFilter { get; set; }
}
