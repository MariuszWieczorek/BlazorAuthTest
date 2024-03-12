select pr.Id, pr.ProductNumber
,r.MoveTime,r.OperationId, r.Overlap
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
where ca.CategoryNumber in ('OWU') and r.OperationId = 18


update r
set r.Overlap = 1
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
where ca.CategoryNumber in ('OWU') and r.OperationId = 18
