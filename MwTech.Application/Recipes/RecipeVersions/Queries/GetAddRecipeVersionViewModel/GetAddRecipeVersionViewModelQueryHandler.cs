using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Recipes.RecipeVersions.Commands.AddRecipeVersion;

namespace MwTech.Application.Recipes.RecipeVersions.Queries.GetAddRecipeVersionViewModel;

public class GetAddRecipeVersionViewModelQueryHandler : IRequestHandler<GetAddRecipeVersionViewModelQuery, AddRecipeVersionViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public GetAddRecipeVersionViewModelQueryHandler(IApplicationDbContext context,
          ICurrentUserService currentUserService,
          IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task<AddRecipeVersionViewModel> Handle(GetAddRecipeVersionViewModelQuery request, CancellationToken cancellationToken)
    {

        int maxRecipeVersionNumber = 1;
        int maxRecipeAlternativeNo = 1;

        var numberOfRouteVersion = _context.RecipeVersions
            .Where(x => x.RecipeId == request.RecipeId).Count();

        if (numberOfRouteVersion != 0)
        {
            maxRecipeVersionNumber = _context.RecipeVersions
                .Where(x => x.RecipeId == request.RecipeId)
                .Max(x => x.VersionNumber) + 1;
        }


        var AddRecipeVersionCommand = new AddRecipeVersionCommand
        {
            RecipeId = request.RecipeId,
            VersionNumber = maxRecipeVersionNumber,
            AlternativeNo = maxRecipeAlternativeNo,
            Name = $"wersja {maxRecipeVersionNumber}",
            Description = "",
            RecipeQty = 1,
            RecipeWeight = 0,
            IsActive = true,
            CreatedByUserId = _currentUserService.UserId,
            CreatedDate = _dateTimeService.Now
        };


        var vm = new AddRecipeVersionViewModel
        {
            AddRecipeVersionCommand = AddRecipeVersionCommand,
        };
        return vm;
    }
}
