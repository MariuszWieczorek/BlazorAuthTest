using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Recipes.RecipePositionsPackages.Commands.EditRecipePositionsPackage;

public class EditRecipePositionsPackageCommandHandler : IRequestHandler<EditRecipePositionsPackageCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public EditRecipePositionsPackageCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task Handle(EditRecipePositionsPackageCommand request, CancellationToken cancellationToken)
    {
        var RecipePositionsPackage = await _context.RecipePositionsPackages
               .Include(x => x.RecipeStage)
               .Include(x => x.RecipeStage.RecipeVersion)
               .Include(x => x.RecipeStage.RecipeVersion.Recipe)
               .SingleOrDefaultAsync(x =>
                  x.Id == request.Id
                    && x.RecipeStageId == request.RecipeStageId
               );


        RecipePositionsPackage.PackageNumber = request.PackageNumber;
        RecipePositionsPackage.ProductNumber = request.ProductNumber;
        RecipePositionsPackage.ProductName = request.ProductName;
        RecipePositionsPackage.Description = request.Description;
        RecipePositionsPackage.WorkCenterId = request.WorkCenterId;
        RecipePositionsPackage.LabourClassId = request.LabourClassId;
        RecipePositionsPackage.CrewSize = request.CrewSize;
        RecipePositionsPackage.TimeInSeconds = request.TimeInSeconds;
        RecipePositionsPackage.BagIsIncluded = request.BagIsIncluded;

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
