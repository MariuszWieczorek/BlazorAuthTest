use mwtech
declare @part as varchar(20)
set @part = '%OS70-1%'


select X.REVISION, X.ALTERNATIVE, x.ALTERNATIVE_DESCRIPTION, x.PART_NO, x.SetName, x.LINE_ITEM_NO, x.COMPONENT_PART, x.ComponentName, x.QTY_PER_ASSEMBLY, x.CONSUMPTION_ITEM
from dbo.mwtech_bom_ifs as x
where  IsDefaultVersion = 1
AND x.PART_NO like @part
order by x.PART_NO, x.LINE_ITEM_NO

SELECT y.ROUTING_REVISION, y.ALTERNATIVE_NO, y.ALTERNATIVE_DESCRIPTION, y.PART_NO, y.ALTERNATIVE_DESCRIPTION, y.WORK_CENTER_NO, y.LABOR_CLASS_NO, y.LABOR_RUN_FACTOR, y.CREW_SIZE
, y.Weight
, CAST ( ROUND((y.Weight * 3600)/y.LABOR_RUN_FACTOR,0) as DECIMAL(10,0)) as Czas
FROM (
select  x.ROUTING_REVISION, x.ALTERNATIVE_NO,  x.PART_NO, x.ALTERNATIVE_DESCRIPTION, x.WORK_CENTER_NO, x.LABOR_CLASS_NO, x.LABOR_RUN_FACTOR, x.CREW_SIZE
,dbo.getProductWeight(x.ProductId) as Weight
from dbo.mwtech_route_ifs as x
where IsDefaultVersion = 1
AND PART_NO like @part
) as y
order by y.PART_NO
