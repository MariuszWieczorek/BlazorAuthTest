using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Recipes.RecipeManuals.Commands.EditRecipeManual;

public class EditRecipeManualCommandHandler : IRequestHandler<EditRecipeManualCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public EditRecipeManualCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task Handle(EditRecipeManualCommand request, CancellationToken cancellationToken)
    {
        var RecipeManual = await _context.RecipeManuals
            .Include(x => x.RecipeVersion)
            .FirstOrDefaultAsync(x => x.Id == request.Id
            && x.RecipeVersionId == request.RecipeVersionId
            && x.RecipeVersion.RecipeId == request.RecipeId);

        RecipeManual.RecipeStageId = request.RecipeStageId;
        RecipeManual.PositionNo = request.PositionNo;
        RecipeManual.Description = request.Description;
        RecipeManual.Duration = request.Duration;
        RecipeManual.TextValue = request.TextValue;


        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
