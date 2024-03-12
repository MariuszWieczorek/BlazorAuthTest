using MediatR;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.SetRecipeVersionAsActive;


public class SetRecipeVersionAsActiveCommandHandler : IRequestHandler<SetRecipeVersionAsActiveCommand>
{
    private readonly IApplicationDbContext _context;

    public SetRecipeVersionAsActiveCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(SetRecipeVersionAsActiveCommand request, CancellationToken cancellationToken)
    {
        var routeVersion = _context.RecipeVersions
            .SingleOrDefault(x => x.Id == request.RecipeVersionId && x.RecipeId == request.RecipeId);

        routeVersion.IsActive = true;

        await _context.SaveChangesAsync();

        return;
    }
}
