using MediatR;
using MwTech.Application.Products.Common;

namespace MwTech.Application.Recipes.RecipePositions.Queries.GetEditRecipePositionViewModel;

public class GetEditRecipePositionViewModelQuery : IRequest<EditRecipePositionViewModel>
{
    public int RecipePositionId { get; set; }
    public int RecipeStageId { get; set; }
    public string Tab { get; set; }
    public ProductFilter ProductFilter { get; set; }
}
