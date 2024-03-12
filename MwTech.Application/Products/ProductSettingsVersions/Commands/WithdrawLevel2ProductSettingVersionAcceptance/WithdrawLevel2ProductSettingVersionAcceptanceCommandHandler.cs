using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductSettingVersions.Commands.WithdrawLevel2ProductSettingVersionAcceptance;


public class WithdrawLevel2ProductSettingVersionAcceptanceCommandHandler : IRequestHandler<WithdrawLevel2ProductSettingVersionAcceptanceCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public WithdrawLevel2ProductSettingVersionAcceptanceCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }
    public async Task Handle(WithdrawLevel2ProductSettingVersionAcceptanceCommand request, CancellationToken cancellationToken)
    {
        var productVersion = _context.ProductSettingVersions
            .SingleOrDefault(x => x.Id == request.ProductSettingVersionId && x.ProductId == request.ProductId);

        productVersion.IsAccepted02 = false;
        productVersion.Accepted02ByUserId = null;
        productVersion.Accepted02Date = null;
        

        await _context.SaveChangesAsync();

        return;
    }
}
