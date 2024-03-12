select x.ProductNumber, x.VersionNumber, x.AlternativeNo
from dbo.RouteVersions as rr
inner join 
(
select cp.CategoryNumber as ProductCategory, cr.CategoryNumber, p.ProductNumber, r.ProductId, r.VersionNumber, R.AlternativeNo
,count(*) as ile
from dbo.RouteVersions as r
inner join dbo.Products as p
on p.Id = r.ProductId
left join dbo.ProductCategories as cr
on cr.id = r.ProductCategoryId
--
left join dbo.ProductCategories as cp
on cp.id = p.ProductCategoryId
--
where 1 = 1
-- AND r.IsActive = 1
group by r.ProductId, r.VersionNumber, R.AlternativeNo, p.ProductNumber , cr.CategoryNumber, cp.CategoryNumber
having count(*) > 1
-- order by cp.CategoryNumber, p.ProductNumber
) as x
on rr.ProductId = x.ProductId
and rr.VersionNumber = x.VersionNumber
and rr.AlternativeNo = x.AlternativeNo
and rr.IsActive = 1
order by x.ProductNumber




