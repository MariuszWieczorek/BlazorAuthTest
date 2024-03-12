using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductSettingVersions.Commands.WithdrawLevel1ProductSettingVersionAcceptance;


public class WithdrawLevel1ProductSettingVersionAcceptanceCommandHandler : IRequestHandler<WithdrawLevel1ProductSettingVersionAcceptanceCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public WithdrawLevel1ProductSettingVersionAcceptanceCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }
    public async Task Handle(WithdrawLevel1ProductSettingVersionAcceptanceCommand request, CancellationToken cancellationToken)
    {
        var productVersion = _context.ProductSettingVersions
            .SingleOrDefault(x => x.Id == request.ProductSettingVersionId && x.ProductId == request.ProductId);

        productVersion.IsAccepted01 = false;
        productVersion.Accepted01ByUserId = null;
        productVersion.Accepted01Date = null;
        

        await _context.SaveChangesAsync();

        return;
    }
}
