using MediatR;
using MwTech.Application.Resources.Queries.GetResources;

namespace MwTech.Application.Recipes.RecipeStages.Queries.GetEditRecipeStageViewModel;

public class GetEditRecipeStageViewModelQuery : IRequest<EditRecipeStageViewModel>
{
    public int RecipeStageId { get; set; }
    public int RecipeVersionId { get; set; }
    public ResourceFilter ResourceFilter { get; set; }
    public string Tab { get; set; }
}
