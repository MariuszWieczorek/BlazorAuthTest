select pr.id, pr.ProductNumber, ca.CategoryNumber, COUNT(we.id) as w,
(select COUNT(*) from dbo.Boms as bom where bom.SetVersionId = we.Id and bom.SetId = pr.Id) as b
from dbo.Products as pr
left join dbo.ProductVersions as we
on we.ProductId = pr.Id and we.IfsDefaultVersion = 1
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where ca.CategoryNumber in ('MIE')
group by pr.id, we.Id, pr.ProductNumber, ca.CategoryNumber
having COUNT(we.id) = 0
order by  ca.CategoryNumber, pr.ProductNumber


select pr.id, pr.ProductNumber, ca.CategoryNumber, COUNT(we.id) as w,
(select COUNT(*) from dbo.ManufactoringRoutes as r where r.RouteVersionId = we.Id) as r
from dbo.Products as pr
left join dbo.RouteVersions as we
on we.ProductId = pr.Id and we.ToIfs = 1
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where ca.CategoryNumber in ('MIE')
group by pr.id, we.id, pr.ProductNumber, ca.CategoryNumber
having COUNT(we.id) = 0
order by  ca.CategoryNumber, pr.ProductNumber

