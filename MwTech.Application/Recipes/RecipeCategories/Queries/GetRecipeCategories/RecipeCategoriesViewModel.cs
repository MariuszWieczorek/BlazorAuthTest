using MwTech.Application.Common.Models;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.RecipeCategories.Queries.GetRecipeCategories;

public class RecipeCategoriesViewModel
{
    public IEnumerable<RecipeCategory> RecipeCategories { get; set; }
    public RecipeCategoryFilter RecipeCategoryFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }

}
