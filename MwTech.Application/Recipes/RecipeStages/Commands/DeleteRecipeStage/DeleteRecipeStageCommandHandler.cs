using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Recipes.RecipeStages.Commands.DeleteRecipeStage;

public class DeleteRecipeStageCommandHandler : IRequestHandler<DeleteRecipeStageCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteRecipeStageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(DeleteRecipeStageCommand request, CancellationToken cancellationToken)
    {


        var RecipePositionsToDelete = await _context.RecipePositions
            .Include(x => x.RecipeStage)
            .ThenInclude(x => x.RecipeVersion)
            .Where(x => x.RecipeStageId == request.RecipeStageId
            && x.RecipeStage.RecipeVersionId == request.RecipeVersionId
            && x.RecipeStage.RecipeVersion.RecipeId == request.RecipeId).ToListAsync();


        _context.RecipePositions.RemoveRange(RecipePositionsToDelete);

        var RecipeStageToDelete = await _context.RecipeStages
            .Include(x => x.RecipeVersion)
            .SingleOrDefaultAsync(x => x.Id == request.RecipeStageId
            && x.RecipeVersionId == request.RecipeVersionId
            && x.RecipeVersion.RecipeId == request.RecipeId);


        _context.RecipeStages.Remove(RecipeStageToDelete);

        await _context.SaveChangesAsync();

        return;

    }
}
