using MediatR;

namespace MwTech.Application.Recipes.RecipeVersions.Queries.GetEditRecipeVersionViewModel;

public class GetEditRecipeVersionViewModelQuery : IRequest<EditRecipeVersionViewModel>
{
    public int RecipeVersionId { get; set; }
    public int RecipeId { get; set; }
    public string Tab { get; set; }
}
