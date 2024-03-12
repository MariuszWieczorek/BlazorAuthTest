-- PF_KLEJENIE

select p.ProductNumber, re.ResourceNumber 
from dbo.ManufactoringRoutes as r
inner join dbo.RouteVersions as v
on v.Id = r.RouteVersionId 
inner join dbo.Products as p
on p.Id = v.ProductId
inner join dbo.Resources as re
on re.Id = r.ResourceId
where r.OperationId = (select Id from dbo.Operations as o where o.OperationNumber = 'PF_KLEJENIE')

UPDATE r
set ResourceId = (select Id from dbo.Resources where ResourceNumber = 'PC.PW.WWS')
from dbo.ManufactoringRoutes as r
inner join dbo.RouteVersions as v
on v.Id = r.RouteVersionId 
inner join dbo.Products as p
on p.Id = v.ProductId
inner join dbo.Resources as re
on re.Id = r.ResourceId
where r.OperationId = (select Id from dbo.Operations as o where o.OperationNumber = 'PF_KLEJENIE')