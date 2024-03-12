using MediatR;
using MwTech.Application.Common.Models;

namespace MwTech.Application.Recipes.RecipeCategories.Queries.GetRecipeCategories;

public class GetRecipeCategoriesQuery : IRequest<RecipeCategoriesViewModel>
{
    public RecipeCategoryFilter RecipeCategoryFilter { get; set; }

    public PagingInfo PagingInfo { get; set; }

}
