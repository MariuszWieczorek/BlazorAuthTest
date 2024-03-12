using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.RecipeManuals.Commands.AddRecipeManual;

public class AddRecipeManualCommandHandler : IRequestHandler<AddRecipeManualCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public AddRecipeManualCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task Handle(AddRecipeManualCommand request, CancellationToken cancellationToken)
    {
        var RecipeManual = new RecipeManual
        {
            RecipeVersionId = request.RecipeVersionId,
            RecipeStageId = request.RecipeStageId,
            PositionNo = request.PositionNo,
            Description = request.Description,
            Duration = request.Duration,
            TextValue = request.TextValue,
        };

        await _context.RecipeManuals.AddAsync(RecipeManual);

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
