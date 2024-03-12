using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using System.Linq.Dynamic.Core;
using System.Data.Common;
using MwTech.Domain.Entities.Ifs;
using MwTech.Application.Ifs.Common;

namespace MwTech.Application.Ifs.IfsWorkCenterOperations.Queries.GetIfsWorkCenterOperationsReports;

public class GetIfsWorkCenterOperationsReportsQueryHandler : IRequestHandler<GetIfsWorkCenterOperationsReportsQuery, IfsWorkCenterOperationsReportsViewModel>
{
    private readonly IOracleDbContext _oracleContext;
    private readonly IApplicationDbContext _context;
    private readonly IScadaIfsDbContext _scadaContext;
    private readonly IProductService _productWeightService;

    public GetIfsWorkCenterOperationsReportsQueryHandler(IOracleDbContext oracleContext,
        IApplicationDbContext context,
        IScadaIfsDbContext scadaContext,
        IProductService productWeightService
        )
    {
        _oracleContext = oracleContext;
        _context = context;
        _scadaContext = scadaContext;
        _productWeightService = productWeightService;
    }
    public async Task<IfsWorkCenterOperationsReportsViewModel> Handle(GetIfsWorkCenterOperationsReportsQuery request, CancellationToken cancellationToken)
    {



        var ifsOperationsReports = _oracleContext.IfsWorkCentersOperationsReports
                   .FromSqlRaw(
                   @$"
                   SELECT x.""DepartmentNo"", x.""WorkCenterNo"", x.""ProductionLine"", x.""OrderNo"", x.""SequenceNo"", x.""OperationId"",
                          x.""OpStartDay"",x.""OpStartMonthYear"", x.""OpStartDate"", x.""OpFinishDate"", x.""ProductNumber"", 
                          x.""RevisedQtyDue"", x.""QtyComplete"", x.""UnitMeas"",
                          x.""RowState"", x.""PlannedProductionTime"", x.""RealProductionTime"",
                          x.""Percent"", x.""RouteAltDescr"", x.""StructureAltDescr"",
                          x.""RealStartedDate"", x.""NumberOfEmployee"", x.""Priority"", x.""Shift"", x.""WeightNet"", x.""TotalWeightCompleted"", x.""TotalWeightRevised""  
                   FROM (  
                   SELECT
                    o.DEPARTMENT_NO as ""DepartmentNo"",
                    o.WORK_CENTER_NO as ""WorkCenterNo"",
                    o.PRODUCTION_LINE as ""ProductionLine"",
                    o.ORDER_NO as ""OrderNo"",
                    o.SEQUENCE_NO as ""SequenceNo"",
                    o.OP_ID as ""OperationId"",
                    o.OP_START_DATE as ""OpStartDate"",
                    to_char(o.OP_START_DATE,'DD.MM.YYYY') as ""OpStartDay"",
                    to_char(o.OP_START_DATE,'MM.YYYY') as ""OpStartMonthYear"",
                    o.OP_FINISH_DATE as ""OpFinishDate"",
                    o.PART_NO as ""ProductNumber"",
                    o.REVISED_QTY_DUE as ""RevisedQtyDue"",
                    o.QTY_COMPLETE as ""QtyComplete"",
                    INVENTORY_PART_API.Get_Unit_Meas(o.contract,o.PART_NO) as ""UnitMeas"",
                    o.ROWSTATE as ""RowState"",
                    o.CF$_PLANOWANY_CZAS_REALIZA as ""PlannedProductionTime"",
                    o.CF$_CZAS_TRWANIA_OPER as ""RealProductionTime"",
                    o.CF$_REALIZACJA_PLANU as ""Percent"",
                    o.CF$_ROUT_ALT_DESC as ""RouteAltDescr"",
                    o.CF$_ALTERNATIVE_DESCRIPTIO as ""StructureAltDescr"",
                    MIN(h.DATED) as ""RealStartedDate"",
                    coalesce(COUNT(DISTINCT h.CREATED_BY_EMPLOYEE_ID),0) as ""NumberOfEmployee"",
                    o.CF$_SHIFT as ""Shift"",
                    o.CF$_PRIORITY as ""Priority"",
                    pr.WEIGHT_NET as ""WeightNet"",
                    CASE WHEN INVENTORY_PART_API.Get_Unit_Meas(o.contract,o.PART_NO) != 'kg' THEN pr.WEIGHT_NET *  o.QTY_COMPLETE ELSE  o.QTY_COMPLETE END as ""TotalWeightCompleted"",
                    CASE WHEN INVENTORY_PART_API.Get_Unit_Meas(o.contract,o.PART_NO) != 'kg' THEN pr.WEIGHT_NET *  o.REVISED_QTY_DUE ELSE  o.REVISED_QTY_DUE END as ""TotalWeightRevised""
                    FROM ifsapp.SO_OPER_DISPATCH_LIST_cfv o
                    INNER JOIN ifsapp.PART_CATALOG_cfv  pr
                    ON pr.PART_NO = o.PART_NO
                    LEFT JOIN OPER_AND_IND_HIST_UIV h
                    ON o.ORDER_NO = h.order_no AND h.SEQUENCE_NO = o.SEQUENCE_NO AND h.OP_ID = o.OP_ID
                    WHERE 1 = 1 
                    -- and o.ROWSTATE in ('Released','Started','Planned')
                    -- AND (
                    -- to_char(o.OP_START_DATE,'MM.DD.YYYY') >= '03.28.2023'
                    -- o.OP_START_DATE >= TO_DATE('25.03.2023','DD.MM.YYYY')
                    -- AND
                    -- to_char(o.OP_START_DATE,'MM.DD.YYYY') <= '03.28.2023'
                    --  o.OP_START_DATE <= TO_DATE('31.03.2023','DD.MM.YYYY')
                    -- )
                    -- AND o.WORK_CENTER_NO LIKE ('LM%')
                    -- AND o.ORDER_NO = '195551'
                    GROUP BY 
                    o.DEPARTMENT_NO,
                    o.WORK_CENTER_NO,
                    o.PRODUCTION_LINE,
                    o.ORDER_NO,
                    o.SEQUENCE_NO,
                    o.OP_ID,
                    o.OP_START_DATE,
                    o.OP_FINISH_DATE,
                    o.PART_NO,
                    o.REVISED_QTY_DUE,
                    o.QTY_COMPLETE,
                    o.ROWSTATE,
                    o.CF$_PLANOWANY_CZAS_REALIZA,
                    o.CF$_CZAS_TRWANIA_OPER,
                    o.CF$_ROUT_ALT_DESC,
                    o.CF$_ALTERNATIVE_DESCRIPTIO,
                    o.contract,
                    o.CF$_SHIFT,
                    o.CF$_PRIORITY,
                    CF$_REALIZACJA_PLANU,
                    pr.WEIGHT_NET
                    ) x
                   ")
                   .AsNoTracking()
                   .AsQueryable();

        //SHOP_ORDER_OPERATION_API.GET_REMAINING_QTY(ORDER_NO, RELEASE_NO, SEQUENCE_NO, OPERATION_NO) AS REMAINING_QTY

        ifsOperationsReports = Filter(ifsOperationsReports, request.IfsWorkCenterOperationsReportsFilter);


        var ifsOperationsReportsList = await ifsOperationsReports
            .OrderBy(x => x.OpStartDate)
            .ThenBy(x => x.OrderNo)
            .ToListAsync();


        foreach (var item in ifsOperationsReportsList)
        {
            if (item.PlannedProductionTime.GetValueOrDefault() > 0)
                item.PercentTime = Math.Round(item.RealProductionTime.GetValueOrDefault() / item.PlannedProductionTime.GetValueOrDefault() * 100, 0);

            if (item.RevisedQtyDue.GetValueOrDefault() > 0)
                item.PercentQty = Math.Round(item.QtyComplete.GetValueOrDefault() / item.RevisedQtyDue.GetValueOrDefault() * 100, 0);

            if (item.TotalWeightRevised.GetValueOrDefault() > 0)
                item.PercentWeight = Math.Round(item.TotalWeightCompleted.GetValueOrDefault() / item.TotalWeightRevised.GetValueOrDefault() * 100, 0);
        }

        var reportSummary = new IfsWorkCenterOperationsSummary
        {
            TotalPlannedWeight = Math.Round(ifsOperationsReportsList.Sum(x => x.TotalWeightRevised.GetValueOrDefault()), 2),
            TotalRealWeight = Math.Round(ifsOperationsReportsList.Sum(x => x.TotalWeightCompleted.GetValueOrDefault()), 2),
            TotalPlannedTime = Math.Round(ifsOperationsReportsList.Sum(x => x.PlannedProductionTime.GetValueOrDefault()), 2),
            TotalRealTime = Math.Round(ifsOperationsReportsList.Sum(x => x.RealProductionTime.GetValueOrDefault()), 2),
            TotalPlannedQty = Math.Round(ifsOperationsReportsList.Sum(x => x.RevisedQtyDue.GetValueOrDefault()), 2),
            TotalRealQty = Math.Round(ifsOperationsReportsList.Sum(x => x.QtyComplete.GetValueOrDefault()), 2)
        };

        if (reportSummary.TotalPlannedTime != 0)
        {
            reportSummary.TimePercentComplete = Math.Round(reportSummary.TotalRealTime / reportSummary.TotalPlannedTime * 100, 2);
        }

        if (reportSummary.TotalPlannedWeight != 0)
        {
            reportSummary.WeightPercentComplete = Math.Round(reportSummary.TotalRealWeight / reportSummary.TotalPlannedWeight * 100, 2);
        }

        if (reportSummary.TotalPlannedQty != 0)
        {
            reportSummary.QtyPercentComplete = Math.Round(reportSummary.TotalRealQty / reportSummary.TotalPlannedQty * 100, 2);
        }

        var vm = new IfsWorkCenterOperationsReportsViewModel
        {
            IfsWorkCenterOperationsReportsFilter = request.IfsWorkCenterOperationsReportsFilter,
            IfsWorkCentersOperationsReports = ifsOperationsReportsList,
            IfsWorkCenterOperationsSummary = reportSummary

        };

        return vm;

    }

    public IQueryable<IfsWorkCenterOperationsReport> Filter(IQueryable<IfsWorkCenterOperationsReport> operations, IfsWorkCenterOperationsReportsFilter filter)
    {
        if (filter != null)
        {

            if (!string.IsNullOrWhiteSpace(filter.ProductNumber))
                operations = operations.Where(x => x.ProductNumber.ToUpper().Contains(filter.ProductNumber.ToUpper()));

            if (!string.IsNullOrWhiteSpace(filter.OrderNo))
                operations = operations.Where(x => x.OrderNo.ToUpper().Contains(filter.OrderNo.ToUpper()));

            if (!string.IsNullOrWhiteSpace(filter.Shift))
                operations = operations.Where(x => x.Shift.ToUpper().Contains(filter.Shift.ToUpper()));


            if (!string.IsNullOrWhiteSpace(filter.WorkCenterNo))
                if (filter.WorkCenterNo.Contains("%") || filter.WorkCenterNo.Contains("_"))
                {
                    operations = operations.Where(x => EF.Functions.Like(x.WorkCenterNo, filter.WorkCenterNo));
                }
                else
                {
                    operations = operations.Where(x => x.WorkCenterNo.ToUpper().Contains(filter.WorkCenterNo.ToUpper()));
                }


            if (!string.IsNullOrWhiteSpace(filter.DepartmentNo))
                if (filter.DepartmentNo.Contains("%") || filter.DepartmentNo.Contains("_"))
                {
                    operations = operations.Where(x => EF.Functions.Like(x.DepartmentNo, filter.DepartmentNo));
                }
                else
                {
                    operations = operations.Where(x => x.DepartmentNo.ToUpper().Contains(filter.DepartmentNo.ToUpper()));
                }

            if (filter.StartDateFrom != null)
            {
                operations = operations.Where(x => x.OpStartDate >= filter.StartDateFrom);
            }

            if (filter.StartDateTo != null)
            {
                operations = operations.Where(x => x.OpStartDate <= filter.StartDateTo);
            }


        }

        return operations;
    }
}
