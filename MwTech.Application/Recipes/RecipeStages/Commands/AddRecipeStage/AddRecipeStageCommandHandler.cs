using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.RecipeStages.Commands.AddRecipeStage;

public class AddRecipeStageCommandHandler : IRequestHandler<AddRecipeStageCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public AddRecipeStageCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task Handle(AddRecipeStageCommand request, CancellationToken cancellationToken)
    {
        var RecipeStage = new RecipeStage
        {
            RecipeVersionId = request.RecipeVersionId,
            StageNo = request.StageNo,
            StageName = request.StageName,
            ProductNumber = request.ProductNumber,
            ProductName = request.ProductName,
            Description = request.Description,
            MixerVolume = request.MixerVolume,
            WorkCenterId = request.WorkCenterId,
            PrevStageQty = request.PrevStageQty,
            LabourClassId = request.LabourClassId,
            CrewSize = request.CrewSize,
            LabourRunFactor = request.LabourRunFactor,
            StageTimeInSeconds = request.StageTimeInSeconds,
        };

        await _context.RecipeStages.AddAsync(RecipeStage);

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
