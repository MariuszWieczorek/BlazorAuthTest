using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Resources.Queries.GetResources;

public class GetResourcesQueryHandler : IRequestHandler<GetResourcesQuery, GetResourcesViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetResourcesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<GetResourcesViewModel> Handle(GetResourcesQuery request, CancellationToken cancellationToken)
    {
        var resources = _context.Resources
            .Include(x=>x.Unit)
            .Include(x=>x.ProductCategory)
            .Include(x=>x.LabourClass)
            .AsNoTracking()
            .AsQueryable();

        resources = Filter(resources, request.ResourceFilter);

        // stronicowanie
        if (request.PagingInfo != null)
        {

            request.PagingInfo.TotalItems = resources.Count();
            request.PagingInfo.ItemsPerPage = 100;

            if (request.PagingInfo.ItemsPerPage > 0 && request.PagingInfo.TotalItems > 0)
                resources = resources
                    .Skip((request.PagingInfo.CurrentPage - 1) * request.PagingInfo.ItemsPerPage)
                    .Take(request.PagingInfo.ItemsPerPage);
        }

        var ResourcesList = await resources.ToListAsync();

        var vm = new GetResourcesViewModel
            { 
              Resources = ResourcesList,
              ResourceFilter = request.ResourceFilter,
              PagingInfo = request.PagingInfo,
              ProductCategories = await _context.ProductCategories.ToListAsync()
            };

        return vm;
           
    }

    public IQueryable<Resource> Filter(IQueryable<Resource> resources, ResourceFilter resourceFilter)
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
