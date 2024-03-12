using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Recipes.RecipePositionsPackages.Commands.DeleteRecipePositionsPackage;

public class DeleteRecipePositionsPackageCommandHandler : IRequestHandler<DeleteRecipePositionsPackageCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteRecipePositionsPackageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(DeleteRecipePositionsPackageCommand request, CancellationToken cancellationToken)
    {

        /*
        var RecipePositionsToDelete = await _context.RecipePositions
            .Include(x => x.RecipePositionsPackage)
            .ThenInclude(x=>x.RecipeVersion)
            .Where(x => x.RecipePositionsPackageId == request.RecipePositionsPackageId
            && x.RecipePositionsPackage.RecipeVersionId == request.RecipeVersionId
            && x.RecipePositionsPackage.RecipeVersion.RecipeId == request.RecipeId).ToListAsync();


        _context.RecipePositions.RemoveRange(RecipePositionsToDelete);
        */

        var RecipePositionsPackageToDelete = await _context.RecipePositionsPackages
            .Include(x => x.RecipeStage)
            .Include(x => x.RecipeStage.RecipeVersion)
            .Include(x => x.RecipeStage.RecipeVersion.Recipe)
            .SingleOrDefaultAsync(x =>
               x.Id == request.RecipePositionsPackageId
            && x.RecipeStageId == request.RecipeStageId
            && x.RecipeStage.RecipeVersionId == request.RecipeVersionId
            && x.RecipeStage.RecipeVersion.RecipeId == request.RecipeId
            );

        _context.RecipePositionsPackages.Remove(RecipePositionsPackageToDelete);

        await _context.SaveChangesAsync();

        return;

    }
}
