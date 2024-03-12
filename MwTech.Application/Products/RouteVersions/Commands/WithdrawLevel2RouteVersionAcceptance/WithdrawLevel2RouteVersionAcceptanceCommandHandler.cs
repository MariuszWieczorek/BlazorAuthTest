using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.RouteVersions.Commands.WithdrawLevel2RouteVersionAcceptance;


public class WithdrawLevel2RouteVersionAcceptanceCommandHandler : IRequestHandler<WithdrawLevel2RouteVersionAcceptanceCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public WithdrawLevel2RouteVersionAcceptanceCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }
    public async Task Handle(WithdrawLevel2RouteVersionAcceptanceCommand request, CancellationToken cancellationToken)
    {
        var RouteVersion = _context.RouteVersions
            .SingleOrDefault(x => x.ProductId == request.ProductId && x.Id == request.RouteVersionId);

        RouteVersion.IsAccepted02 = false;
        RouteVersion.Accepted02ByUserId = null;
        RouteVersion.Accepted02Date = null;
        

        await _context.SaveChangesAsync();

        return;
    }
}
