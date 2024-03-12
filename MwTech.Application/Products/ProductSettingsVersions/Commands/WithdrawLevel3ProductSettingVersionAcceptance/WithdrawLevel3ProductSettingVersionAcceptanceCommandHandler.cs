using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductSettingVersions.Commands.WithdrawLevel3ProductSettingVersionAcceptance;


public class WithdrawLevel3ProductSettingVersionAcceptanceCommandHandler : IRequestHandler<WithdrawLevel3ProductSettingVersionAcceptanceCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public WithdrawLevel3ProductSettingVersionAcceptanceCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }
    public async Task Handle(WithdrawLevel3ProductSettingVersionAcceptanceCommand request, CancellationToken cancellationToken)
    {
        var productVersion = _context.ProductSettingVersions
            .SingleOrDefault(x => x.Id == request.ProductSettingVersionId && x.ProductId == request.ProductId);

        productVersion.IsAccepted03 = false;
        productVersion.Accepted03ByUserId = null;
        productVersion.Accepted03Date = null;
        

        await _context.SaveChangesAsync();

        return;
    }
}
