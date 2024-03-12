using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.Products.Commands.AddProduct;
using MwTech.Application.Products.ProductSettingVersions.Commands.AddProductSettingVersion;
using MwTech.Application.Resources.Queries.GetResources;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductSettingVersions.Queries.GetAddProductSettingVersionViewModel;

public class GetAddProductSettingVersionViewModelQueryHandler : IRequestHandler<GetAddProductSettingVersionViewModelQuery, AddProductSettingVersionViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public GetAddProductSettingVersionViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AddProductSettingVersionViewModel> Handle(GetAddProductSettingVersionViewModelQuery request, CancellationToken cancellationToken)
    {

        var ProductSettingVersions = await _context.ProductSettingVersions
            .Where(x => x.ProductId == request.ProductId).ToListAsync();

        int settingVersionNumber = 1;
        int maxAlternativeNo = 1;


        if (ProductSettingVersions.Any())
        {
            settingVersionNumber = ProductSettingVersions.Max(x => x.ProductSettingVersionNumber) + 1; 
        }
        

        var AddProductSettingVersionCommand = new AddProductSettingVersionCommand
        {
            ProductId = request.ProductId,
            MachineCategoryId = request.MachineCategoryId, 
            MachineId = request.MachineId,
            WorkCenterId = request.WorkCenterId,
            ProductSettingVersionNumber = settingVersionNumber,
            AlternativeNo = maxAlternativeNo,
            Name = string.Empty,
            DefaultVersion = false,
            Description = null,
        };

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


        var vm = new AddProductSettingVersionViewModel
        {
            AddProductSettingVersionCommand = AddProductSettingVersionCommand,
            Machines = await _context.Machines.ToListAsync(),
            GetResourcesViewModel = getResourcesViewModel,
            MachineCategories = await _context.MachineCategories.ToListAsync()
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
