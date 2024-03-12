
-- WBZ02 -> WAZ01


select pr.ProductNumber, r.ChangeOverResourceId
from dbo.ManufactoringRoutes as r
inner join dbo.RouteVersions as v
on v.Id = r.RouteVersionId 
inner join dbo.Resources as z
on z.Id = r.WorkCenterId
inner join dbo.Products as pr
on pr.Id = v.ProductId
where z.ResourceNumber = 'WAZ01'
and pr.ProductNumber not like 'SBA%'


UPDATE r
set ChangeOverResourceId = 515
from dbo.ManufactoringRoutes as r
inner join dbo.RouteVersions as v
on v.Id = r.RouteVersionId 
inner join dbo.Resources as z
on z.Id = r.WorkCenterId
inner join dbo.Products as pr
on pr.Id = v.ProductId
where z.ResourceNumber = 'WAZ01'
and pr.ProductNumber not like 'SBA%'
