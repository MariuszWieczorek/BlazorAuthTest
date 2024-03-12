using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductVersions.Commands.WithdrawLevel2ProductVersionAcceptance;


public class WithdrawLevel2ProductVersionAcceptanceCommandHandler : IRequestHandler<WithdrawLevel2ProductVersionAcceptanceCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public WithdrawLevel2ProductVersionAcceptanceCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }
    public async Task Handle(WithdrawLevel2ProductVersionAcceptanceCommand request, CancellationToken cancellationToken)
    {
        var productVersion = _context.ProductVersions
            .SingleOrDefault(x => x.ProductId == request.ProductId && x.Id == request.ProductVersionId);

        productVersion.IsAccepted02 = false;
        productVersion.Accepted02ByUserId = null;
        productVersion.Accepted02Date = null;
        

        await _context.SaveChangesAsync();

        return;
    }
}
