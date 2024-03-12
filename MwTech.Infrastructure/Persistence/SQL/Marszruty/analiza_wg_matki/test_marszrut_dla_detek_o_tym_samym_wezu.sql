-- Analiza marszrut dêtek dla tej samej matki 
select x.CategoryNumber,x.idx02,x.OperationId ,COUNT(*)
from (
select ca.CategoryNumber,pr.idx02
-- ,v.AlternativeNo
-- ,v.VersionNumber
,r.OperationId
,r.ResourceId
,r.OrdinalNumber
,r.ResourceQty
-- ,r.WorkCenterId
,round(r.OperationLabourConsumption,5) as OperationLabourConsumption
,round(r.OperationMachineConsumption,5) as OperationMachineConsumption
,round(r.ChangeOverLabourConsumption,5) as ChangeOverLabourConsumption
,round(r.ChangeOverMachineConsumption,5) as ChangeOverMachineConsumption
,r.ChangeOverNumberOfEmployee
,r.ChangeOverResourceId
,r.MoveTime
,r.Overlap
,r.ProductCategoryId
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
where pr.Idx02 is not null and pr.idx02 != ''
and (pr.Idx01 is null or pr.idx01 = '')
group by ca.CategoryNumber,pr.idx02
--,v.AlternativeNo
--,v.VersionNumber
,r.OperationId
,r.ResourceId
,r.OrdinalNumber
,r.ResourceQty
-- ,r.WorkCenterId
,round(r.OperationLabourConsumption,5)
,round(r.OperationMachineConsumption,5)
,round(r.ChangeOverLabourConsumption,5)
,round(r.ChangeOverMachineConsumption,5)
,r.ChangeOverNumberOfEmployee
,r.ChangeOverResourceId
,r.MoveTime
,r.Overlap
,r.ProductCategoryId
) as x
group by x.CategoryNumber,x.idx02,x.OperationId
having count(*) > 1


select ca.CategoryNumber,pr.idx02,pr.ProductNumber
-- , v.AlternativeNo, v.VersionNumber
,r.OperationId
,r.ResourceId
,r.OrdinalNumber
,r.ResourceQty
-- ,r.WorkCenterId
,round(r.OperationLabourConsumption,5) as OperationLabourConsumption
,round(r.OperationMachineConsumption,5) as OperationMachineConsumption
,round(r.ChangeOverLabourConsumption,5) as ChangeOverLabourConsumption
,round(r.ChangeOverMachineConsumption,5) as ChangeOverMachineConsumption
,r.ChangeOverNumberOfEmployee
,r.ChangeOverResourceId
-- ,r.MoveTime
-- ,r.Overlap
,r.ProductCategoryId
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
where pr.idx02 is not null and pr.idx02 != ''
and pr.idx02 = '0267N' and ca.CategoryNumber = 'DWY'
group by ca.CategoryNumber,pr.idx02,pr.ProductNumber
-- , v.AlternativeNo, v.VersionNumber
,r.OperationId
,r.ResourceId
,r.OrdinalNumber
,r.ResourceQty
-- ,r.WorkCenterId
,round(r.OperationLabourConsumption,5)
,round(r.OperationMachineConsumption,5)
,round(r.ChangeOverLabourConsumption,5)
,round(r.ChangeOverMachineConsumption,5)
,r.ChangeOverNumberOfEmployee
,r.ChangeOverResourceId
--,r.MoveTime
--,r.Overlap
,r.ProductCategoryId
order by ca.CategoryNumber,pr.idx02
--, v.AlternativeNo, v.VersionNumber,r.OrdinalNumber


select ca.CategoryNumber,pr.idx02,pr.ProductNumber
, v.AlternativeNo, v.VersionNumber
,r.OperationId
,r.ResourceId
,r.OrdinalNumber
,r.ResourceQty
-- ,r.WorkCenterId
,round(r.OperationLabourConsumption,5) as OperationLabourConsumption
,round(r.OperationMachineConsumption,5) as OperationMachineConsumption
,round(r.ChangeOverLabourConsumption,5) as ChangeOverLabourConsumption
,round(r.ChangeOverMachineConsumption,5) as ChangeOverMachineConsumption
--,r.ChangeOverNumberOfEmployee
,r.ChangeOverResourceId
-- ,r.MoveTime
-- ,r.Overlap
,r.ProductCategoryId
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
where pr.idx02 is not null and pr.idx02 != ''
and pr.idx02 = '0032' and ca.CategoryNumber = 'DWY'
and pr.ProductNumber = 'DWY1607020KB'
group by ca.CategoryNumber,pr.idx02,pr.ProductNumber
 , v.AlternativeNo, v.VersionNumber
,r.OperationId
,r.ResourceId
,r.OrdinalNumber
,r.ResourceQty
-- ,r.WorkCenterId
,round(r.OperationLabourConsumption,5)
,round(r.OperationMachineConsumption,5)
,round(r.ChangeOverLabourConsumption,5)
,round(r.ChangeOverMachineConsumption,5)
--,r.ChangeOverNumberOfEmployee
,r.ChangeOverResourceId
--,r.MoveTime
--,r.Overlap
,r.ProductCategoryId
order by ca.CategoryNumber,pr.idx02
, v.AlternativeNo, v.VersionNumber
--,r.OrdinalNumber


-- zmiany w rekordzie marszruty


/*
update r
set OperationLabourConsumption = 0.001730,
OperationMachineConsumption = 0.001730
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
where pr.idx02 is not null and pr.idx02 != ''
and pr.idx02 = '0267N' and ca.CategoryNumber = 'DWY'

*/



/*
update r
SET
 OperationLabourConsumption = round(r.OperationLabourConsumption,5)
,OperationMachineConsumption = round(r.OperationMachineConsumption,5)
,ChangeOverLabourConsumption = round(r.ChangeOverLabourConsumption,5)
,ChangeOverMachineConsumption = round(r.ChangeOverMachineConsumption,5)
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
-- where pr.idx02 is not null and pr.idx02 != ''
-- and pr.idx02 = '17518514TR13FI32TUB' and ca.CategoryNumber = 'DWU'
*/

/*
update r
SET
Overlap = 1
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
where pr.idx02 is not null and pr.idx02 != ''
-- and pr.idx02 = '17518514TR13FI32TUB' and ca.CategoryNumber = 'DWU'
*/

/*
update r
SET
Overlap = 1
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
where pr.idx02 is not null and pr.idx02 != ''
and ca.CategoryNumber  = 'WW-PROF'
*/
-- 'DWY', 'DST', 'AWY', 'AST'

