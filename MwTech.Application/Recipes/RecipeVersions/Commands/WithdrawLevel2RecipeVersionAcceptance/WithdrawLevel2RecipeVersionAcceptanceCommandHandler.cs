using MediatR;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.WithdrawLevel2RecipeVersionAcceptance;


public class WithdrawLevel2RecipeVersionAcceptanceCommandHandler : IRequestHandler<WithdrawLevel2RecipeVersionAcceptanceCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public WithdrawLevel2RecipeVersionAcceptanceCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }
    public async Task Handle(WithdrawLevel2RecipeVersionAcceptanceCommand request, CancellationToken cancellationToken)
    {
        var RecipeVersion = _context.RecipeVersions
            .SingleOrDefault(x => x.RecipeId == request.RecipeId && x.Id == request.RecipeVersionId);

        RecipeVersion.IsAccepted02 = false;
        RecipeVersion.Accepted02ByUserId = null;
        RecipeVersion.Accepted02Date = null;


        await _context.SaveChangesAsync();

        return;
    }
}
