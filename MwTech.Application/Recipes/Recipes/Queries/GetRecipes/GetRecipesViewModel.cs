using MwTech.Application.Common.Models;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.Recipes.Queries.GetRecipes;

public class GetRecipesViewModel
{
    public IEnumerable<Recipe> Recipes { get; set; }
    public RecipeFilter RecipeFilter { get; set; }
    public List<RecipeCategory> RecipeCategories { get; set; }
    public PagingInfo PagingInfo { get; set; }

}
