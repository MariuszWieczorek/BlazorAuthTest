using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.Recipes.Queries.GetRecipes;

public class GetRecipesQueryHandler : IRequestHandler<GetRecipesQuery, GetRecipesViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IRecipeService _recipeService;

    public GetRecipesQueryHandler(IApplicationDbContext context, IRecipeService recipeService)
    {
        _context = context;
        _recipeService = recipeService;
    }
    public async Task<GetRecipesViewModel> Handle(GetRecipesQuery request, CancellationToken cancellationToken)
    {
        var Recipes = _context.Recipes
            .Include(x=>x.RecipeCategory)
            .AsNoTracking()
            .AsQueryable();

        Recipes = Filter(Recipes, request.RecipeFilter);

        // stronicowanie
        if (request.PagingInfo != null)
        {

            request.PagingInfo.TotalItems = Recipes.Count();
            request.PagingInfo.ItemsPerPage = 100;

            if (request.PagingInfo.ItemsPerPage > 0 && request.PagingInfo.TotalItems > 0)
                Recipes = Recipes
                    .Skip((request.PagingInfo.CurrentPage - 1) * request.PagingInfo.ItemsPerPage)
                    .Take(request.PagingInfo.ItemsPerPage);
        }

        var recipesList = await Recipes.OrderBy(x=>x.RecipeNumber).ToListAsync();

        recipesList = await GetCost(recipesList);

        recipesList = await CheckUserManuals(recipesList);


        var vm = new GetRecipesViewModel
            { 
              Recipes = recipesList,
              RecipeFilter = request.RecipeFilter,
              PagingInfo = request.PagingInfo,
              RecipeCategories = await _context.RecipeCategories.ToListAsync()
            };

        return vm;
           
    }

    private async Task<List<Recipe>> CheckUserManuals(List<Recipe> recipesList)
    {
        foreach (var recipe in recipesList) 
        {
            var firstRecipeManual = await _context.RecipeManuals.FirstOrDefaultAsync(x =>x.RecipeVersionId == recipe.VersionId);
            recipe.HasUserManual = firstRecipeManual != null;
        }
        return recipesList;
    }

    private async Task<List<Recipe>> GetCost(List<Recipe> recipes)
    {
        foreach (var recipe in recipes) 
        {
            var recipeSummary = await _recipeService.GetRecipeSummary(recipe.Id);
            recipe.TotalCost = recipeSummary.TotalCost;
            recipe.MaterialCost = recipeSummary.MaterialCost;
            recipe.LabourCost = recipeSummary.LabourCost;
            recipe.MarkupCost = recipeSummary.MarkupCost;
            recipe.IsAccepted01 = recipeSummary.IsAccepted01;
            recipe.IsAccepted02 = recipeSummary.IsAccepted02;
            recipe.VersionName = recipeSummary.VersionName;
            recipe.VersionId = recipeSummary.VersionId;
            recipe.ProductNumber = recipeSummary.ProductNumber;
            recipe.MaterialUnitCostWithoutProcessReturn = recipeSummary.MaterialUnitCostWithoutProcessReturn;
        }

        
        
        return recipes;
    }

    public IQueryable<Recipe> Filter(IQueryable<Recipe> Recipes, RecipeFilter RecipeFilter)
    {
        if (RecipeFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(RecipeFilter.Name))
                Recipes = Recipes.Where(x => x.Name.Contains(RecipeFilter.Name));

            if (!string.IsNullOrWhiteSpace(RecipeFilter.RecipeNumber))
                Recipes = Recipes.Where(x => x.RecipeNumber.Contains(RecipeFilter.RecipeNumber));

            if (RecipeFilter.RecipeCategoryId != 0)
                Recipes = Recipes.Where(x => x.RecipeCategoryId == RecipeFilter.RecipeCategoryId);
        }

        return Recipes;
    }
}
