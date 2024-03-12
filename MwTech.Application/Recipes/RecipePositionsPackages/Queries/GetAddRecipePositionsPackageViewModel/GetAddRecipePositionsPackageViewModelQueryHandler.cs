using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Recipes.RecipePositionsPackages.Commands.AddRecipePositionsPackage;
using MwTech.Application.Resources.Queries.GetResources;
using MwTech.Domain.Entities;

namespace MwTech.Application.Recipes.RecipePositionsPackages.Queries.GetAddRecipePositionsPackageViewModel;

public class GetAddRecipePositionsPackageViewModelQueryHandler : IRequestHandler<GetAddRecipePositionsPackageViewModelQuery, AddRecipePositionsPackageViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public GetAddRecipePositionsPackageViewModelQueryHandler(IApplicationDbContext context,
          ICurrentUserService currentUserService,
          IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task<AddRecipePositionsPackageViewModel> Handle(GetAddRecipePositionsPackageViewModelQuery request, CancellationToken cancellationToken)
    {

        int maxNumber = 1;

        var numberOfStages = _context.RecipePositionsPackages
            .Where(x => x.RecipeStageId == request.RecipeStageId).Count();

        if (numberOfStages != 0)
        {
            maxNumber = _context.RecipePositionsPackages
                .Where(x => x.RecipeStageId == request.RecipeStageId)
                .Max(x => x.PackageNumber) + 1;
        }

        var recipeStage = _context.RecipeStages
            .Include(x => x.RecipeVersion)
            .Include(x => x.RecipeVersion.Recipe)
            .SingleOrDefault(x => x.Id == request.RecipeStageId);

        var AddRecipePositionsPackageCommand = new AddRecipePositionsPackageCommand
        {
            RecipeId = recipeStage.RecipeVersion.RecipeId,
            RecipeVersionId = recipeStage.RecipeVersionId,
            RecipeStageId = request.RecipeStageId,
            PackageNumber = maxNumber,
            Description = "",
            WorkCenterId = 0,
            WorkCenter = null,
            BagIsIncluded = true
        };


        //
        var resources = _context.Resources
            .Include(x => x.ProductCategory)
            .Include(x => x.Unit)
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

        var vm = new AddRecipePositionsPackageViewModel
        {
            AddRecipePositionsPackageCommand = AddRecipePositionsPackageCommand,
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
