using MediatR;

namespace MwTech.Application.Recipes.RecipeVersions.Queries.GetAddRecipeVersionViewModel;

public class GetAddRecipeVersionViewModelQuery : IRequest<AddRecipeVersionViewModel>
{
    public int RecipeId { get; set; }
    public string Tab { get; set; }
}
