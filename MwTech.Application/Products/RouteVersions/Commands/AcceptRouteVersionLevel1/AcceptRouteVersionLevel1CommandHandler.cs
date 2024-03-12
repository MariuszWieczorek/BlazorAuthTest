using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.RouteVersions.Commands.AcceptRouteVersionLevel1;


public class AcceptRouteVersionLevel1CommandCommandHandler : IRequestHandler<AcceptRouteVersionLevel1Command>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public AcceptRouteVersionLevel1CommandCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }
    public async Task Handle(AcceptRouteVersionLevel1Command request, CancellationToken cancellationToken)
    {
        var routeVersion = _context.RouteVersions
            .SingleOrDefault(x => x.ProductId == request.ProductId && x.Id == request.RouteVersionId);

        routeVersion.IsAccepted01 = true;
        routeVersion.Accepted01ByUserId = _currentUserService.UserId;
        routeVersion.Accepted01Date = _dateTimeService.Now;
        

        await _context.SaveChangesAsync();

        return;
    }
}
