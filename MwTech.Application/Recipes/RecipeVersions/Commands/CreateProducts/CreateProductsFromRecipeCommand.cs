using MediatR;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.CreateProducts;

public class CreateProductsFromRecipeCommand : IRequest
{
    public int RecipeId { get; set; }
    public int RecipeVersionId { get; set; }
}
