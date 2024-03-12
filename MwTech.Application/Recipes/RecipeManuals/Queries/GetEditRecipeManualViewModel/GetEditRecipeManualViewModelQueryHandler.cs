using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Recipes.RecipeManuals.Commands.EditRecipeManual;
using MwTech.Domain.Entities;

namespace MwTech.Application.Recipes.RecipeManuals.Queries.GetEditRecipeManualViewModel;

public class GetEditRecipeManualViewModelQueryHandler : IRequestHandler<GetEditRecipeManualViewModelQuery, EditRecipeManualViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetEditRecipeManualViewModelQueryHandler(
        IApplicationDbContext context
        )
    {
        _context = context;
    }

    public async Task<EditRecipeManualViewModel> Handle(GetEditRecipeManualViewModelQuery request, CancellationToken cancellationToken)
    {

        if (request.RecipeManualId != 0 && request.RecipeManualId != null)
        {
            request.RecipeManualId = _context.RecipeManuals.FirstOrDefault(x => x.Id == request.RecipeManualId).Id;
        }


        var RecipeManual = _context.RecipeManuals
            .Include(x => x.RecipeVersion)
            .ThenInclude(x => x.Recipe)
            .SingleOrDefault(x => x.Id == request.RecipeManualId && x.RecipeVersionId == request.RecipeVersionId);




        var editRecipeManualCommand = new EditRecipeManualCommand
        {
            Id = request.RecipeManualId,
            PositionNo = RecipeManual.PositionNo,
            RecipeId = RecipeManual.RecipeVersion.RecipeId,
            RecipeVersionId = request.RecipeVersionId,
            RecipeStageId = RecipeManual.RecipeStageId,
            Description = RecipeManual.Description,
            Duration = RecipeManual.Duration,
            TextValue = RecipeManual.TextValue,
        };

        var currentRecipeStages = await _context.RecipeStages
            .Where(x => x.RecipeVersionId == request.RecipeVersionId)
            .OrderBy(x => x.StageNo)
            .ToListAsync();

        var vm = new EditRecipeManualViewModel
        {
            EditRecipeManualCommand = editRecipeManualCommand,
            RecipeStages = currentRecipeStages
        };

        return vm;
    }




}




