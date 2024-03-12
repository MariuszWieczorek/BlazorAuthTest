using MediatR;

namespace MwTech.Application.Recipes.RecipeVersions.Queries.GetRecipeVersionTechCardViewModel;

public class GetRecipeVersionTechCardViewModelQuery : IRequest<RecipeVersionTechCardViewModel>
{
    public int RecipeVersionId { get; set; }
}
