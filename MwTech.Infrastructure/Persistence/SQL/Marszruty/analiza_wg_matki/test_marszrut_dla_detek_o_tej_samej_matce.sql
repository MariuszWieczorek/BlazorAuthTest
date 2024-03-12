-- Analiza marszrut dêtek dla tej samej matki 
select x.CategoryNumber,x.Idx01,x.OperationId ,COUNT(*)
from (
select ca.CategoryNumber,pr.Idx01
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
where pr.Idx01 is not null and pr.Idx01 != ''
group by ca.CategoryNumber,pr.Idx01
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
group by x.CategoryNumber,x.Idx01,x.OperationId
having count(*) > 1


select ca.CategoryNumber,pr.Idx01,pr.ProductNumber
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
where pr.Idx01 is not null and pr.Idx01 != ''
and pr.idx01 = '100020V' and ca.CategoryNumber = 'DKJ'
group by ca.CategoryNumber,pr.Idx01,pr.ProductNumber
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
--,r.ChangeOverNumberOfEmployee
,r.ChangeOverResourceId
--,r.MoveTime
--,r.Overlap
,r.ProductCategoryId
order by ca.CategoryNumber,pr.Idx01
--, v.AlternativeNo, v.VersionNumber,r.OrdinalNumber


select ca.CategoryNumber,pr.Idx01,pr.ProductNumber
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
where pr.Idx01 is not null and pr.Idx01 != ''
and pr.idx01 = '17518514TR13FI32TUB' and ca.CategoryNumber = 'DWU'
group by ca.CategoryNumber,pr.Idx01,pr.ProductNumber
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
order by ca.CategoryNumber,pr.Idx01
, v.AlternativeNo, v.VersionNumber
--,r.OrdinalNumber


-- zmiany w rekordzie marszruty


/*
update r
set OperationLabourConsumption = 0.079440,
OperationMachineConsumption = 0.079440
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
where pr.Idx01 is not null and pr.Idx01 != ''
and pr.idx01 = '17518514TR13FI32TUB' and ca.CategoryNumber = 'DWU'

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
-- where pr.Idx01 is not null and pr.Idx01 != ''
-- and pr.idx01 = '17518514TR13FI32TUB' and ca.CategoryNumber = 'DWU'
*/

/*
update r
SET
ChangeOverNumberOfEmployee = 0
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
where pr.Idx01 is not null and pr.Idx01 != ''
and ca.CategoryNumber = 'DET-LUZ'
*/

/*
update r
SET
MoveTime = 0
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
where pr.Idx01 is not null and pr.Idx01 != ''
and ca.CategoryNumber  = 'DET-KBK'
*/
-- 'DWY', 'DST', 'AWY', 'AST'

