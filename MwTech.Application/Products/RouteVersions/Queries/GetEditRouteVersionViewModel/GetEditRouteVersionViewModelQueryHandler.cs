using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.Products.Commands.EditProduct;
using MwTech.Application.Products.RouteVersions.Commands.EditRouteVersion;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.RouteVersions.Queries.GetEditRouteVersionViewModel;

public class GetEditRouteVersionViewModelQueryHandler : IRequestHandler<GetEditRouteVersionViewModelQuery, EditRouteVersionViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IProductService _productWeightService;
    private readonly IProductCostService _productCostService;

    public GetEditRouteVersionViewModelQueryHandler(
        IApplicationDbContext context,
        IProductService productWeightService,
        IProductCostService productCostService
        )
    {
        _context = context;
        _productWeightService = productWeightService;
        _productCostService = productCostService;
    }

    public async Task<EditRouteVersionViewModel> Handle(GetEditRouteVersionViewModelQuery request, CancellationToken cancellationToken)
    {

        var RouteVersion = await _context.RouteVersions
            .FirstOrDefaultAsync(x => x.Id == request.RouteVersionId);

        // todo  && x.ProductId == request.ProductId

        var routes = await GetRoutes(request);

        var editRouteVersionCommand = new EditRouteVersionCommand
        {
            Id = request.RouteVersionId,
            ProductId = request.ProductId,
            Product = await _context.Products.SingleOrDefaultAsync(x=>x.Id == request.ProductId),
            VersionNumber = RouteVersion.VersionNumber,
            AlternativeNo = RouteVersion.AlternativeNo,   
            Name = RouteVersion.Name,
            Description = RouteVersion.Description,
            ProductQty = RouteVersion.ProductQty,
            IsActive = RouteVersion.IsActive,
            ToIfs = RouteVersion.ToIfs,
            ModifiedByUserId = RouteVersion.ModifiedByUserId,
            ModifiedDate = RouteVersion.ModifiedDate,
            IsAccepted01 = RouteVersion.IsAccepted01,
            IsAccepted02 = RouteVersion.IsAccepted02,
            ProductCategoryId = RouteVersion.ProductCategoryId
        };


        var vm = new EditRouteVersionViewModel
        {
            EditRouteVersionCommand = editRouteVersionCommand,
            ProductCategories = await _context.ProductCategories.AsNoTracking().ToListAsync(),
            Routes = routes
        };
        return vm;
    }

    private async Task<List<ManufactoringRoute>> GetRoutes(GetEditRouteVersionViewModelQuery request)
    {

        // TODO && x.ProductId == request.ProductId

        var routes = await _context.ManufactoringRoutes
                .Include(x => x.RouteVersion)
                .ThenInclude(x => x.Product)
                .Include(x=>x.RoutingTool)
                .Include(x => x.Operation)
                .ThenInclude(x => x.Unit)
                .Include(x => x.Resource)
                .ThenInclude(x => x.Unit)
                .Include(x => x.WorkCenter)
                .ThenInclude(x => x.Unit)
                .Include(x => x.ProductCategory)
                .Include(x=>x.ChangeOverResource)
                .Where(x => x.RouteVersionId == request.RouteVersionId)
                .OrderBy(x => x.OrdinalNumber)
                .AsNoTracking()
                .ToListAsync();


        routes = await _productCostService.GetRoutesCost(routes);

        return routes;
    }


}




