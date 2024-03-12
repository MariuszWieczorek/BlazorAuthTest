using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Recipes.Recipes.Commands.DeleteRecipe;

public class DeleteRecipeCommandHandler : IRequestHandler<DeleteRecipeCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteRecipeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        
        var RecipeToDelete = await _context.Recipes.SingleOrDefaultAsync(x => x.Id == request.Id);
        _context.Recipes.Remove(RecipeToDelete);
        await _context.SaveChangesAsync();

        //todo usuwanie weresji receptur 

        return;

    }
}
