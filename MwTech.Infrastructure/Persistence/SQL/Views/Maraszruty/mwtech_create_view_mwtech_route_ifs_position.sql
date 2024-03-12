/* Export operacji marszrut do IFS */
CREATE OR ALTER VIEW mwtech_route_ifs
AS
(
  SELECT wc.Contract as CONTRACT
, pc.CategoryNumber as CategoryNumber
, REPLACE(p.ProductNumber,'_','-') as PART_NO
, iif(rw.DefaultVersion = 1, '*', cast(rw.alternativeNo as varchar(5)) ) as ALTERNATIVE_NO
, rw.name  as ALTERNATIVE_DESCRIPTION
-- MwTech Plik Importu
, rw.versionNumber as VersionNo
, rw.alternativeNo as AlternativeNo
, iif(rw.IsActive=1,1,0) as IsActive
, iif(rw.DefaultVersion=1,1,0) as IsDefaultVersion
, rpc.CategoryNumber as RouteCategory
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
-- narzêdzia
, t.ToolNumber as TOOL_ID
, m.ToolQuantity as TOOL_QUANTITY
-- dodatkowe pola
, pc.Name as ProductCategoryName
, CAST(o.Name as varchar(35)) as OperationName
, p.Client as Client
, p.Idx01
, p.Idx02
, null as Idx
, m.id as RoutePositionId
, rw.ProductId
, rw.VersionNumber as ROUTING_REVISION
, GETDATE() as ExportDate
-- , CAST(ROW_NUMBER() over(order by pc.Name, p.ProductNumber, rw.versionNumber,rw.alternativeNo, m.OrdinalNumber  ) as numeric(10) )as rowno
--
from MwTech.dbo.Products as p
inner join MwTech.dbo.RouteVersions as rw
on rw.ProductId = p.Id 
inner join MwTech.dbo.ProductCategories as pc
on pc.Id = p.ProductCategoryId
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
left join MwTech.dbo.ProductCategories as rpc
on rpc.Id = rw.ProductCategoryId
left join dbo.RoutingTools as t
on t.Id = m.RoutingToolId
where pc.RouteSource = 0 -- 0 - produkt, 1 - idx01, 2 - idx02 
and p.IsActive = 1
and rw.IsActive = 1
-- and p.IsTest = 0
)

