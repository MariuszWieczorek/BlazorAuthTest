using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Recipes.RecipeStages.Commands.EditRecipeStage;

public class EditRecipeStageCommandHandler : IRequestHandler<EditRecipeStageCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public EditRecipeStageCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task Handle(EditRecipeStageCommand request, CancellationToken cancellationToken)
    {
        var RecipeStage = await _context.RecipeStages
            .Include(x => x.RecipeVersion)
            .FirstOrDefaultAsync(x => x.Id == request.Id
            && x.RecipeVersionId == request.RecipeVersionId
            && x.RecipeVersion.RecipeId == request.RecipeId);

        RecipeStage.StageNo = request.StageNo;
        RecipeStage.StageName = request.StageName;
        RecipeStage.ProductNumber = request.ProductNumber;
        RecipeStage.ProductName = request.ProductName;
        RecipeStage.Description = request.Description;
        RecipeStage.MixerVolume = request.MixerVolume;
        RecipeStage.DivideQtyBy = request.DivideQtyBy;
        RecipeStage.MultiplyQtyBy = request.MultiplyQtyBy;
        RecipeStage.WorkCenterId = request.WorkCenterId;
        RecipeStage.PrevStageQty = request.PrevStageQty;
        RecipeStage.LabourClassId = request.LabourClassId;
        RecipeStage.CrewSize = request.CrewSize;
        RecipeStage.LabourRunFactor = request.LabourRunFactor;
        RecipeStage.StageTimeInSeconds = request.StageTimeInSeconds;

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
