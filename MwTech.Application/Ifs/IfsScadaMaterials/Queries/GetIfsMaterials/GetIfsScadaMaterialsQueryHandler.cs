using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Extensions;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities.Ifs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsScadaMaterials.Queries.GetIfsMaterials;

public class GetIfsScadaMaterialsQueryHandler : IRequestHandler<GetIfsScadaMaterialsQuery, IfsScadaMaterialsViewModel>
{
    private readonly IOracleDbContext _oracleContext;
    private readonly IApplicationDbContext _context;
    private readonly IScadaIfsDbContext _scadaContext;

    public GetIfsScadaMaterialsQueryHandler(IOracleDbContext oracleContext, IApplicationDbContext context, IScadaIfsDbContext scadaContext)
    {
        _oracleContext = oracleContext;
        _context = context;
        _scadaContext = scadaContext;
    }
    public async Task<IfsScadaMaterialsViewModel> Handle(GetIfsScadaMaterialsQuery request, CancellationToken cancellationToken)
    {

        string workCenterFilter = string.Empty;

        if (request.IfsScadaMaterialFilter?.WorkCenterNo != null)
        {
            workCenterFilter = $"and WORK_CENTER_NO = '{request.IfsScadaMaterialFilter?.WorkCenterNo}'";
        }



        IQueryable<IfsScadaMaterial> ifsMaterials = _oracleContext.IfsScadaMaterials
                   .FromSqlRaw($@"SELECT *
                   FROM IFSINFO.SCADA_MATERIALS
                   where 1 = 1 
                   {workCenterFilter}
                   and OBJSTATE in ('Released','Started','Planned')
                   and to_char(OP_START_DATE,'MM.DD.YYYY') = to_char(SYSDATE,'MM.DD.YYYY')
                   "
                   )
                 .AsNoTracking()
                 .AsQueryable();


        ifsMaterials = Filter(ifsMaterials, request.IfsScadaMaterialFilter);

        var ifsMaterialsList = await ifsMaterials.ToListAsync();


        foreach (var item in ifsMaterialsList)
        {

            var scadaReportedQty = _scadaContext.SCADA_REPORT
                .Where(x => x.OP_ID == item.OP_ID && x.TYPE == "ISSUE")
                .Sum(x => x.QTY_REPORTED);

            var scadaIssuedQty = _scadaContext.SCADA_REPORT
                .Where(x => x.OP_ID == item.OP_ID && x.TYPE == "ISSUE")
                .Sum(x => x.QTY_ISSUED);



            item.QTY_SCADA_REPORTED = scadaReportedQty;
            item.QTY_SCADA_ISSUED = scadaIssuedQty;

        }

        var vm = new IfsScadaMaterialsViewModel
        {
            IfsScadaMaterials = ifsMaterialsList,
            IfsScadaMaterialFilter = request.IfsScadaMaterialFilter
        };

        return vm;

    }

    public IQueryable<IfsScadaMaterial> Filter(IQueryable<IfsScadaMaterial> materials, IfsScadaMaterialFilter filter)
    {
        if (filter != null)
        {
            if (!string.IsNullOrWhiteSpace(filter.PartNo))
                materials = materials.Where(x => x.PART_NO.ToUpper().Contains(filter.PartNo.ToUpper()));

            if (!string.IsNullOrWhiteSpace(filter.OrderNo))
                materials = materials.Where(x => x.ORDERRNO.ToUpper().Contains(filter.OrderNo.ToUpper()));

            if (!string.IsNullOrWhiteSpace(filter.WorkCenterNo))
                materials = materials.Where(x => x.WORK_CENTER_NO.ToUpper().Contains(filter.WorkCenterNo.ToUpper()));


            if (filter.OpId != 0)
                materials = materials.Where(x => x.OP_ID == filter.OpId);
        }

        return materials;
    }
}
