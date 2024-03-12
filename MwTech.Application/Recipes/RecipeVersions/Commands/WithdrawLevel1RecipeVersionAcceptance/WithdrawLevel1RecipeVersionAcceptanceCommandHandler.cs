using MediatR;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.WithdrawLevel1RecipeVersionAcceptance;


public class WithdrawLevel1RecipeVersionAcceptanceCommandHandler : IRequestHandler<WithdrawLevel1RecipeVersionAcceptanceCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public WithdrawLevel1RecipeVersionAcceptanceCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }
    public async Task Handle(WithdrawLevel1RecipeVersionAcceptanceCommand request, CancellationToken cancellationToken)
    {
        var RecipeVersion = _context.RecipeVersions
            .SingleOrDefault(x => x.RecipeId == request.RecipeId && x.Id == request.RecipeVersionId);

        RecipeVersion.IsAccepted01 = false;
        RecipeVersion.Accepted01ByUserId = null;
        RecipeVersion.Accepted01Date = null;


        await _context.SaveChangesAsync();

        return;
    }
}
