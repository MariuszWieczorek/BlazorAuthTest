using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductPropertiesVersions.Commands.AcceptProductPropertiesVersionLevel2;


public class AcceptProductPropertiesVersionLevel2CommandCommandHandler : IRequestHandler<AcceptProductPropertiesVersionLevel2Command>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;
    private readonly IProductCsvService _productCsvService;

    public AcceptProductPropertiesVersionLevel2CommandCommandHandler(IApplicationDbContext context,
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
    public async Task Handle(AcceptProductPropertiesVersionLevel2Command request, CancellationToken cancellationToken)
    {
        var ProductPropertiesVersion = _context.ProductPropertyVersions
            .SingleOrDefault(x => x.ProductId == request.ProductId && x.Id == request.ProductPropertiesVersionId);

        ProductPropertiesVersion.IsAccepted02 = true;
        ProductPropertiesVersion.Accepted02ByUserId = _currentUserService.UserId;
        ProductPropertiesVersion.Accepted02Date = _dateTimeService.Now;
        

        await _context.SaveChangesAsync();

        return;
    }
}
