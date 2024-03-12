select pr.id, pr.ProductNumber, ca.CategoryNumber, COUNT(we.id) as w
from dbo.Products as pr
left join dbo.ProductVersions as we
on we.ProductId = pr.Id and we.DefaultVersion = 1
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where ca.CategoryNumber in ('MIE','NAW','MIP','MIF')
group by pr.id, pr.ProductNumber, ca.CategoryNumber
having COUNT(we.id) = 0
order by  ca.CategoryNumber, pr.ProductNumber


select pr.id, pr.ProductNumber, ca.CategoryNumber, COUNT(we.id) as w
from dbo.Products as pr
left join dbo.RouteVersions as we
on we.ProductId = pr.Id and we.DefaultVersion = 1
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where ca.CategoryNumber in ('MIE','NAW','MIP','MIF')
group by pr.id, pr.ProductNumber, ca.CategoryNumber
having COUNT(we.id) = 0
order by  ca.CategoryNumber, pr.ProductNumber

