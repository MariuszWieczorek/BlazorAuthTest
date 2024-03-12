using MediatR;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.SetRecipeVersionAsInactive;


public class SetRecipeVersionAsInactiveCommandHandler : IRequestHandler<SetRecipeVersionAsInactiveCommand>
{
    private readonly IApplicationDbContext _context;

    public SetRecipeVersionAsInactiveCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(SetRecipeVersionAsInactiveCommand request, CancellationToken cancellationToken)
    {
        var routeVersion = _context.RecipeVersions
            .SingleOrDefault(x => x.Id == request.RecipeVersionId && x.RecipeId == request.RecipeId);

        routeVersion.IsActive = false;

        await _context.SaveChangesAsync();

        return;
    }
}
