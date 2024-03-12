using MediatR;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.AcceptRecipeVersionLevel2;


public class AcceptRecipeVersionLevel2CommandCommandHandler : IRequestHandler<AcceptRecipeVersionLevel2Command>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public AcceptRecipeVersionLevel2CommandCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }
    public async Task Handle(AcceptRecipeVersionLevel2Command request, CancellationToken cancellationToken)
    {
        var RecipeVersion = _context.RecipeVersions
            .SingleOrDefault(x => x.RecipeId == request.RecipeId && x.Id == request.RecipeVersionId);

        RecipeVersion.IsAccepted02 = true;
        RecipeVersion.Accepted02ByUserId = _currentUserService.UserId;
        RecipeVersion.Accepted02Date = _dateTimeService.Now;


        await _context.SaveChangesAsync();

        return;
    }
}
