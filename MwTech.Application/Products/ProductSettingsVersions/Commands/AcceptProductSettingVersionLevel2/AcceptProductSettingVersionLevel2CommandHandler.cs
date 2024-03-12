using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductSettingVersions.Commands.AcceptProductSettingVersionLevel2;


public class AcceptProductSettingVersionLevel2CommandCommandHandler : IRequestHandler<AcceptProductSettingVersionLevel2Command>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;
    private readonly IProductCsvService _productCsvService;

    public AcceptProductSettingVersionLevel2CommandCommandHandler(IApplicationDbContext context,
        ICurrentUserService currentUserService,
        IDateTimeService dateTimeService,
        IProductCsvService productCsvService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
        _productCsvService = productCsvService;
    }
    public async Task Handle(AcceptProductSettingVersionLevel2Command request, CancellationToken cancellationToken)
    {
        var productVersion = _context.ProductSettingVersions
            .SingleOrDefault(x => x.Id == request.ProductSettingVersionId && x.ProductId == request.ProductId);

        /*
        var lastRev = _context.ProductSettingVersions
            .Where(x => x.ProductId == request.ProductId && x.AlternativeNo == productVersion.AlternativeNo && x.ProductSettingVersionNumber == productVersion.ProductSettingVersionNumber)
            .Max(x => x.Rev);
        */

        productVersion.IsAccepted02 = true;
        productVersion.Accepted02ByUserId = _currentUserService.UserId;
        productVersion.Accepted02Date = _dateTimeService.Now;
        productVersion.Rev = productVersion.Rev + 1;
        

        await _context.SaveChangesAsync();
        await _productCsvService.GenerateCsv(request.ProductId,request.ProductSettingVersionId, CsvTrigger.AcceptProductSettingVersionLevel2);

        return;
    }
}
