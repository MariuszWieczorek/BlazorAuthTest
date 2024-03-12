using MediatR;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.AcceptRecipeVersionLevel1;


public class AcceptRecipeVersionLevel1CommandCommandHandler : IRequestHandler<AcceptRecipeVersionLevel1Command>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public AcceptRecipeVersionLevel1CommandCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }
    public async Task Handle(AcceptRecipeVersionLevel1Command request, CancellationToken cancellationToken)
    {
        var RecipeVersion = _context.RecipeVersions
            .SingleOrDefault(x => x.RecipeId == request.RecipeId && x.Id == request.RecipeVersionId);

        RecipeVersion.IsAccepted01 = true;
        RecipeVersion.Accepted01ByUserId = _currentUserService.UserId;
        RecipeVersion.Accepted01Date = _dateTimeService.Now;


        await _context.SaveChangesAsync();

        return;
    }
}
