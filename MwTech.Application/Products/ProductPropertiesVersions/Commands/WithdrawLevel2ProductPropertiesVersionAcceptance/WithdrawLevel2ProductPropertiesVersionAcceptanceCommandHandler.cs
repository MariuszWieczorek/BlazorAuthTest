using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductPropertiesVersions.Commands.WithdrawLevel2ProductPropertiesVersionAcceptance;


public class WithdrawLevel2ProductPropertiesVersionAcceptanceCommandHandler : IRequestHandler<WithdrawLevel2ProductPropertiesVersionAcceptanceCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public WithdrawLevel2ProductPropertiesVersionAcceptanceCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }
    public async Task Handle(WithdrawLevel2ProductPropertiesVersionAcceptanceCommand request, CancellationToken cancellationToken)
    {
        var ProductPropertiesVersion = _context.ProductPropertyVersions
            .SingleOrDefault(x => x.ProductId == request.ProductId && x.Id == request.ProductPropertiesVersionId);

        ProductPropertiesVersion.IsAccepted02 = false;
        ProductPropertiesVersion.Accepted02ByUserId = null;
        ProductPropertiesVersion.Accepted02Date = null;
        

        await _context.SaveChangesAsync();

        return;
    }
}
