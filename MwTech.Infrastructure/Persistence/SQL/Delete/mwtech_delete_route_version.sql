select *
from dbo.RouteVersions as rw
inner join dbo.Products as pr
on pr.Id = rw.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = rw.id
where ca.CategoryNumber  = 'DAP' 
and rw.Name = 'DMAL1'


delete dbo.RouteVersions
from dbo.RouteVersions as rw
inner join dbo.Products as pr
on pr.Id = rw.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where ca.CategoryNumber  = 'DAP' 
and rw.Name = 'DMAL1'

