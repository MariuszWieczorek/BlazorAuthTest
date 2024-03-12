using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductPropertiesVersions.Commands.WithdrawLevel1ProductPropertiesVersionAcceptance;


public class WithdrawLevel1ProductPropertiesVersionAcceptanceCommandHandler : IRequestHandler<WithdrawLevel1ProductPropertiesVersionAcceptanceCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public WithdrawLevel1ProductPropertiesVersionAcceptanceCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }
    public async Task Handle(WithdrawLevel1ProductPropertiesVersionAcceptanceCommand request, CancellationToken cancellationToken)
    {
        var ProductPropertiesVersion = _context.ProductPropertyVersions
            .SingleOrDefault(x => x.ProductId == request.ProductId && x.Id == request.ProductPropertiesVersionId);

        ProductPropertiesVersion.IsAccepted01 = false;
        ProductPropertiesVersion.Accepted01ByUserId = null;
        ProductPropertiesVersion.Accepted01Date = null;
        

        await _context.SaveChangesAsync();

        return;
    }
}
