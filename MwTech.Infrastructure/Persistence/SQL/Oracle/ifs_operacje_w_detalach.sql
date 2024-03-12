SELECT "m"."DepartmentNo", "m"."NumberOfEmployee", "m"."OpFinishDate", "m"."OpStartDate", "m"."OpStartDay", "m"."OrderNo", "m"."SequenceNo", "m"."PlannedProductionTime", "m"."Priority", "m"."ProductNumber", "m"."ProductionLine", "m"."QtyComplete", "m"."RealProductionTime", "m"."RealStartedDate", "m"."RevisedQtyDue", "m"."RouteAltDescr", "m"."RowState", "m"."Shift", "m"."StructureAltDescr", "m"."TotalWeightCompleted", "m"."TotalWeightRevised", "m"."UnitMeas", "m"."WeightNet", "m"."WorkCenterNo"
FROM (

                       SELECT x."DepartmentNo", x."WorkCenterNo", x."ProductionLine", x."OrderNo", x."SequenceNo",
                              x."OpStartDay", x."OpStartDate", x."OpFinishDate", x."ProductNumber", 
                              x."RevisedQtyDue", x."QtyComplete", x."UnitMeas",
                              x."RowState", x."PlannedProductionTime", x."RealProductionTime",
                              x."Percent", x."RouteAltDescr", x."StructureAltDescr",
                              x."RealStartedDate", x."NumberOfEmployee", x."Priority", x."Shift", x."WeightNet", x."TotalWeightCompleted", x."TotalWeightRevised"  
                       FROM (  
                       SELECT
                        o.DEPARTMENT_NO as "DepartmentNo",
                        o.WORK_CENTER_NO as "WorkCenterNo",
                        o.PRODUCTION_LINE as "ProductionLine",
                        o.ORDER_NO as "OrderNo",
                        o.SEQUENCE_NO as "SequenceNo",
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
                        ON o.ORDER_NO = h.order_no and o.sequence_no = h.sequence_no and o.op_id = h.op_id
                        WHERE 1 = 1 
                        -- and o.ROWSTATE in ('Released','Started','Planned')
                        AND (
                        -- to_char(o.OP_START_DATE,'MM.DD.YYYY') >= '03.28.2023'
                        o.OP_START_DATE >= TO_DATE('01.03.2023','DD.MM.YYYY')
                        AND
                        -- to_char(o.OP_START_DATE,'MM.DD.YYYY') <= '03.28.2023'
                        o.OP_START_DATE <= TO_DATE('31.03.2023','DD.MM.YYYY')
                        )
                        AND o.WORK_CENTER_NO LIKE ('LM%')
                        -- AND o.ORDER_NO = '195551'
                        GROUP BY 
                        o.DEPARTMENT_NO,
                        o.WORK_CENTER_NO,
                        o.PRODUCTION_LINE,
                        o.ORDER_NO,
                        o.SEQUENCE_NO,
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
                       
) "m"
ORDER BY "m"."OpStartDate"
