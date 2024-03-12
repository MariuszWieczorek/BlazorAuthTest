using MediatR;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.SetRecipeVersionAsDefault;


public class SetRecipeVersionAsDefaultCommandHandler : IRequestHandler<SetRecipeVersionAsDefaultCommand>
{
    private readonly IApplicationDbContext _context;

    public SetRecipeVersionAsDefaultCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(SetRecipeVersionAsDefaultCommand request, CancellationToken cancellationToken)
    {
        var RecipeVersions = _context.RecipeVersions
            .Where(x => x.RecipeId == request.RecipeId);

        foreach (var item in RecipeVersions)
        {
            item.DefaultVersion = item.Id == request.RecipeVersionId;
        }

        await _context.SaveChangesAsync();

        return;
    }
}
