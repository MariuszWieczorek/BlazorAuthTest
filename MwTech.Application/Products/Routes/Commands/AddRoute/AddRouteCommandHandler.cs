using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.Routes.Commands.AddRoute;

public class AddRouteCommandHandler : IRequestHandler<AddRouteCommand>
{
    private readonly IApplicationDbContext _context;

    public AddRouteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    
    public async Task Handle(AddRouteCommand request, CancellationToken cancellationToken)
    {
        var routeToAdd = new ManufactoringRoute
        {
            // ProductId = request.ProductId,
            RouteVersionId = request.RouteVersionId,
            OperationId = request.OperationId, 
            OrdinalNumber = request.OrdinalNumber,
           
            WorkCenterId = request.WorkCenterId,
            ResourceId = request.ResourceId,
            ResourceQty = request.ResourceQty,
            OperationLabourConsumption = request.OperationLabourConsumption,
            OperationMachineConsumption = request.OperationMachineConsumption,
            ChangeOverResourceId = request.ChangeOverResourceId,
            ChangeOverNumberOfEmployee = request.ChangeOverNumberOfEmployee,
            ChangeOverLabourConsumption = request.ChangeOverLabourConsumption,
            ChangeOverMachineConsumption = request.ChangeOverMachineConsumption,
            Overlap = request.Overlap,
            MoveTime = request.MoveTime,
            ProductCategoryId = request.ProductCategoryId,
            RoutingToolId = request.RoutingToolId,
            ToolQuantity = request.ToolQuantity,
        };

        _context.ManufactoringRoutes.Add(routeToAdd);

        await _context.SaveChangesAsync();
        return;
    }
}
