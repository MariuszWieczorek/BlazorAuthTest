using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Recipes.RecipePositionsPackages.Commands.EditRecipePositionsPackage;
using MwTech.Application.Resources.Queries.GetResources;
using MwTech.Domain.Entities;

namespace MwTech.Application.Recipes.RecipePositionsPackages.Queries.GetEditRecipePositionsPackageViewModel;

public class GetEditRecipePositionsPackageViewModelQueryHandler : IRequestHandler<GetEditRecipePositionsPackageViewModelQuery, EditRecipePositionsPackageViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetEditRecipePositionsPackageViewModelQueryHandler(
        IApplicationDbContext context
        )
    {
        _context = context;
    }

    public async Task<EditRecipePositionsPackageViewModel> Handle(GetEditRecipePositionsPackageViewModelQuery request, CancellationToken cancellationToken)
    {


        var RecipePositionsPackage = await _context.RecipePositionsPackages
            .Include(x => x.RecipeStage)
            .Include(x => x.RecipeStage.RecipeVersion)
            .Include(x => x.RecipeStage.RecipeVersion.Recipe)
            .Include(x => x.WorkCenter)
            .Include(x => x.LabourClass)
            .SingleOrDefaultAsync(x =>
               x.Id == request.RecipePositionsPackageId
                && x.RecipeStageId == request.RecipeStageId
            );

        var editRecipePositionsPackageCommand = new EditRecipePositionsPackageCommand
        {
            Id = request.RecipePositionsPackageId,
            PackageNumber = RecipePositionsPackage.PackageNumber,
            ProductNumber = RecipePositionsPackage.ProductNumber,
            ProductName = RecipePositionsPackage.ProductName,
            Description = RecipePositionsPackage.Description,
            RecipeId = RecipePositionsPackage.RecipeStage.RecipeVersion.RecipeId,
            RecipeVersionId = RecipePositionsPackage.RecipeStage.RecipeVersionId,
            RecipeStageId = RecipePositionsPackage.RecipeStageId,
            WorkCenterId = RecipePositionsPackage.WorkCenterId,
            WorkCenter = RecipePositionsPackage.WorkCenter,
            LabourClassId = RecipePositionsPackage.LabourClassId,
            LabourClass = RecipePositionsPackage.LabourClass,
            CrewSize = RecipePositionsPackage.CrewSize,
            TimeInSeconds = RecipePositionsPackage.TimeInSeconds,
            BagIsIncluded = RecipePositionsPackage.BagIsIncluded
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

        var vm = new EditRecipePositionsPackageViewModel
        {
            EditRecipePositionsPackageCommand = editRecipePositionsPackageCommand,
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




