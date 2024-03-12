using MediatR;
using MwTech.Application.Resources.Queries.GetResources;

namespace MwTech.Application.Recipes.RecipeStages.Queries.GetAddRecipeStageViewModel;

public class GetAddRecipeStageViewModelQuery : IRequest<AddRecipeStageViewModel>
{
    public int RecipeVersionId { get; set; }
    public ResourceFilter ResourceFilter { get; set; }
    public string Tab { get; set; }
}
