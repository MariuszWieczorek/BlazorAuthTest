using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Recipes.Recipes.Commands.EditRecipe;

public class EditRecipeCommandHandler : IRequestHandler<EditRecipeCommand>
{
    private readonly IApplicationDbContext _context;

    public EditRecipeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EditRecipeCommand request, CancellationToken cancellationToken)
    {
        var Recipe = await _context.Recipes.FirstOrDefaultAsync(x => x.Id == request.Id);
        
        Recipe.RecipeNumber = request.RecipeNumber;
        Recipe.Name = request.Name;
        Recipe.Description = request.Description;
        Recipe.RecipeCategoryId = request.RecipeCategoryId;

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
