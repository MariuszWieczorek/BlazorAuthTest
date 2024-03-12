using MwTech.Application.Recipes.RecipePositionsPackages.Commands.EditRecipePositionsPackage;
using MwTech.Application.Resources.Queries.GetResources;
using MwTech.Domain.Entities;

namespace MwTech.Application.Recipes.RecipePositionsPackages.Queries.GetEditRecipePositionsPackageViewModel;

public class EditRecipePositionsPackageViewModel
{
    public EditRecipePositionsPackageCommand EditRecipePositionsPackageCommand { get; set; }
    public GetResourcesViewModel ResourcesViewModel { get; set; }

}
