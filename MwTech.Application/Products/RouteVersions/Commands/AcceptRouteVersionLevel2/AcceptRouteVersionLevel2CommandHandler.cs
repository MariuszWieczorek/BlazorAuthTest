using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.RouteVersions.Commands.AcceptRouteVersionLevel2;


public class AcceptRouteVersionLevel2CommandCommandHandler : IRequestHandler<AcceptRouteVersionLevel2Command>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public AcceptRouteVersionLevel2CommandCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }
    public async Task Handle(AcceptRouteVersionLevel2Command request, CancellationToken cancellationToken)
    {
        var routeVersion = _context.RouteVersions
            .SingleOrDefault(x => x.ProductId == request.ProductId && x.Id == request.RouteVersionId);

        routeVersion.IsAccepted02 = true;
        routeVersion.Accepted02ByUserId = _currentUserService.UserId;
        routeVersion.Accepted02Date = _dateTimeService.Now;
        

        await _context.SaveChangesAsync();

        return;
    }
}
