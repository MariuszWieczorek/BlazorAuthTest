using MediatR;
using MwTech.Application.Recipes.RecipeCategories.Commands.EditRecipeCategory;

namespace MwTech.Application.Recipes.RecipeCategories.Queries.GetEditRecipeCategory;

public class GetEditRecipeCategoryQuery : IRequest<EditRecipeCategoryCommand>
{
    public int Id { get; set; }
}
