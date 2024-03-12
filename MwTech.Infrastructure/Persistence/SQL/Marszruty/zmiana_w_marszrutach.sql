use mwtech;
/*
update r
set ResourceQty = 0.02
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.Id
inner join dbo.Operations as o
on o.id = r.OperationId
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where 1=1
-- and ca.CategoryNumber = 'OWU'
and pr.ProductNumber like 'SK3%'
and o.OperationNumber = 'BOL.SOL.SUSZ.OPON'
and v.DefaultVersion = 1
*/

select ca.CategoryNumber, pr.ProductNumber, o.OperationNumber, o.Name, r.ResourceQty, r.OrdinalNumber 
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.Id
inner join dbo.Operations as o
on o.id = r.OperationId
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where 1=1
-- and ca.CategoryNumber = 'OWU'
and pr.ProductNumber like 'SK3%'
and o.OperationNumber = 'BOL.SOL.SUSZ.OPON'
and v.DefaultVersion = 1
order by pr.ProductNumber

