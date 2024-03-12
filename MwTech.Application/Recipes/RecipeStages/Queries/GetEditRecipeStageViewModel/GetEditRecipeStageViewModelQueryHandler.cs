using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Recipes.RecipeStages.Commands.EditRecipeStage;
using MwTech.Application.Resources.Queries.GetResources;
using MwTech.Domain.Entities;

namespace MwTech.Application.Recipes.RecipeStages.Queries.GetEditRecipeStageViewModel;

public class GetEditRecipeStageViewModelQueryHandler : IRequestHandler<GetEditRecipeStageViewModelQuery, EditRecipeStageViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetEditRecipeStageViewModelQueryHandler(
        IApplicationDbContext context
        )
    {
        _context = context;
    }

    public async Task<EditRecipeStageViewModel> Handle(GetEditRecipeStageViewModelQuery request, CancellationToken cancellationToken)
    {


        var RecipeStage = _context.RecipeStages
            .Include(x => x.RecipeVersion)
            .ThenInclude(x => x.Recipe)
            .Include(x => x.WorkCenter)
            .Include(x => x.LabourClass)
            .SingleOrDefault(x => x.Id == request.RecipeStageId && x.RecipeVersionId == request.RecipeVersionId);




        var editRecipeStageCommand = new EditRecipeStageCommand
        {
            Id = request.RecipeStageId,
            RecipeId = RecipeStage.RecipeVersion.RecipeId,
            RecipeVersionId = request.RecipeVersionId,
            StageNo = RecipeStage.StageNo,
            StageName = RecipeStage.StageName,
            ProductNumber = RecipeStage.ProductNumber,
            ProductName = RecipeStage.ProductName,
            Description = RecipeStage.Description,
            MixerVolume = RecipeStage.MixerVolume,
            DivideQtyBy = RecipeStage.DivideQtyBy,
            MultiplyQtyBy = RecipeStage.MultiplyQtyBy,
            WorkCenterId = RecipeStage.WorkCenterId,
            WorkCenter = RecipeStage.WorkCenter,
            PrevStageQty = RecipeStage.PrevStageQty,
            LabourClassId = RecipeStage.LabourClassId,
            LabourClass = RecipeStage.LabourClass,
            CrewSize = RecipeStage.CrewSize,
            LabourRunFactor = RecipeStage.LabourRunFactor,
            StageTimeInSeconds = RecipeStage.StageTimeInSeconds,
        };


        //
        var resources = _context.Resources
            .Include(x => x.ProductCategory)
            .Include(x => x.Unit)
            .Include(x => x.LabourClass)
            .AsNoTracking()
            .AsQueryable();

        resources = ResourcesFilter(resources, request.ResourceFilter);

        var getResourcesViewModel = new GetResourcesViewModel
        {
            ProductCategories = await _context.ProductCategories.OrderBy(x => x.OrdinalNumber).AsNoTracking().ToListAsync(),
            ResourceFilter = request.ResourceFilter,
            Resources = await resources.Take(500).ToListAsync(),
        };

        //

        var vm = new EditRecipeStageViewModel
        {
            EditRecipeStageCommand = editRecipeStageCommand,
            ResourcesViewModel = getResourcesViewModel
        };

        return vm;
    }



    private IQueryable<Resource> ResourcesFilter(IQueryable<Resource> resources, ResourceFilter resourceFilter)
    {
        if (resourceFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(resourceFilter.Name))
                resources = resources.Where(x => x.Name.Contains(resourceFilter.Name));

            if (!string.IsNullOrWhiteSpace(resourceFilter.ResourceNumber))
                resources = resources.Where(x => x.ResourceNumber.Contains(resourceFilter.ResourceNumber));

            if (resourceFilter.ProductCategoryId != 0)
                resources = resources.Where(x => x.ProductCategoryId == resourceFilter.ProductCategoryId);

        }

        return resources;
    }

}




