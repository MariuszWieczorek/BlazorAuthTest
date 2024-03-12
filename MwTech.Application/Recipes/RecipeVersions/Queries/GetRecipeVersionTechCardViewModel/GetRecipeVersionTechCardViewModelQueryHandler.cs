using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Recipes.RecipeStages.Commands.EditRecipeStage;
using MwTech.Domain.Entities;

namespace MwTech.Application.Recipes.RecipeVersions.Queries.GetRecipeVersionTechCardViewModel;

public class GetRecipeVersionTechCardViewModelQueryHandler : IRequestHandler<GetRecipeVersionTechCardViewModelQuery, RecipeVersionTechCardViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IRecipeService _recipeService;

    public GetRecipeVersionTechCardViewModelQueryHandler(
        IApplicationDbContext context,
        IRecipeService recipeService
        )
    {
        _context = context;
        _recipeService = recipeService;
    }

    public async Task<RecipeVersionTechCardViewModel> Handle(GetRecipeVersionTechCardViewModelQuery request, CancellationToken cancellationToken)
    {

        var recipeStages = await _recipeService.GetRecipeVersionStages(request.RecipeVersionId);



        var recipeVersion = await _context.RecipeVersions
            .Include(x => x.Accepted01ByUser)
            .Include(x => x.Accepted02ByUser)
            .Include(x => x.CreatedByUser)
            .Include(x => x.Recipe)
            .SingleOrDefaultAsync(x => x.Id == request.RecipeVersionId);



        var vm = new RecipeVersionTechCardViewModel
        {
            RecipeStages = recipeStages,
            RecipeVersion = recipeVersion
        };


        return vm;
    }




}




