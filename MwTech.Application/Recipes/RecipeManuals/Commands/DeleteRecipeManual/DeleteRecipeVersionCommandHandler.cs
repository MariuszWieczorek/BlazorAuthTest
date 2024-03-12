using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Recipes.RecipeManuals.Commands.DeleteRecipeManual;

public class DeleteRecipeManualCommandHandler : IRequestHandler<DeleteRecipeManualCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteRecipeManualCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(DeleteRecipeManualCommand request, CancellationToken cancellationToken)
    {



        var RecipeManualToDelete = await _context.RecipeManuals
            .Include(x => x.RecipeVersion)
            .SingleOrDefaultAsync(x => x.Id == request.RecipeManualId
            && x.RecipeVersionId == request.RecipeVersionId
            && x.RecipeVersion.RecipeId == request.RecipeId);


        _context.RecipeManuals.Remove(RecipeManualToDelete);

        await _context.SaveChangesAsync();

        return;

    }
}
