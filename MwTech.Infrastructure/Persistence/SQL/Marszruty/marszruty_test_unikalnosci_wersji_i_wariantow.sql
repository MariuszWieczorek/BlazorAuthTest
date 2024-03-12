select v.ProductId, pr.ProductNumber, v.ProductCategoryId, ca.CategoryNumber, v.VersionNumber, v.AlternativeNo, COUNT(*)
from dbo.RouteVersions as v
inner join dbo.Products as pr
on pr.Id = v.ProductId
left join dbo.ProductCategories as ca
on ca.Id = v.ProductCategoryId
group by v.ProductId, v.ProductCategoryId, v.VersionNumber, v.AlternativeNo, pr.ProductNumber, ca.CategoryNumber
having COUNT(*) > 1
order by pr.ProductNumber

