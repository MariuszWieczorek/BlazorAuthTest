/* Export operacji marszrut do IFS */
CREATE OR ALTER VIEW mwtech_route_ifs_idx02
AS
(
select wc.Contract as CONTRACT
,REPLACE(p.ProductNumber,'_','-') as PART_NO
,iif(rw.DefaultVersion = 1, '*', cast(rw.alternativeNo as varchar(5)) ) as ALTERNATIVE_NO
,rw.name  as ALTERNATIVE_DESCRIPTION
-- operacja
, m.OrdinalNumber as OPERATION_NO
, CAST(o.OperationNumber as varchar(35)) as OPERATION_DESCRIPTION
-- gniazdo
, wc.ResourceNumber as WORK_CENTER_NO
-- SETUP / przezbrojenie
, m.ChangeOverMachineConsumption as MACH_SETUP_TIME
, m.ChangeOverLabourConsumption as LABOR_SETUP_TIME
, ch.ResourceNumber as SETUP_LABOR_CLASS_NO
, m.ChangeOverNumberOfEmployee as SETUP_CREW_SIZE
-- RUN / wykonanie
, m.OperationMachineConsumption as MACH_RUN_FACTOR
, m.OperationLabourConsumption as LABOR_RUN_FACTOR
, u.UnitCode as RUN_TIME_CODE
, r.ResourceNumber as LABOR_CLASS_NO
, m.ResourceQty as CREW_SIZE
-- inne
, m.MoveTime as MOVE_TIME
, m.Overlap as OVERLAP
, CAST('Jednostki' as varchar(10)) as OVERLAP_UNIT
, CAST('Nierównoleg³a' as varchar(15)) as PARALLEL_OPERATION
, CAST('Produkcja' as varchar(15)) as BOM_TYPE
, CAST('M' as varchar(1)) as BOM_TYPE_DB
-- narzêdzia
, t.ToolNumber as TOOL_ID
, m.ToolQuantity as TOOL_QUANTITY
-- dodatkowe pola
, pc.CategoryNumber
, pc.Name as CategoryName
, p.idx02
, p.idx02 as idx
, rw.alternativeNo as AlternativeNo
, rw.versionNumber as VersionNo
, rw.name  as AlternativeName
, p.client as Client
, CAST(o.Name as varchar(35)) as OperationName
, rw.IsActive
, rw.DefaultVersion
, rw.Id as RouteVersionId
, m.Id  as RoutePositionId
, rw.ProductId
, rw.VersionNumber as ROUTING_REVISION
, GETDATE() as ExportDate
--, CAST(ROW_NUMBER() over(order by pc.Name, p.ProductNumber, rw.versionNumber,rw.alternativeNo, m.OrdinalNumber  ) as numeric(10) )as rowno
--
from MwTech.dbo.Products as p
inner join MwTech.dbo.ProductCategories as pc
on pc.Id = p.ProductCategoryId
--
inner join dbo.Products as idx
on idx.ProductNumber = p.idx02
--
inner join MwTech.dbo.RouteVersions as rw
on rw.ProductId = idx.Id and rw.ProductCategoryId = p.ProductCategoryId
inner join MwTech.dbo.ManufactoringRoutes as m
on m.RouteVersionId = rw.Id
inner join MwTech.dbo.Operations as o
on o.Id = m.OperationId
inner join MwTech.dbo.Resources as r
on r.Id = m.ResourceId
inner join MwTech.dbo.Resources as wc
on wc.Id = m.WorkCenterId
left join MwTech.dbo.Resources as ch
on ch.Id = m.ChangeOverResourceId
inner join MwTech.dbo.Units as u
on u.Id = o.UnitId
left join dbo.RoutingTools as t
on t.Id = m.RoutingToolId
where 1 = 1
and pc.RouteSource = 2
and p.IsActive = 1
-- and p.IsTest = 0
and rw.IsActive = 1
and p.idx02 is not null and p.idx02 != ''
)



