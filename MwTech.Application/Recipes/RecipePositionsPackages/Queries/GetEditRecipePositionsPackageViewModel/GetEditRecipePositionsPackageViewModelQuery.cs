using MediatR;
using MwTech.Application.Resources.Queries.GetResources;

namespace MwTech.Application.Recipes.RecipePositionsPackages.Queries.GetEditRecipePositionsPackageViewModel;

public class GetEditRecipePositionsPackageViewModelQuery : IRequest<EditRecipePositionsPackageViewModel>
{
    public int RecipePositionsPackageId { get; set; }
    public int RecipeStageId { get; set; }
    public ResourceFilter ResourceFilter { get; set; }
    public string Tab { get; set; }
}
