using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.RecipePositionsPackages.Commands.AddRecipePositionsPackage;

public class AddRecipePositionsPackageCommandHandler : IRequestHandler<AddRecipePositionsPackageCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public AddRecipePositionsPackageCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task Handle(AddRecipePositionsPackageCommand request, CancellationToken cancellationToken)
    {
        var RecipePositionsPackage = new RecipePositionsPackage
        {
            RecipeStageId = request.RecipeStageId,
            PackageNumber = request.PackageNumber,
            ProductName = request.ProductName,
            Description = request.Description,
            WorkCenterId = request.WorkCenterId,
            LabourClassId = request.LabourClassId,
            CrewSize = request.CrewSize,
            TimeInSeconds = request.TimeInSeconds,
            BagIsIncluded = request.BagIsIncluded
        };

        await _context.RecipePositionsPackages.AddAsync(RecipePositionsPackage);

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
