using MediatR;

namespace MwTech.Application.Recipes.Recipes.Queries.GetEditRecipeViewModel;

public class GetEditRecipeViewModelQuery : IRequest<EditRecipeViewModel>
{
    public int RecipeId { get; set; }
    public string? Tab { get; set;}
}
