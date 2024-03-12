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

namespace MwTech.Application.ProductSettingsVersions.Commands.GenerateSingleProductSettingsCsv;

public class GenerateSingleProductSettingsCsvCommandHandler : IRequestHandler<GenerateSingleProductSettingsCsvCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<GenerateSingleProductSettingsCsvCommandHandler> _logger;
    private readonly IProductCsvService _productCsvService;

    public GenerateSingleProductSettingsCsvCommandHandler(IApplicationDbContext context,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUser,
        ILogger<GenerateSingleProductSettingsCsvCommandHandler> logger,
        IProductCsvService productCsvService
        )
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _currentUser = currentUser;
        _logger = logger;
        _productCsvService = productCsvService;
    }
    public async Task Handle(GenerateSingleProductSettingsCsvCommand request, CancellationToken cancellationToken)
    {

        await _productCsvService.GenerateCsv(request.ProductId, request.ProductSettingsVersionId, CsvTrigger.CsvGenerator);
        return;
    }

    
}
