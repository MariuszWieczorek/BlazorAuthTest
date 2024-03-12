using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductVersions.Commands.AcceptProductVersionLevel2;


public class AcceptProductVersionLevel2CommandCommandHandler : IRequestHandler<AcceptProductVersionLevel2Command>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public AcceptProductVersionLevel2CommandCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }
    public async Task Handle(AcceptProductVersionLevel2Command request, CancellationToken cancellationToken)
    {
        var productVersion = _context.ProductVersions
            .SingleOrDefault(x => x.ProductId == request.ProductId && x.Id == request.ProductVersionId);

        productVersion.IsAccepted02 = true;
        productVersion.Accepted02ByUserId = _currentUserService.UserId;
        productVersion.Accepted02Date = _dateTimeService.Now;
        

        await _context.SaveChangesAsync();

        return;
    }
}
