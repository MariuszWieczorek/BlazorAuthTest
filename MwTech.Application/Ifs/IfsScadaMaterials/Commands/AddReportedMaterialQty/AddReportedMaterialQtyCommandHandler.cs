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

namespace MwTech.Application.Ifs.IfsScadaMaterials.Commands.AddReportedMaterialQty;

public class AddReportedMaterialQtyCommandHandler : IRequestHandler<AddReportedMaterialQtyCommand>
{
    private readonly IScadaIfsDbContext _scadaContext;
    private readonly IOracleDbContext _oracleContext;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUserService;

    public AddReportedMaterialQtyCommandHandler(IScadaIfsDbContext scadaContext,
        IOracleDbContext oracleContext,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUserService)
    {
        _scadaContext = scadaContext;
        _oracleContext = oracleContext;
        _dateTimeService = dateTimeService;
        _currentUserService = currentUserService;
    }

    public async Task Handle(AddReportedMaterialQtyCommand request, CancellationToken cancellationToken)
    {

        var ifsMaterials = _oracleContext.IfsScadaMaterials
                   .FromSqlInterpolated(
                   @$"SELECT *
                   FROM IFSINFO.SCADA_MATERIALS
                   WHERE OP_ID = {request.OP_ID} 
                    AND TRIM(COMPONENT_PART_NO) = TRIM({request.COMPONENT_PART_NO})
                    ")
                 .AsNoTracking()
                 .ToList();

        if (ifsMaterials.Count() != 1)
        {
            throw new Exception("Więcej niż jedena operacja o danym OP_ID, lub brak operacji w IFS");
        }

        var ifsOperation = ifsMaterials.Single(x => x.OP_ID == request.OP_ID);

        var scadaReport = new ScadaReport
        {
            OP_ID = request.OP_ID,
            QTY_ISSUED = request.QTY_ISSUED,
            TIMESTAMP = _dateTimeService.Now,
            TYPE = "ISSUE",
            PART_NO = ifsOperation.COMPONENT_PART_NO,
            REPORTED_BY = _currentUserService.UserName,
            WORK_CENTER_NO = ifsOperation.WORK_CENTER_NO,
            LOT_BATCH_NO = request.LOT_BATCH_NO ?? string.Empty,
            HANDLING_UNIT_ID = request.HANDLING_UNIT_ID ?? string.Empty,
            STATUS = "",
            RESOURCE_ID = ""
        };

        await _scadaContext.SCADA_REPORT.AddAsync(scadaReport);
        await _scadaContext.SaveChangesAsync();

        return;
    }
}
