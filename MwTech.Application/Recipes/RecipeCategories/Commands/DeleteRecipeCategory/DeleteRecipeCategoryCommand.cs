using MediatR;

namespace MwTech.Application.Recipes.RecipeCategories.Commands.DeleteRecipeCategory;

public class DeleteRecipeCategoryCommand : IRequest
{
    public int Id { get; set; }
}
