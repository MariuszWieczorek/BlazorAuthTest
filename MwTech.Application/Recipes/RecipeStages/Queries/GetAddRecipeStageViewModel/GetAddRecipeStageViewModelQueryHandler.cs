using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Recipes.RecipeStages.Commands.AddRecipeStage;
using MwTech.Application.Resources.Queries.GetResources;
using MwTech.Domain.Entities;

namespace MwTech.Application.Recipes.RecipeStages.Queries.GetAddRecipeStageViewModel;

public class GetAddRecipeStageViewModelQueryHandler : IRequestHandler<GetAddRecipeStageViewModelQuery, AddRecipeStageViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public GetAddRecipeStageViewModelQueryHandler(IApplicationDbContext context,
          ICurrentUserService currentUserService,
          IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task<AddRecipeStageViewModel> Handle(GetAddRecipeStageViewModelQuery request, CancellationToken cancellationToken)
    {

        int maxRecipeStageNumber = 1;

        var numberOfStages = _context.RecipeStages
            .Where(x => x.RecipeVersionId == request.RecipeVersionId).Count();

        if (numberOfStages != 0)
        {
            maxRecipeStageNumber = _context.RecipeStages
                .Where(x => x.RecipeVersionId == request.RecipeVersionId)
                .Max(x => x.StageNo) + 1;
        }

        var RecipeVersion = _context.RecipeVersions
            .SingleOrDefault(x => x.Id == request.RecipeVersionId);

        var AddRecipeStageCommand = new AddRecipeStageCommand
        {
            RecipeId = RecipeVersion.RecipeId,
            RecipeVersionId = request.RecipeVersionId,
            StageNo = maxRecipeStageNumber,
            Description = "",
            WorkCenterId = 0,
            WorkCenter = null,
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

        var vm = new AddRecipeStageViewModel
        {
            AddRecipeStageCommand = AddRecipeStageCommand,
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
