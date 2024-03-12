DELETE rr
-- SELECT *
from dbo.ManufactoringRoutes as rr
inner join 
(
select ca.CategoryNumber, pr.ProductNumber, r.RouteVersionId, r.OrdinalNumber
, r.ResourceId, r.WorkCenterId , r.OperationLabourConsumption, r.OperationMachineConsumption, r.ResourceQty 
, r.ChangeOverResourceId ,r.ChangeOverLabourConsumption,r.ChangeOverMachineConsumption ,r.ChangeOverNumberOfEmployee
, COUNT(*) as ile
, MAX(r.id) as MaxId
from dbo.ManufactoringRoutes as r
inner join dbo.RouteVersions as v
on v.Id = r.RouteVersionId
inner join dbo.Products as pr
on pr.Id = v.ProductId
inner join.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where 1 = 1 
-- AND ca.CategoryNumber != 'DMA'
-- AND r.OrdinalNumber = 80
-- AND v.IsActive = 1
group by ca.CategoryNumber, pr.ProductNumber, r.RouteVersionId, r.OperationId, r.OrdinalNumber
, r.ResourceId, r.WorkCenterId , r.OperationLabourConsumption, r.OperationMachineConsumption, r.ResourceQty 
, r.ChangeOverResourceId ,r.ChangeOverLabourConsumption,r.ChangeOverMachineConsumption ,r.ChangeOverNumberOfEmployee
having COUNT(*) > 1
) as x
on rr.Id = x.MaxId
