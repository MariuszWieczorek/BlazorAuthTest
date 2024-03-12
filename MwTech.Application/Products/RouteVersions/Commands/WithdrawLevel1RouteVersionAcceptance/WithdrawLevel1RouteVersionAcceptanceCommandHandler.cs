using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.RouteVersions.Commands.WithdrawLevel1RouteVersionAcceptance;


public class WithdrawLevel1RouteVersionAcceptanceCommandHandler : IRequestHandler<WithdrawLevel1RouteVersionAcceptanceCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public WithdrawLevel1RouteVersionAcceptanceCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }
    public async Task Handle(WithdrawLevel1RouteVersionAcceptanceCommand request, CancellationToken cancellationToken)
    {
        var RouteVersion = _context.RouteVersions
            .SingleOrDefault(x => x.ProductId == request.ProductId && x.Id == request.RouteVersionId);

        RouteVersion.IsAccepted01 = false;
        RouteVersion.Accepted01ByUserId = null;
        RouteVersion.Accepted01Date = null;
        

        await _context.SaveChangesAsync();

        return;
    }
}
