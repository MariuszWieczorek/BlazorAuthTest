using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.AddRecipeVersion;

public class AddRecipeVersionCommandHandler : IRequestHandler<AddRecipeVersionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public AddRecipeVersionCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task Handle(AddRecipeVersionCommand request, CancellationToken cancellationToken)
    {
        var RecipeVersion = new RecipeVersion
        {
            RecipeId = request.RecipeId,
            VersionNumber = request.VersionNumber,
            AlternativeNo = request.AlternativeNo,
            Name = request.Name,
            Description = request.Description,
            RecipeQty = request.RecipeQty,
            IsActive = request.IsActive,
            CreatedByUserId = _currentUserService.UserId,
            CreatedDate = _dateTimeService.Now,
            DefaultVersion = request.DefaultVersion
        };

        await _context.RecipeVersions.AddAsync(RecipeVersion);

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
