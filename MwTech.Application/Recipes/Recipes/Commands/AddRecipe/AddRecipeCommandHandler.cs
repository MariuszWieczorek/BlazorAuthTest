using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.Recipes.Commands.AddRecipe;

public class AddRecipeCommandHandler : IRequestHandler<AddRecipeCommand>
{
    private readonly IApplicationDbContext _context;

    public AddRecipeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddRecipeCommand request, CancellationToken cancellationToken)
    {
        var Recipe = new Recipe();
        
        Recipe.RecipeNumber = request.RecipeNumber;
        Recipe.Name = request.Name;
        Recipe.Description = request.Description;
        Recipe.RecipeCategoryId = request.RecipeCategoryId;
 
        await _context.Recipes.AddAsync(Recipe);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
