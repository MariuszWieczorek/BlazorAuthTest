using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Recipes.Recipes.Commands.AddRecipe;

namespace MwTech.Application.Recipes.Recipes.Queries.GetAddRecipeViewModel;

public class GetAddRecipeViewModelQueryHandler : IRequestHandler<GetAddRecipeViewModelQuery, AddRecipeViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetAddRecipeViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<AddRecipeViewModel> Handle(GetAddRecipeViewModelQuery request, CancellationToken cancellationToken)
    {
        var vm = new AddRecipeViewModel()
        {
            RecipeCategories = await _context.RecipeCategories.AsNoTracking().ToListAsync(),
            Units = await _context.Units.AsNoTracking().ToListAsync(),
            AddRecipeCommand = new AddRecipeCommand()
        };

        return vm;
    }
}
