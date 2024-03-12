using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using System.Linq.Dynamic.Core;
using MwTech.Domain.Entities.Ifs;
using MwTech.Application.Ifs.Common;

namespace MwTech.Application.Ifs.IfsWorkCenterOperations.Queries.GetIfsWorkCenterOperationsGroupByMonthDepart;

public class GetIfsWorkCenterOperationsGroupByMonthDepartQueryHandler : IRequestHandler<GetIfsWorkCenterOperationsGroupByMonthDepartQuery, IfsWorkCenterOperationsGroupByMonthDepartViewModel>
{
    private readonly IOracleDbContext _oracleContext;
    private readonly IApplicationDbContext _context;
    private readonly IScadaIfsDbContext _scadaContext;
    private readonly IProductService _productWeightService;

    public GetIfsWorkCenterOperationsGroupByMonthDepartQueryHandler(IOracleDbContext oracleContext,
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
    public async Task<IfsWorkCenterOperationsGroupByMonthDepartViewModel> Handle(GetIfsWorkCenterOperationsGroupByMonthDepartQuery request, CancellationToken cancellationToken)
    {


        string WorkCenterNoLike = string.Empty;
        string DepartmentNoLike = string.Empty;
        string StartDateFrom = string.Empty;
        string StartDateTo = string.Empty;

        var filter = request.IfsWorkCenterOperationsReportsFilter;
        if (filter?.WorkCenterNo != null)
        {
            WorkCenterNoLike = $"AND o.WORK_CENTER_NO LIKE('{filter.WorkCenterNo}')";
        }

        if (filter?.DepartmentNo != null)
        {
            DepartmentNoLike = $"AND o.DEPARTMENT_NO LIKE('{filter.DepartmentNo}')";
        }

        if (filter?.StartDateFrom != null)
        {
            var startDate = filter.StartDateFrom.GetValueOrDefault().ToString("dd.MM.yyyy H.m");
            StartDateFrom = $"o.OP_START_DATE >= TO_DATE('{startDate}', 'DD.MM.YYYY  HH24:MI')";
        }

        if (filter?.StartDateTo != null)
        {
            var finishDate = filter.StartDateTo.GetValueOrDefault().ToString("dd.MM.yyyy H.m");
            StartDateTo = $"o.OP_START_DATE <= TO_DATE('{finishDate}', 'DD.MM.YYYY HH24:MI')";
        }

        var ifsOperationsGroupByMonthDepart = _oracleContext.IfsWorkCenterOperationsByMonthDepartReports
                   .FromSqlRaw(
                   @$"
                        SELECT x.""OpStartMonthYear"",
                        x.""DepartmentNo"",
                        x.""WorkCenterNo"",
                        x.""UnitMeas"",
                        SUM(x.""QtyComplete"") as ""QtyComplete"",
                        SUM(x.""RevisedQtyDue"") as ""RevisedQtyDue"",
                        SUM(x.""PlannedProductionTime"") as ""PlannedProductionTime"",
                        SUM(x.""RealProductionTime"") as ""RealProductionTime"",
                        SUM(x.""TotalWeightCompleted"") as ""TotalWeightCompleted"",
                        SUM(x.""TotalWeightRevised"") as ""TotalWeightRevised"",
                        ROUND((SUM(x.""RealProductionTime"") / SUM(x.""PlannedProductionTime"")) * 100, 0) as ""TimePercentComplete"",
                        ROUND((SUM(x.""QtyComplete"") / SUM(x.""RevisedQtyDue"")) * 100, 0) as ""QtyPercentComplete"",
                        ROUND((SUM(x.""TotalWeightCompleted"") / SUM(x.""TotalWeightRevised"")) * 100, 0) as ""WeightPercentComplete""
                        FROM
                        (
                        SELECT
                        o.DEPARTMENT_NO as ""DepartmentNo"",
                        o.WORK_CENTER_NO as ""WorkCenterNo"",
                        o.PRODUCTION_LINE as ""ProductionLine"",
                        o.ORDER_NO as ""OrderNo"",
                        o.OP_START_DATE as ""OpStartDate"",
                        to_char(o.OP_START_DATE, 'DD.MM.YYYY') as ""OpStartDay"",
                        to_char(o.OP_START_DATE, 'MM.YYYY') as ""OpStartMonthYear"",
                        o.OP_FINISH_DATE as ""OpFinishDate"",
                        o.PART_NO as ""ProductNumber"",
                        o.REVISED_QTY_DUE as ""RevisedQtyDue"",
                        o.QTY_COMPLETE as ""QtyComplete"",
                        INVENTORY_PART_API.Get_Unit_Meas(o.contract, o.PART_NO) as ""UnitMeas"",
                        o.ROWSTATE as ""RowState"",
                        o.CF$_PLANOWANY_CZAS_REALIZA as ""PlannedProductionTime"",
                        o.CF$_CZAS_TRWANIA_OPER as ""RealProductionTime"",
                        o.CF$_REALIZACJA_PLANU as ""Percent"",
                        o.CF$_ROUT_ALT_DESC as ""RouteAltDescr"",
                        o.CF$_ALTERNATIVE_DESCRIPTIO as ""StructureAltDescr"",
                        MIN(h.DATED) as ""RealStartedDate"",
                        coalesce(COUNT(DISTINCT h.CREATED_BY_EMPLOYEE_ID), 0) as ""NumberOfEmployee"",
                        o.CF$_SHIFT as ""Shift"",
                        o.CF$_PRIORITY as ""Priority"",
                        pr.WEIGHT_NET as ""WeightNet"",
                        CASE WHEN INVENTORY_PART_API.Get_Unit_Meas(o.contract, o.PART_NO) != 'kg' THEN pr.WEIGHT_NET * o.QTY_COMPLETE ELSE  o.QTY_COMPLETE END as ""TotalWeightCompleted"",
                        CASE WHEN INVENTORY_PART_API.Get_Unit_Meas(o.contract, o.PART_NO) != 'kg' THEN pr.WEIGHT_NET * o.REVISED_QTY_DUE ELSE  o.REVISED_QTY_DUE END as ""TotalWeightRevised""
                        FROM ifsapp.SO_OPER_DISPATCH_LIST_cfv o
                        INNER JOIN ifsapp.PART_CATALOG_cfv  pr
                        ON pr.PART_NO = o.PART_NO
                        LEFT JOIN OPER_AND_IND_HIST_UIV h
                        ON o.ORDER_NO = h.order_no
                        WHERE 1 = 1
                        -- and o.ROWSTATE in ('Released', 'Started', 'Planned')
                        AND(
                        {StartDateFrom}
                        AND
                        {StartDateTo}
                        )
                        {WorkCenterNoLike}
                        {DepartmentNoLike}
                        GROUP BY
                        o.DEPARTMENT_NO,
                        o.WORK_CENTER_NO,
                        o.PRODUCTION_LINE,
                        o.ORDER_NO,
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
                        group by x.""OpStartMonthYear"", x.""DepartmentNo"", x.""WorkCenterNo"", x.""UnitMeas""
                        order by x.""DepartmentNo"", x.""OpStartMonthYear"", x.""UnitMeas""
                        ")
                   .AsNoTracking()
                   .AsQueryable();

        //SHOP_ORDER_OPERATION_API.GET_REMAINING_QTY(ORDER_NO, RELEASE_NO, SEQUENCE_NO, OPERATION_NO) AS REMAINING_QTY

        // ifsOperationsGroupByMonthDepart = Filter(ifsOperationsGroupByMonthDepart, request.IfsWorkCenterOperationsGroupByMonthDepartFilter);


        var ifsOperationsGroupByMonthDepartList = await ifsOperationsGroupByMonthDepart
            .OrderBy(x => x.OpStartMonthYear)
            .ThenBy(x => x.DepartmentNo)
            .ThenBy(x => x.WorkCenterNo)
            .ToListAsync();


        foreach (var item in ifsOperationsGroupByMonthDepartList)
        {
            if (item.PlannedProductionTime.GetValueOrDefault() > 0)
                item.TimePercentComplete = Math.Round(item.RealProductionTime.GetValueOrDefault() / item.PlannedProductionTime.GetValueOrDefault() * 100, 0);

            if (item.RevisedQtyDue.GetValueOrDefault() > 0)
                item.QtyPercentComplete = Math.Round(item.QtyComplete.GetValueOrDefault() / item.RevisedQtyDue.GetValueOrDefault() * 100, 0);

            if (item.TotalWeightRevised.GetValueOrDefault() > 0)
                item.WeightPercentComplete = Math.Round(item.TotalWeightCompleted.GetValueOrDefault() / item.TotalWeightRevised.GetValueOrDefault() * 100, 0);
        }

        var reportSummary = new IfsWorkCenterOperationsSummary
        {
            TotalPlannedTime = Math.Round(ifsOperationsGroupByMonthDepartList.Sum(x => x.PlannedProductionTime.GetValueOrDefault()), 2),
            TotalRealTime = Math.Round(ifsOperationsGroupByMonthDepartList.Sum(x => x.RealProductionTime.GetValueOrDefault()), 2),
            TotalPlannedQty = Math.Round(ifsOperationsGroupByMonthDepartList.Sum(x => x.RevisedQtyDue.GetValueOrDefault()), 2),
            TotalRealQty = Math.Round(ifsOperationsGroupByMonthDepartList.Sum(x => x.QtyComplete.GetValueOrDefault()), 2),
            TotalPlannedWeight = Math.Round(ifsOperationsGroupByMonthDepartList.Sum(x => x.TotalWeightRevised.GetValueOrDefault()), 2),
            TotalRealWeight = Math.Round(ifsOperationsGroupByMonthDepartList.Sum(x => x.TotalWeightCompleted.GetValueOrDefault()), 2)
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

        var vm = new IfsWorkCenterOperationsGroupByMonthDepartViewModel
        {
            IfsWorkCenterOperationsGroupByMonthDepartFilter = request.IfsWorkCenterOperationsReportsFilter,
            IfsWorkCentersOperationsGroupByMonthDepart = ifsOperationsGroupByMonthDepartList,
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
