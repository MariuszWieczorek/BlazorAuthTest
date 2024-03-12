select ca.CategoryNumber, pr.ProductNumber
,(select count(*) from dbo.RouteVersions as vx where  vx.IsActive = 1  and vx.DefaultVersion = 1 and vx.ProductId = pr.Id) as ile
from dbo.RouteVersions as v
inner join dbo.Products as pr
on pr.Id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where v.IsActive = 1 and v.AlternativeNo = 1 and v.DefaultVersion = 0
-- and ca.CategoryNumber in ('DWU','DWY')
order by ca.CategoryNumber, pr.ProductNumber

select x.CategoryNumber, x.ProductNumber, x.ile
from(
select ca.CategoryNumber, pr.ProductNumber
,(select count(*) from dbo.RouteVersions as vx where  vx.IsActive = 1  and vx.DefaultVersion = 1 and vx.ProductId = pr.Id) as ile
from dbo.RouteVersions as v
inner join dbo.Products as pr
on pr.Id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where v.IsActive = 1
) as x
where x.ile = 0
order by x.CategoryNumber, x.ProductNumber


/*
update v
set DefaultVersion = 1
from dbo.RouteVersions as v
inner join dbo.Products as pr
on pr.Id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where v.IsActive = 1 and v.AlternativeNo = 1 and v.DefaultVersion = 0
and ca.CategoryNumber in ('DWU','DWY')
*/