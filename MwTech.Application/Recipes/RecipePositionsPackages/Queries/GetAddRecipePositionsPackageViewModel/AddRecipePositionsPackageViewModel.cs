using MwTech.Application.Recipes.RecipePositionsPackages.Commands.AddRecipePositionsPackage;
using MwTech.Application.Resources.Queries.GetResources;

namespace MwTech.Application.Recipes.RecipePositionsPackages.Queries.GetAddRecipePositionsPackageViewModel;

public class AddRecipePositionsPackageViewModel
{
    public AddRecipePositionsPackageCommand AddRecipePositionsPackageCommand { get; set; }
    public GetResourcesViewModel ResourcesViewModel { get; set; }

}
