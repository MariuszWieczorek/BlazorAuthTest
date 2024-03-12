using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Recipes.RecipeVersions.Commands.EditRecipeVersion;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.RecipeVersions.Queries.GetEditRecipeVersionViewModel;

public class GetEditRecipeVersionViewModelQueryHandler : IRequestHandler<GetEditRecipeVersionViewModelQuery, EditRecipeVersionViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IRecipeService _recipeService;

    public GetEditRecipeVersionViewModelQueryHandler(
        IApplicationDbContext context,
        IRecipeService recipeService
        )
    {
        _context = context;
        _recipeService = recipeService;
    }

    public async Task<EditRecipeVersionViewModel> Handle(GetEditRecipeVersionViewModelQuery request, CancellationToken cancellationToken)
    {


        var RecipeVersion = _context.RecipeVersions
            .SingleOrDefault(x => x.Id == request.RecipeVersionId);

        var RecipeVersions = await _context.RecipeVersions
            .Include(x => x.Accepted01ByUser)
            .Include(x => x.Accepted02ByUser)
            .Include(x => x.CreatedByUser)
            .Where(x => x.RecipeId == request.RecipeId)
            .OrderByDescending(x => x.VersionNumber)
            .AsNoTracking()
            .ToListAsync();

        //  var boms = await GetBoms(request);


        var editRecipeVersionCommand = new EditRecipeVersionCommand
        {
            Id = request.RecipeVersionId,
            RecipeId = request.RecipeId,
            Recipe = await _context.Recipes.SingleOrDefaultAsync(x => x.Id == request.RecipeId),
            VersionNumber = RecipeVersion.VersionNumber,
            AlternativeNo = RecipeVersion.AlternativeNo,
            Name = RecipeVersion.Name,
            Description = RecipeVersion.Description,
            RecipeQty = RecipeVersion.RecipeQty,
            IsActive = RecipeVersion.IsActive,
            ModifiedByUserId = RecipeVersion.ModifiedByUserId,
            ModifiedDate = RecipeVersion.ModifiedDate,
            IsAccepted01 = RecipeVersion.IsAccepted01,
            IsAccepted02 = RecipeVersion.IsAccepted02,
        };


        var recipeStages = await _recipeService.GetRecipeVersionStages(request.RecipeVersionId);


        var recipeManuals = await _context.RecipeManuals
                .Include(x => x.RecipeVersion)
                .Include(x => x.RecipeStage)
                .Where(x => x.RecipeVersionId == request.RecipeVersionId)
                .OrderBy(x => x.RecipeStage.StageNo)
                .ThenBy(x => x.PositionNo)
                .ToListAsync();



        var vm = new EditRecipeVersionViewModel
        {
            EditRecipeVersionCommand = editRecipeVersionCommand,
            RecipeStages = recipeStages,
            RecipeManuals = recipeManuals
        };

        return vm;
    }

}




