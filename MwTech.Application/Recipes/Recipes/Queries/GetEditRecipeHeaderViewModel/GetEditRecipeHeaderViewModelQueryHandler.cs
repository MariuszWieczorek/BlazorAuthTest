using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Recipes.Recipes.Commands.EditRecipe;

namespace MwTech.Application.Recipes.Recipes.Queries.GetEditRecipeHeaderViewModel;

public class GetEditRecipeHeaderViewModelQueryHandler : IRequestHandler<GetEditRecipeHeaderViewModelQuery, EditRecipeHeaderViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetEditRecipeHeaderViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EditRecipeHeaderViewModel> Handle(GetEditRecipeHeaderViewModelQuery request, CancellationToken cancellationToken)
    {

        var Recipe = _context.Recipes.SingleOrDefault(x => x.Id == request.RecipeId);

        var RecipeCategories = await _context.RecipeCategories
            .OrderBy(x => x.OrdinalNumber)
            .AsNoTracking()
            .ToListAsync();

        var units = await _context.Units
            .OrderBy(x => x.UnitCode)
            .AsNoTracking()
            .ToListAsync();


        var editRecipeCommand = new EditRecipeCommand
        {
            Id = Recipe.Id,
            RecipeNumber = Recipe.RecipeNumber,
            Name = Recipe.Name,
            Description = Recipe.Description,
            RecipeCategoryId = Recipe.RecipeCategoryId,
            IsActive = Recipe.IsActive,
//          IsTest = Recipe.IsTest,
        };


        var vm = new EditRecipeHeaderViewModel
        {
            Recipe = Recipe,
            EditRecipeCommand = editRecipeCommand,
            RecipeCategories = RecipeCategories,
        };
        return vm;
    }

    
}
