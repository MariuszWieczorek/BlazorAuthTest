using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities.Ifs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsScadaOperations.Queries.GetIfsOperations;

public class GetIfsScadaOperationsQueryHandler : IRequestHandler<GetIfsScadaOperationsQuery, IfsScadaOperationsViewModel>
{
    private readonly IOracleDbContext _oracleContext;
    private readonly IApplicationDbContext _context;
    private readonly IScadaIfsDbContext _scadaContext;

    public GetIfsScadaOperationsQueryHandler(IOracleDbContext oracleContext, IApplicationDbContext context, IScadaIfsDbContext scadaContext)
    {
        _oracleContext = oracleContext;
        _context = context;
        _scadaContext = scadaContext;
    }
    public async Task<IfsScadaOperationsViewModel> Handle(GetIfsScadaOperationsQuery request, CancellationToken cancellationToken)
    {

        var ifsOperations = _oracleContext.IfsScadaOperations
                   .FromSqlInterpolated(
                   @$"SELECT *
                   FROM IFSINFO.SCADA_OPERATIONS")
                 .AsNoTracking()
                 .AsQueryable();


        ifsOperations = Filter(ifsOperations, request.IfsScadaOperationFilter);

        var ifsOperationsList = await ifsOperations.ToListAsync();


        foreach (var item in ifsOperationsList)
        {

            var scadaReportedQty = _scadaContext.SCADA_REPORT
                .Where(x => x.OP_ID == item.OP_ID && x.TYPE == "REPORT")
                .Sum(x => x.QTY_REPORTED);



            item.QTY_SCADA_REPORTED = scadaReportedQty;

        }


        var vm = new IfsScadaOperationsViewModel
        {
            IfsScadaOperations = ifsOperationsList,
            IfsScadaOperationFilter = request.IfsScadaOperationFilter
        };

        return vm;

    }

    public IQueryable<IfsScadaOperation> Filter(IQueryable<IfsScadaOperation> operations, IfsScadaOperationFilter filter)
    {
        if (filter != null)
        {
            if (!string.IsNullOrWhiteSpace(filter.PartNo))
                operations = operations.Where(x => x.PART_NO.ToUpper().Contains(filter.PartNo.ToUpper()));

            if (!string.IsNullOrWhiteSpace(filter.OrderNo))
                operations = operations.Where(x => x.ORDERRNO.ToUpper().Contains(filter.OrderNo.ToUpper()));

            if (!string.IsNullOrWhiteSpace(filter.WorkCenterNo))
                operations = operations.Where(x => x.WORK_CENTER_NO.ToUpper().Contains(filter.WorkCenterNo.ToUpper()));


            if (filter.OpId != 0)
                operations = operations.Where(x => x.OP_ID == filter.OpId);
        }

        return operations;
    }
}
