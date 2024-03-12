using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Common.Models;
using MwTech.Application.Operations.Queries.GetOperations;
using MwTech.Application.Products.Routes.Commands.EditRoute;
using MwTech.Application.Resources.Queries.GetResources;
using MwTech.Application.RoutingTools.Queries.GetRoutingTools;
using MwTech.Domain.Entities;

namespace MwTech.Application.Products.Routes.Queries.GetEditRouteViewModel;

public class GetEditRouteViewModelQueryHandler : IRequestHandler<GetEditRouteViewModelQuery, EditRouteViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetEditRouteViewModelQueryHandler(IApplicationDbContext context,
        IHttpContextAccessor httpContextAccessor
        )
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<EditRouteViewModel> Handle(GetEditRouteViewModelQuery request, CancellationToken cancellationToken)
    {

        var route = _context.ManufactoringRoutes
            .Include(x => x.RouteVersion)
            .ThenInclude(x => x.Product)
            .Include(x => x.WorkCenter)
            .Include(x => x.Resource)
            .Include(x => x.RoutingTool)
            .Include(x => x.ChangeOverResource)
            .Include(x => x.Operation)
            .ThenInclude(x => x.Unit)
            .SingleOrDefault(x => x.Id == request.Id && x.RouteVersionId == request.RouteVersionId);


        // todo ProductId - nie ma pola w encji
        // && x.ProductId == request.ProductId

        var editRouteCommand = new EditRouteCommand
        {
            Id = request.Id,
            OrdinalNumber = route.OrdinalNumber,
            ProductId = route.RouteVersion.ProductId,
            Product = route.RouteVersion.Product,
            RouteVersionId = request.RouteVersionId,
            WorkCenterId = route.WorkCenterId,
            WorkCenter = route.WorkCenter,
            ResourceId = route.ResourceId,
            Resource = route.Resource,
            ResourceQty = route.ResourceQty,
            ChangeOverResourceId = route.ChangeOverResourceId,
            ChangeOverResource = route.ChangeOverResource,
            OperationId = route.OperationId,
            Operation = route.Operation,
            OperationLabourConsumption = route.OperationLabourConsumption,
            OperationMachineConsumption = route.OperationMachineConsumption,
            ChangeOverNumberOfEmployee = route.ChangeOverNumberOfEmployee,
            ChangeOverLabourConsumption = route.ChangeOverLabourConsumption,
            ChangeOverMachineConsumption = route.ChangeOverMachineConsumption,
            Overlap = route.Overlap,
            MoveTime = route.MoveTime,
            ProductCategoryId = route.ProductCategoryId,
            RoutingToolId = route.RoutingToolId,
            RoutingTool = route.RoutingTool,
            ToolQuantity = route.ToolQuantity,
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

        var getResourcesViewModel = new GetResourcesViewModel
        {
            ProductCategories = await _context.ProductCategories.OrderBy(x => x.OrdinalNumber).AsNoTracking().ToListAsync(),
            ResourceFilter = request.ResourceFilter,
            Resources = await resources.Take(500).ToListAsync()
        };

        routingTools = RoutingToolsFilter(routingTools, request.RoutingToolFilter);

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

        var vm = new EditRouteViewModel
        {
            EditRouteCommand = editRouteCommand,
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
