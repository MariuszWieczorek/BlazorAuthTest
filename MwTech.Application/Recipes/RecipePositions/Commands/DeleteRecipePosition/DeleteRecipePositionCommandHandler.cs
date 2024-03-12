using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Recipes.RecipePositions.Commands.DeleteRecipePosition;

public class DeleteRecipePositionCommandHandler : IRequestHandler<DeleteRecipePositionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteRecipePositionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(DeleteRecipePositionCommand request, CancellationToken cancellationToken)
    {

        var recipePositionToDelete = await _context.RecipePositions
            .Include(x => x.RecipeStage)
            .ThenInclude(x => x.RecipeVersion)
            .SingleOrDefaultAsync(x =>
               x.Id == request.RecipePositionId
            && x.RecipeStage.Id == request.RecipeStageId
            && x.RecipeStage.RecipeVersion.Id == request.RecipeVersionId
            && x.RecipeStage.RecipeVersion.RecipeId == request.RecipeId
            );

        if (recipePositionToDelete != null)
        {
            _context.RecipePositions.Remove(recipePositionToDelete);
            await _context.SaveChangesAsync();
        }

        return;

    }
}
