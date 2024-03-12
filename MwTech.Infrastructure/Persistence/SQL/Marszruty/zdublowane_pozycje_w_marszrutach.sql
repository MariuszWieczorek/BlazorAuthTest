use mwtech;

select ca.CategoryNumber, pr.ProductNumber, v.*
from dbo.RouteVersions  as v
inner join (
select RouteVersionId, OperationId, count(*) as ile
from dbo.ManufactoringRoutes as r
group by RouteVersionId, OperationId
having count(*) > 1
) as x
on x.RouteVersionId = v.Id
inner join dbo.Products as pr
on pr.Id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
order by pr.ProductNumber

