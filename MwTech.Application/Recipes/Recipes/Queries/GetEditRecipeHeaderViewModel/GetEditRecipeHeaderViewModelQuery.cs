using MediatR;

namespace MwTech.Application.Recipes.Recipes.Queries.GetEditRecipeHeaderViewModel;

public class GetEditRecipeHeaderViewModelQuery : IRequest<EditRecipeHeaderViewModel>
{
    public int RecipeId { get; set; }
    public string? Tab { get; set; }
}
