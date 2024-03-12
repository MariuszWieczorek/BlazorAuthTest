using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using MwTech.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Ifs.IfsWorkCenterOperations.Command.GenerateStartCsv;

public class GenerateStartCsvCommandHandler : IRequestHandler<GenerateStartCsvCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<GenerateStartCsvCommandHandler> _logger;
    private readonly IProductCsvService _productCsvService;

    public GenerateStartCsvCommandHandler(IApplicationDbContext context,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUser,
        ILogger<GenerateStartCsvCommandHandler> logger,
        IProductCsvService productCsvService
        )
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _currentUser = currentUser;
        _logger = logger;
        _productCsvService = productCsvService;
    }
    public async Task Handle(GenerateStartCsvCommand request, CancellationToken cancellationToken)
    {
        _productCsvService.GenerateStartOrderCsv(request.ProductNumber, request.Qty, request.OrderNumber, request.WorkCenterNumber, request.OperationId);

        return;
    }


}
