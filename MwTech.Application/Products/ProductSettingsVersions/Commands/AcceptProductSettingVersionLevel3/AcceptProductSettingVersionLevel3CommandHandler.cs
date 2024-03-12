using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductSettingVersions.Commands.AcceptProductSettingVersionLevel3;


public class AcceptProductSettingVersionLevel3CommandCommandHandler : IRequestHandler<AcceptProductSettingVersionLevel3Command>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;
    private readonly IProductCsvService _productCsvService;

    public AcceptProductSettingVersionLevel3CommandCommandHandler(IApplicationDbContext context,
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
    public async Task Handle(AcceptProductSettingVersionLevel3Command request, CancellationToken cancellationToken)
    {
        var productVersion = _context.ProductSettingVersions
            .SingleOrDefault(x => x.Id == request.ProductSettingVersionId && x.ProductId == request.ProductId);

        productVersion.IsAccepted03 = true;
        productVersion.Accepted03ByUserId = _currentUserService.UserId;
        productVersion.Accepted03Date = _dateTimeService.Now;
        productVersion.Rev = productVersion.Rev + 1;
        

        await _context.SaveChangesAsync();
        await _productCsvService.GenerateCsv(request.ProductId,request.ProductSettingVersionId, CsvTrigger.AcceptProductSettingVersionLevel3);

        return;
    }
}
