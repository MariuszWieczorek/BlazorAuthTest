using MediatR;
using MwTech.Application.Resources.Queries.GetResources;

namespace MwTech.Application.Recipes.RecipePositionsPackages.Queries.GetAddRecipePositionsPackageViewModel;

public class GetAddRecipePositionsPackageViewModelQuery : IRequest<AddRecipePositionsPackageViewModel>
{
    public int RecipeStageId { get; set; }
    public ResourceFilter ResourceFilter { get; set; }
    public string Tab { get; set; }
}
