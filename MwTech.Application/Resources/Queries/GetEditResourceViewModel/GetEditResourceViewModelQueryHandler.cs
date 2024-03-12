using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Resources.Commands.EditResource;
using MwTech.Application.Resources.Queries.GetResources;
using MwTech.Domain.Entities;

namespace MwTech.Application.Resources.Queries.GetEditResourceViewModel;

public class GetEditResourceViewModelQueryHandler : IRequestHandler<GetEditResourceViewModelQuery, EditResourceViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetEditResourceViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<EditResourceViewModel> Handle(GetEditResourceViewModelQuery request, CancellationToken cancellationToken)
    {

        var resource = await _context.Resources
            .Include(x=>x.LabourClass)
            .SingleAsync(x => x.Id == request.Id);

        var editResourceCommand = new EditResourceCommand
        {
            Id = resource.Id,
            Name = resource.Name,
            ResourceNumber = resource.ResourceNumber,
            Description = resource.Description,
            UnitId = resource.UnitId,
            ProductCategoryId = resource.ProductCategoryId,
            Markup = resource.Markup,
            Cost = resource.Cost,
            EstimatedCost = resource.EstimatedCost,
            EstimatedMarkup = resource.EstimatedMarkup,
            Contract = resource.Contract,
            LabourClassId = resource.LabourClassId,
            LabourClass = resource.LabourClass,
        };


        var resources = _context.Resources
            .Include(x => x.ProductCategory)
            .Include(x => x.Unit)
            .Include(x=>x.LabourClass)
            .AsNoTracking()
            .AsQueryable();

        resources = ResourcesFilter(resources, request.ResourceFilter);

        var getResourcesViewModel = new GetResourcesViewModel
        {
            ProductCategories = await _context.ProductCategories.OrderBy(x => x.OrdinalNumber).AsNoTracking().ToListAsync(),
            ResourceFilter = request.ResourceFilter,
            Resources = await resources.Take(500).ToListAsync(),
        };

        var vm = new EditResourceViewModel()
        {
            ProductCategories = await _context.ProductCategories.AsNoTracking().ToListAsync(),
            Units = await _context.Units.AsNoTracking().ToListAsync(),
            EditResourceCommand = editResourceCommand,
            GetResourcesViewModel = getResourcesViewModel
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
