using MwTech.Application.Products.Common;
using MwTech.Application.Products.Products.Queries.GetProductsForPopup;
using MwTech.Application.Recipes.RecipePositions.Commands.EditRecipePosition;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.RecipePositions.Queries.GetEditRecipePositionViewModel;

public class EditRecipePositionViewModel
{
    public EditRecipePositionCommand EditRecipePositionCommand { get; set; }
    public ProductsForPopupViewModel GetProductsForPopupViewModel { get; set; }
    public List<RecipePositionsPackage> RecipePositionsPackages { get; set; }
}
