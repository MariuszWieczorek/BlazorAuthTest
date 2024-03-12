using MwTech.Application.Products.Products.Queries.GetProductsForPopup;
using MwTech.Application.Recipes.RecipePositions.Commands.AddRecipePosition;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.RecipePositions.Queries.GetAddRecipePositionViewModel;

public class AddRecipePositionViewModel
{
    public AddRecipePositionCommand AddRecipePositionCommand { get; set; }
    public ProductsForPopupViewModel GetProductsForPopupViewModel { get; set; }
    public List<RecipePositionsPackage> RecipePositionsPackages { get; set; }
}
