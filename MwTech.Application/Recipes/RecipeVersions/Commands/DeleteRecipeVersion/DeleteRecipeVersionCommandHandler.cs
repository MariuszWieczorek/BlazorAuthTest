using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.DeleteRecipeVersion;

public class DeleteRecipeVersionCommandHandler : IRequestHandler<DeleteRecipeVersionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteRecipeVersionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(DeleteRecipeVersionCommand request, CancellationToken cancellationToken)
    {



        await DeletePositions(request);

        await DeletePositionsPackages(request);

        await DeleteManuals(request);

        await DeleteStages(request);

        await DeleteVersion(request);

        await _context.SaveChangesAsync();

        return;

    }

    private async Task DeleteVersion(DeleteRecipeVersionCommand request)
    {
        var RecipeVersionToDelete = await _context.RecipeVersions
                    .SingleOrDefaultAsync(x => x.Id == request.RecipeVersionId && x.RecipeId == request.RecipeId);

        _context.RecipeVersions.Remove(RecipeVersionToDelete);
    }

    private async Task DeletePositions(DeleteRecipeVersionCommand request)
    {
        var recipePositionsToDelete = await _context.RecipePositions
                    .Include(x => x.RecipeStage)
                    .ThenInclude(x => x.RecipeVersion)
                    .Where(x =>
                       x.RecipeStage.RecipeVersionId == request.RecipeVersionId
                    && x.RecipeStage.RecipeVersion.RecipeId == request.RecipeId).ToListAsync();


        _context.RecipePositions.RemoveRange(recipePositionsToDelete);
    }

    private async Task DeletePositionsPackages(DeleteRecipeVersionCommand request)
    {
        var recipePositionsPackagesToDelete = await _context.RecipePositionsPackages
                    .Include(x => x.RecipeStage)
                    .ThenInclude(x => x.RecipeVersion)
                    .Where(x =>
                       x.RecipeStage.RecipeVersionId == request.RecipeVersionId
                    && x.RecipeStage.RecipeVersion.RecipeId == request.RecipeId).ToListAsync();

        _context.RecipePositionsPackages.RemoveRange(recipePositionsPackagesToDelete);
    }

    private async Task DeleteManuals(DeleteRecipeVersionCommand request)
    {
        var recipeManualsToDelete = await _context.RecipeManuals
                    .Include(x => x.RecipeStage)
                    .ThenInclude(x => x.RecipeVersion)
                    .Where(x =>
                       x.RecipeStage.RecipeVersionId == request.RecipeVersionId
                    && x.RecipeStage.RecipeVersion.RecipeId == request.RecipeId).ToListAsync();

        _context.RecipeManuals.RemoveRange(recipeManualsToDelete);
    }

    private async Task DeleteStages(DeleteRecipeVersionCommand request)
    {
        var RecipeStagesToDelete = await _context.RecipeStages
                    .Where(x =>
                               x.RecipeVersionId == request.RecipeVersionId
                            && x.RecipeVersion.RecipeId == request.RecipeId)
                                .ToListAsync();


        _context.RecipeStages.RemoveRange(RecipeStagesToDelete);
    }
}
