using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.Products.Queries.GetProducts;
using MwTech.Application.Products.Routes.Commands.AddRoute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;
using Microsoft.AspNetCore.Http;
using MwTech.Domain.Entities;
using MwTech.Application.Operations.Queries.GetOperations;
using MwTech.Application.Resources.Queries.GetResources;
using MwTech.Application.RoutingTools.Queries.GetRoutingTools;

namespace MwTech.Application.Products.Routes.Queries.GetAddRouteViewModel;

public class GetAddRouteViewModelQueryHandler : IRequestHandler<GetAddRouteViewModelQuery, AddRouteViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetAddRouteViewModelQueryHandler(IApplicationDbContext context,
        IHttpContextAccessor httpContextAccessor
        )
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AddRouteViewModel> Handle(GetAddRouteViewModelQuery request, CancellationToken cancellationToken)
    {

        // todo ProductId = request.ProductId,
        var route = new ManufactoringRoute
        {
            //ProductId = request.ProductId,
            RouteVersionId = request.RouteVersionId
        };

        var routeVersion = await _context.RouteVersions
            .SingleOrDefaultAsync(x => x.Id == request.RouteVersionId);

        // todo ProductId - nie ma pola w encji
        // && x.ProductId == request.ProductId

        var addRouteCommand = new AddRouteCommand
        {
            OrdinalNumber = 0,
            ProductId = routeVersion.ProductId,
            Product = null,
            RouteVersionId = request.RouteVersionId,
            WorkCenterId = 0,
            WorkCenter = null,
            ResourceId = 0,
            Resource = null,
            ResourceQty = 0,
            ChangeOverResourceId = null,
            ChangeOverResource = null,
            OperationId = 0,
            Operation = null,
            OperationLabourConsumption = 0,
            OperationMachineConsumption = 0,
            ChangeOverNumberOfEmployee = 0,
            ChangeOverLabourConsumption = 0,
            ChangeOverMachineConsumption = 0,
            RoutingToolId = null,
            RoutingTool = null

        };


        var operations = _context.Operations
            .Include(x => x.ProductCategory)
            .Include(x => x.Unit)
            .AsNoTracking()
            .AsQueryable();

        operations = OperationsFilter(operations, request.OperationFilter);

        var resources = _context.Resources
            .Include(x => x.ProductCategory)
            .Include(x => x.Unit)
            .Include(x => x.LabourClass)
            .AsNoTracking()
            .AsQueryable();

        var routingTools = _context.RoutingTools
            .AsNoTracking()
            .AsQueryable();

        var getOperationsViewModel = new GetOperationsViewModel
        {
            ProductCategories = await _context.ProductCategories.OrderBy(x => x.OrdinalNumber).AsNoTracking().ToListAsync(),
            OperationFilter = request.OperationFilter,
            Operations = await operations.Take(500).ToListAsync(),
        };


        resources = ResourcesFilter(resources, request.ResourceFilter);
        
        routingTools = RoutingToolsFilter(routingTools, request.RoutingToolFilter);

        var getResourcesViewModel = new GetResourcesViewModel
        {
            ProductCategories = await _context.ProductCategories.OrderBy(x => x.OrdinalNumber).AsNoTracking().ToListAsync(),
            ResourceFilter = request.ResourceFilter,
            Resources = await resources.Take(500).ToListAsync(),
        };



        var getRoutingToolsViewModel = new GetRoutingToolsViewModel
        {
            ProductCategories = await _context.ProductCategories.OrderBy(x => x.OrdinalNumber).AsNoTracking().ToListAsync(),
            RoutingToolFilter = request.RoutingToolFilter,
            RoutingTools = await routingTools.Take(500).ToListAsync(),
        };

        var productCategories = await _context.ProductCategories
            .OrderBy(x => x.OrdinalNumber)
            .AsNoTracking()
            .ToListAsync();

        var vm = new AddRouteViewModel
        {
            AddRouteCommand = addRouteCommand,
            GetOperationsViewModel = getOperationsViewModel,
            GetResourcesViewModel = getResourcesViewModel,
            GetRoutingToolsViewModel = getRoutingToolsViewModel,
            ProductCategories = productCategories
        };

        return vm;

    }


    private IQueryable<Operation> OperationsFilter(IQueryable<Operation> operations, OperationFilter operationFilter)
    {
        if (operationFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(operationFilter.Name))
                operations = operations.Where(x => x.Name.Contains(operationFilter.Name));

            if (!string.IsNullOrWhiteSpace(operationFilter.OperationNumber))
                operations = operations.Where(x => x.OperationNumber.Contains(operationFilter.OperationNumber));

            if (operationFilter.ProductCategoryId != 0)
                operations = operations.Where(x => x.ProductCategoryId == operationFilter.ProductCategoryId);

        }

        return operations;
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
    private IQueryable<RoutingTool> RoutingToolsFilter(IQueryable<RoutingTool> routingTools, RoutingToolFilter routingToolFilter)
    {
        if (routingToolFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(routingToolFilter.Name))
                routingTools = routingTools.Where(x => x.Name.Contains(routingToolFilter.Name));

            if (!string.IsNullOrWhiteSpace(routingToolFilter.ToolNumber))
                routingTools = routingTools.Where(x => x.ToolNumber.Contains(routingToolFilter.ToolNumber));

           

        }

        return routingTools;
    }
}
