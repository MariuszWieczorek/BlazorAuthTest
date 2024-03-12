using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.RouteVersions.Commands.CopyRouteVersion;

public class CopyRouteVersionCommandHandler : IRequestHandler<CopyRouteVersionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;

    public CopyRouteVersionCommandHandler(IApplicationDbContext context, IDateTimeService dateTimeService, ICurrentUserService currentUser)
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _currentUser = currentUser;
    }

    public async Task Handle(CopyRouteVersionCommand request, CancellationToken cancellationToken)
    {


        var product = await _context.Products
            .SingleAsync(x => x.Id == request.ProductId);



        await copyRouteVersion(request);


        await _context.SaveChangesAsync();

        return;

    }

    private async Task copyRouteVersion(CopyRouteVersionCommand request)
    {
        var routeVersion = await _context.RouteVersions
                    .SingleOrDefaultAsync(x => x.ProductId == request.ProductId && x.Id == request.RouteVersionId);




        var newRouteVersion = new RouteVersion
        {
            ProductId = request.ProductId,
            VersionNumber = routeVersion.VersionNumber,
            AlternativeNo = routeVersion.AlternativeNo,
            ProductCategoryId = routeVersion.ProductCategoryId,
            Name = routeVersion.Name,
            Description = routeVersion.Description,
            DefaultVersion = false,
            ToIfs = routeVersion.ToIfs,


            IsAccepted01 = false,
            Accepted01ByUserId = null,
            Accepted01Date = null,

            IsAccepted02 = false,
            Accepted02ByUser = null,
            Accepted02Date = null,

            CreatedByUserId = _currentUser.UserId,
            CreatedDate = _dateTimeService.Now,

            ModifiedByUser = null,
            ModifiedDate = null,

            ProductQty = routeVersion.ProductQty,

            IsActive = true
        };


        _context.RouteVersions.Add(newRouteVersion);

        await copyRouteVersionRoutes(request, newRouteVersion);


    }





    private async Task copyRouteVersionRoutes(CopyRouteVersionCommand request, RouteVersion newRouteVersion)
    {
        //x.ProductId == request.ProductId &&

        var routesToCopy = await _context.ManufactoringRoutes
                    .Where(x => x.RouteVersionId == request.RouteVersionId)
                    .AsNoTracking()
                    .ToListAsync();

        foreach (var route in routesToCopy)
        {

            var newRoute = new ManufactoringRoute
            {
               // TODO Dodać ProductId do ManufactoringRoutes
               // ProductId = request.ProductId,
                RouteVersion = newRouteVersion,
                OperationId= route.OperationId,
                OrdinalNumber = route.OrdinalNumber,
                OperationLabourConsumption=route.OperationLabourConsumption,
                OperationMachineConsumption=route.OperationMachineConsumption,
                WorkCenterId= route.WorkCenterId,
                ResourceId= route.ResourceId,
                ResourceQty= route.ResourceQty,
                ChangeOverResourceId= route.ChangeOverResourceId,
                ChangeOverLabourConsumption = route.ChangeOverLabourConsumption,
                ChangeOverNumberOfEmployee = route.ChangeOverNumberOfEmployee,
                ChangeOverMachineConsumption = route.ChangeOverMachineConsumption,
                MoveTime= route.MoveTime,
                Overlap= route.Overlap,
                ToolQuantity= route.ToolQuantity,
                RoutingToolId= route.RoutingToolId,
            };

            _context.ManufactoringRoutes.Add(newRoute);

        }
    }

}
