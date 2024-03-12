using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.EditRecipeVersion;

public class EditRecipeVersionCommandHandler : IRequestHandler<EditRecipeVersionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public EditRecipeVersionCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task Handle(EditRecipeVersionCommand request, CancellationToken cancellationToken)
    {
        var RecipeVersion = await _context.RecipeVersions
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.RecipeId == request.RecipeId);

        RecipeVersion.VersionNumber = request.VersionNumber;
        RecipeVersion.AlternativeNo = request.AlternativeNo;
        RecipeVersion.Name = request.Name;
        RecipeVersion.Description = request.Description;
        RecipeVersion.RecipeQty = request.RecipeQty;
        RecipeVersion.IsActive = request.IsActive;

        RecipeVersion.ModifiedByUserId = _currentUserService.UserId;
        RecipeVersion.ModifiedDate = _dateTimeService.Now;

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
