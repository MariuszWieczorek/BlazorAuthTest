using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductSettingVersions.Commands.AcceptProductSettingVersionLevel1;


public class AcceptProductSettingVersionLevel1CommandCommandHandler : IRequestHandler<AcceptProductSettingVersionLevel1Command>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;
    private readonly IProductCsvService _productCsvService;

    public AcceptProductSettingVersionLevel1CommandCommandHandler(IApplicationDbContext context,
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
    public async Task Handle(AcceptProductSettingVersionLevel1Command request, CancellationToken cancellationToken)
    {
        var productVersion = _context.ProductSettingVersions
            .SingleOrDefault(x => x.Id == request.ProductSettingVersionId && x.ProductId == request.ProductId);

        productVersion.IsAccepted01 = true;
        productVersion.Accepted01ByUserId = _currentUserService.UserId;
        productVersion.Accepted01Date = _dateTimeService.Now;
        

        await _context.SaveChangesAsync();

        await _productCsvService.GenerateCsv(request.ProductId,request.ProductSettingVersionId, CsvTrigger.AcceptProductSettingVersionLevel1);

        return;
    }
}
