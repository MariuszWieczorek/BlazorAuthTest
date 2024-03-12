using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Recipes.Recipes.Commands.EditRecipe;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.Recipes.Queries.GetEditRecipeViewModel;

public class GetEditRecipeViewModelQueryHandler : IRequestHandler<GetEditRecipeViewModelQuery, EditRecipeViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IRecipeService _recipeService;

    public GetEditRecipeViewModelQueryHandler(IApplicationDbContext context, IRecipeService recipeService)
    {
        _context = context;
        _recipeService = recipeService;
    }
    public async Task<EditRecipeViewModel> Handle(GetEditRecipeViewModelQuery request, CancellationToken cancellationToken)
    {

        var Recipe = await _context.Recipes.SingleAsync(x => x.Id == request.RecipeId);
        
        var editRecipeCommand = new EditRecipeCommand
        {
            Id = Recipe.Id,
            Name = Recipe.Name,
            RecipeNumber = Recipe.RecipeNumber,
            Description = Recipe.Description,
            RecipeCategoryId = Recipe.RecipeCategoryId,

        };


        var recipeVersions = await _context.RecipeVersions
            .Include(x => x.Accepted01ByUser)
            .Include(x => x.Accepted02ByUser)
            .Include(x => x.CreatedByUser)
            .Where(x => x.RecipeId == request.RecipeId)
            .OrderByDescending(x => x.VersionNumber)
            .AsNoTracking()
            .ToListAsync();


        recipeVersions = await GetRecipeSummary(recipeVersions);


        var vm = new EditRecipeViewModel()
        {
            RecipeCategories = await _context.RecipeCategories.AsNoTracking().ToListAsync(),
            Units = await _context.Units.AsNoTracking().ToListAsync(),
            EditRecipeCommand = editRecipeCommand,
            RecipeVersions = recipeVersions,
        };

        return vm;
    }


    private async Task<List<RecipeVersion>> GetRecipeSummary(List<RecipeVersion> recipeVersions)
    {
        foreach (var recipeVersion in recipeVersions)
        {
            var cost = await _recipeService.GetRecipeVersionSummary(recipeVersion.RecipeId,recipeVersion.Id);
            recipeVersion.TotalCost = cost.TotalCost;
            recipeVersion.MaterialCost = cost.MaterialCost;
            recipeVersion.LabourCost = cost.LabourCost;
            recipeVersion.MarkupCost = cost.MarkupCost;
            recipeVersion.TotalQty = cost.TotalQty;
            recipeVersion.TotalVolume = cost.TotalVolume;

        }



        return recipeVersions;
    }
}
