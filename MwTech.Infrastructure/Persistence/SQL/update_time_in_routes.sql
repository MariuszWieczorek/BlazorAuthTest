use MwTech;

--select *
update r
set OperationMachineConsumption = 0.125, OperationLabourConsumption = 0.125
from dbo.Products as pr
inner join dbo.RouteVersions as v
on v.ProductId = pr.id
inner join dbo.ProductCategories as cv
on cv.Id = v.ProductCategoryId
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.Id
where 1 = 1
and pr.ProductNumber = '135165161TR15' 
and cv.CategoryNumber = 'DWU' 