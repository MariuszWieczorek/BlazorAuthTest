using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsScadaOperations.Commands.AddReportedProductQty;

public class AddReportedProductQtyCommandHandler : IRequestHandler<AddReportedProductQtyCommand>
{
    private readonly IScadaIfsDbContext _scadaContext;
    private readonly IOracleDbContext _oracleContext;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUserService;

    public AddReportedProductQtyCommandHandler(IScadaIfsDbContext scadaContext,
        IOracleDbContext oracleContext,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUserService)
    {
        _scadaContext = scadaContext;
        _oracleContext = oracleContext;
        _dateTimeService = dateTimeService;
        _currentUserService = currentUserService;
    }

    public async Task Handle(AddReportedProductQtyCommand request, CancellationToken cancellationToken)
    {

        var ifsOperations = _oracleContext.IfsScadaOperations
                   .FromSqlInterpolated(
                   @$"SELECT *
                   FROM IFSINFO.SCADA_OPERATIONS
                   WHERE OP_ID = {request.OP_ID} 
                    ")
                 .AsNoTracking()
                 .ToList();

        if (ifsOperations.Count() != 1)
        {
            throw new Exception("Więcej niż jedena operacja o danym OP_ID, lub brak operacji w IFS");
        }

        var ifsOperation = ifsOperations.Single(x => x.OP_ID == request.OP_ID);

        var scadaReport = new ScadaReport
        {
            OP_ID = request.OP_ID,
            QTY_REPORTED = request.QTY_REPORTED,
            TIMESTAMP = _dateTimeService.Now,
            TYPE = "REPORT",
            PART_NO = ifsOperation.PART_NO,
            REPORTED_BY = _currentUserService.UserName,
            WORK_CENTER_NO = ifsOperation.WORK_CENTER_NO,
            LOT_BATCH_NO = request.LOT_BATCH_NO ?? string.Empty,
            HANDLING_UNIT_ID = request.HANDLING_UNIT_ID ?? string.Empty,
            TIME_CONSUMED = request.TIME_CONSUMED,
            STATUS = "",
            RESOURCE_ID = ""
        };

        await _scadaContext.SCADA_REPORT.AddAsync(scadaReport);
        await _scadaContext.SaveChangesAsync();

        return;
    }
}
