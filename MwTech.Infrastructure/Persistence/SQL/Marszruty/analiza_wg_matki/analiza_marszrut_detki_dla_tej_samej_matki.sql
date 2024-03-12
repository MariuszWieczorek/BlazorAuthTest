-- Analiza marszrut dêtek dla tej samej matki 
select x.CategoryNumber,x.Idx01, x.AlternativeNo, x.VersionNumber, COUNT(*)
from (
select ca.CategoryNumber,pr.Idx01, v.AlternativeNo, v.VersionNumber
,r.OperationId
,r.ResourceId
,r.OrdinalNumber
,r.ResourceQty
,r.WorkCenterId
,r.OperationLabourConsumption
,r.OperationMachineConsumption
,r.ChangeOverLabourConsumption
,r.ChangeOverMachineConsumption
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
where pr.Idx01 is not null and pr.Idx01 != ''
group by ca.CategoryNumber,pr.Idx01, v.AlternativeNo, v.VersionNumber
,r.OperationId
,r.ResourceId
,r.OrdinalNumber
,r.ResourceQty
,r.WorkCenterId
,r.OperationLabourConsumption
,r.OperationMachineConsumption
,r.ChangeOverLabourConsumption
,r.ChangeOverMachineConsumption
,r.ChangeOverNumberOfEmployee
,r.ChangeOverResourceId
--,r.MoveTime
-- ,r.Overlap
,r.ProductCategoryId
) as x
group by x.CategoryNumber,x.Idx01, x.AlternativeNo, x.VersionNumber
having count(*) > 1



select ca.CategoryNumber,pr.Idx01,pr.ProductNumber, v.AlternativeNo, v.VersionNumber
,r.OperationId
,r.ResourceId
,r.OrdinalNumber
,r.ResourceQty
,r.WorkCenterId
,r.OperationLabourConsumption
,r.OperationMachineConsumption
,r.ChangeOverLabourConsumption
,r.ChangeOverMachineConsumption
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
where pr.Idx01 is not null and pr.Idx01 != ''
and pr.idx01 = '15512TR15offset' and ca.CategoryNumber = 'DAP' and AlternativeNo = 4
group by ca.CategoryNumber,pr.Idx01,pr.ProductNumber, v.AlternativeNo, v.VersionNumber
,r.OperationId
,r.ResourceId
,r.OrdinalNumber
,r.ResourceQty
,r.WorkCenterId
,r.OperationLabourConsumption
,r.OperationMachineConsumption
,r.ChangeOverLabourConsumption
,r.ChangeOverMachineConsumption
,r.ChangeOverNumberOfEmployee
,r.ChangeOverResourceId
--,r.MoveTime
--,r.Overlap
,r.ProductCategoryId
order by ca.CategoryNumber,pr.Idx01, v.AlternativeNo, v.VersionNumber,r.OrdinalNumber

