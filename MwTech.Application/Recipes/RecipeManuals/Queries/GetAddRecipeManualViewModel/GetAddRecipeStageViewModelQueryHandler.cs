using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Recipes.RecipeManuals.Commands.AddRecipeManual;

namespace MwTech.Application.Recipes.RecipeManuals.Queries.GetAddRecipeManualViewModel;

public class GetAddRecipeManualViewModelQueryHandler : IRequestHandler<GetAddRecipeManualViewModelQuery, AddRecipeManualViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public GetAddRecipeManualViewModelQueryHandler(IApplicationDbContext context,
          ICurrentUserService currentUserService,
          IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task<AddRecipeManualViewModel> Handle(GetAddRecipeManualViewModelQuery request, CancellationToken cancellationToken)
    {

        int maxRecipeManualNumber = 1;

        var numberOfManuals = _context.RecipeManuals
            .Where(x => x.RecipeVersionId == request.RecipeVersionId).Count();


        var RecipeVersion = _context.RecipeVersions
            .SingleOrDefault(x => x.Id == request.RecipeVersionId);

        var AddRecipeManualCommand = new AddRecipeManualCommand
        {
            RecipeId = RecipeVersion.RecipeId,
            RecipeVersionId = request.RecipeVersionId,
            Description = "",
        };

        var currentRecipeStages = await _context.RecipeStages
            .Where(x => x.RecipeVersionId == request.RecipeVersionId)
            .OrderBy(x => x.StageNo)
            .ToListAsync();

        var vm = new AddRecipeManualViewModel
        {
            AddRecipeManualCommand = AddRecipeManualCommand,
            RecipeStages = currentRecipeStages
        };
        return vm;
    }
}
