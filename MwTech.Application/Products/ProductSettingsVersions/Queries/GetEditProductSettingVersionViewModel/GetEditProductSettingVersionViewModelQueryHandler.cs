using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.Products.Commands.EditProduct;
using MwTech.Application.Products.ProductSettingVersions.Commands.EditProductSettingVersion;
using MwTech.Application.Resources.Queries.GetResources;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductSettingVersions.Queries.GetEditProductSettingVersionViewModel;

public class GetEditProductSettingVersionViewModelQueryHandler : IRequestHandler<GetEditProductSettingVersionViewModelQuery, EditProductSettingVersionViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetEditProductSettingVersionViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;

    }

    public async Task<EditProductSettingVersionViewModel> Handle(GetEditProductSettingVersionViewModelQuery request, CancellationToken cancellationToken)
    {

        var productSettingVersion = await _context.ProductSettingVersions
            .Include(x=>x.Machine)
            .Include(x=>x.MachineCategory)
            .Include(x=>x.WorkCenter)
            .FirstOrDefaultAsync(x => x.Id == request.ProductSettingVersionId && x.ProductId == request.ProductId);


        var product = _context.Products.SingleOrDefault(x => x.Id == productSettingVersion.ProductId);

        var positions = await GetProductSettingVersionPositions(request);

        var editProductSettingVersionCommand = new EditProductSettingVersionCommand
        {
            Id = request.ProductSettingVersionId,
            ProductId = productSettingVersion.ProductId,
            Product = product,
            MachineCategoryId = productSettingVersion.MachineCategoryId,
            MachineId = productSettingVersion.MachineId,
            WorkCenterId = productSettingVersion.WorkCenterId,
            WorkCenter = productSettingVersion.WorkCenter,
            ProductSettingVersionNumber = productSettingVersion.ProductSettingVersionNumber,
            AlternativeNo = productSettingVersion.AlternativeNo,
            Name = productSettingVersion.Name,
            DefaultVersion = productSettingVersion.DefaultVersion,
            Description = productSettingVersion.Description,
            IsAccepted01 = productSettingVersion.IsAccepted01,
            Accepted01ByUserId = productSettingVersion.Accepted01ByUserId,
            Accepted01Date = productSettingVersion.Accepted01Date,
            IsAccepted02 = productSettingVersion.IsAccepted02,
            Accepted02ByUserId = productSettingVersion.Accepted02ByUserId,
            Accepted02Date = productSettingVersion.Accepted02Date,
            IsAccepted03 = productSettingVersion.IsAccepted03,
            Accepted03ByUserId = productSettingVersion.Accepted03ByUserId,
            Accepted03Date = productSettingVersion.Accepted03Date,
            CreatedDate = productSettingVersion.CreatedDate,
            CreatedByUserId = productSettingVersion.CreatedByUserId,
            ModifiedDate = productSettingVersion.ModifiedDate,
            ModifiedByUserId = productSettingVersion.ModifiedByUserId,
            MwBaseId = productSettingVersion.MwbaseId,
            MwBaseName = productSettingVersion.MwBaseName,
            IsActive = productSettingVersion.IsActive,
            Tab = request.Tab,
            ReturnAddress = request.ReturnAddress,
            Anchor = request.Anchor    
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



        var vm = new EditProductSettingVersionViewModel
        {
            EditProductSettingVersionCommand = editProductSettingVersionCommand,
            ProductSettingVersionPositions = positions,
            Machines = await _context.Machines.ToListAsync(),
            GetResourcesViewModel = getResourcesViewModel,
            MachineCategories = await _context.MachineCategories.ToListAsync(),
            ReturnAddress = request.ReturnAddress,
            Anchor = request.Anchor
        };
        
        return vm;
    }

    private async Task<List<ProductSettingVersionPosition>> GetProductSettingVersionPositions(GetEditProductSettingVersionViewModelQuery request)
    {

        var positions = new List<ProductSettingVersionPosition>();

        /*
        var product = await _context.Products.SingleOrDefaultAsync(x=>x.Id == request.ProductId);
        
        var allPositions = await _context.Settings
            .Include(x => x.SettingCategory) 
            .Where(x=>x.SettingCategory.ProductCategoryId == product.ProductCategoryId)
            .ToListAsync();
        */

        var savedPositions = await _context.ProductSettingVersionPositions
                        .Include(x=>x.Setting)
                        .ThenInclude(x=>x.Unit)
                        .Include(x=>x.Setting)
                        .ThenInclude(x=>x.SettingCategory)
                        .Where(x=>x.ProductSettingVersionId == request.ProductSettingVersionId)
                        .OrderBy(x=>x.Setting.SettingCategory.OrdinalNumber)
                        .ThenBy(x=>x.Setting.OrdinalNumber)
                        .AsNoTracking()
                        .ToListAsync();



        return savedPositions;
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




