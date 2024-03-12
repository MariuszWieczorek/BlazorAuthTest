/* IFS Zapytanie grupuj¹ce wykonanie */

SELECT x."OpMonthYear",
x."DepartmentNo",
x."WorkCenterNo",
x."UnitMeas",
SUM(x."QtyComplete") as "QtyComplete",
SUM(x."RevisedQtyDue") as "RevisedQtyDue",
SUM(x."PlannedProductionTime") as "PlannedProductionTime",
SUM(x."RealProductionTime") as "RealProductionTime",
SUM(x."TotalWeightRevised") as "TotalWeightRevised",
SUM(x."TotalWeightCompleted") as "TotalWeightCompleted",
ROUND((SUM(x."RealProductionTime") / SUM(x."PlannedProductionTime")) * 100,0) as "TimePercentComplete",
ROUND((SUM(x."QtyComplete") / SUM(x."RevisedQtyDue")) * 100,0) as "QtyPercentComplete",
ROUND((SUM(x."TotalWeightCompleted") / SUM(x."TotalWeightRevised")) * 100,0) as "WeightPercentComplete"
FROM
(
SELECT
                        o.DEPARTMENT_NO as "DepartmentNo",
                        o.WORK_CENTER_NO as "WorkCenterNo",
                        o.PRODUCTION_LINE as "ProductionLine",
                        o.ORDER_NO as "OrderNo",
                        o.OP_START_DATE as "OpStartDate",
                        to_char(o.OP_START_DATE,'DD.MM.YYYY') as "OpStartDay",
                        to_char(o.OP_START_DATE,'MM.YYYY') as "OpMonthYear",
                        o.OP_FINISH_DATE as "OpFinishDate",
                        o.PART_NO as "ProductNumber",
                        o.REVISED_QTY_DUE as "RevisedQtyDue",
                        o.QTY_COMPLETE as "QtyComplete",
                        INVENTORY_PART_API.Get_Unit_Meas(o.contract,o.PART_NO) as "UnitMeas",
                        o.ROWSTATE as "RowState",
                        o.CF$_PLANOWANY_CZAS_REALIZA as "PlannedProductionTime",
                        o.CF$_CZAS_TRWANIA_OPER as "RealProductionTime",
                        o.CF$_REALIZACJA_PLANU as "Percent",
                        o.CF$_ROUT_ALT_DESC as "RouteAltDescr",
                        o.CF$_ALTERNATIVE_DESCRIPTIO as "StructureAltDescr",
                        MIN(h.DATED) as "RealStartedDate",
                        coalesce(COUNT(DISTINCT h.CREATED_BY_EMPLOYEE_ID),0) as "NumberOfEmployee",
                        o.CF$_SHIFT as "Shift",
                        o.CF$_PRIORITY as "Priority",
                        pr.WEIGHT_NET as "WeightNet",
                        CASE WHEN INVENTORY_PART_API.Get_Unit_Meas(o.contract,o.PART_NO) != 'kg' THEN pr.WEIGHT_NET *  o.QTY_COMPLETE ELSE  o.QTY_COMPLETE END as "TotalWeightCompleted",
                        CASE WHEN INVENTORY_PART_API.Get_Unit_Meas(o.contract,o.PART_NO) != 'kg' THEN pr.WEIGHT_NET *  o.REVISED_QTY_DUE ELSE  o.REVISED_QTY_DUE END as "TotalWeightRevised"
                        FROM ifsapp.SO_OPER_DISPATCH_LIST_cfv o
                        INNER JOIN ifsapp.PART_CATALOG_cfv  pr
                        ON pr.PART_NO = o.PART_NO
                        LEFT JOIN OPER_AND_IND_HIST_UIV h
                        ON o.ORDER_NO = h.order_no
                        WHERE 1 = 1 
                        -- and o.ROWSTATE in ('Released','Started','Planned')
                        AND (
                       o.OP_START_DATE >= TO_DATE('01.03.2023 0.0', 'DD.MM.YYYY  HH24:MI')
                            AND
                            o.OP_START_DATE <= TO_DATE('31.03.2023 23.59', 'DD.MM.YYYY HH24:MI')
                        )
                        AND o.WORK_CENTER_NO LIKE ('LM%')
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
--where x."OpMonthYear" = '03.2023'
--and  x."WorkCenterNo" like 'LM%'
group by x."OpMonthYear", x."DepartmentNo", x."WorkCenterNo",x."UnitMeas"
order by x."DepartmentNo", x."OpMonthYear",x."UnitMeas"
