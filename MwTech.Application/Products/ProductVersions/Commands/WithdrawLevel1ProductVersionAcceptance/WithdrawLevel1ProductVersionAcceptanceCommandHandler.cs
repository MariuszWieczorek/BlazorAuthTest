using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductVersions.Commands.WithdrawLevel1ProductVersionAcceptance;


public class WithdrawLevel1ProductVersionAcceptanceCommandHandler : IRequestHandler<WithdrawLevel1ProductVersionAcceptanceCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public WithdrawLevel1ProductVersionAcceptanceCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }
    public async Task Handle(WithdrawLevel1ProductVersionAcceptanceCommand request, CancellationToken cancellationToken)
    {
        var productVersion = _context.ProductVersions
            .SingleOrDefault(x => x.ProductId == request.ProductId && x.Id == request.ProductVersionId);

        productVersion.IsAccepted01 = false;
        productVersion.Accepted01ByUserId = null;
        productVersion.Accepted01Date = null;
        

        await _context.SaveChangesAsync();

        return;
    }
}
