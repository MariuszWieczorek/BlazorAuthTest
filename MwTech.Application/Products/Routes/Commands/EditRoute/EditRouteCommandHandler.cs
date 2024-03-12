using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.Routes.Commands.EditRoute;

public class EditRouteCommandHandler : IRequestHandler<EditRouteCommand>
{
    private readonly IApplicationDbContext _context;

    public EditRouteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    
    public async Task Handle(EditRouteCommand request, CancellationToken cancellationToken)
    {
        var routeToEdit = _context.ManufactoringRoutes
            .SingleOrDefault(p => p.RouteVersionId == request.RouteVersionId && p.Id == request.Id);

        // todo routeToEdit.ProductId = request.Product
        routeToEdit.RouteVersionId = request.RouteVersionId;
        routeToEdit.OrdinalNumber = request.OrdinalNumber;
        routeToEdit.OperationId = request.OperationId;
        routeToEdit.WorkCenterId = request.WorkCenterId;
        routeToEdit.ResourceId = request.ResourceId;
        routeToEdit.ResourceQty = request.ResourceQty;
        routeToEdit.OperationLabourConsumption = request.OperationLabourConsumption;
        routeToEdit.OperationMachineConsumption = request.OperationMachineConsumption;
        routeToEdit.ChangeOverResourceId = request.ChangeOverResourceId;
        routeToEdit.ChangeOverNumberOfEmployee = request.ChangeOverNumberOfEmployee;
        routeToEdit.ChangeOverLabourConsumption = request.ChangeOverLabourConsumption;
        routeToEdit.ChangeOverMachineConsumption = request.ChangeOverMachineConsumption;
        routeToEdit.Overlap = request.Overlap;
        routeToEdit.MoveTime = request.MoveTime;
        routeToEdit.ProductCategoryId = request.ProductCategoryId; 
        routeToEdit.RoutingToolId = request.RoutingToolId;
        routeToEdit.ToolQuantity = request.ToolQuantity;

        await _context.SaveChangesAsync();

        return;
    }
}
