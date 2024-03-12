select COUNT(*)
from dbo.Products as p

select x.cat, x.ProductNumber,x.ile_pv,x.ile_rv
from(
select p.ProductNumber,
coalesce(pv.id,0)  as pv, 
coalesce(rv.id,0)  as rv, 
(select COUNT(*) from dbo.Boms as pvp where pvp.SetVersionId = pv.id) as ile_pv,
(select COUNT(*) from dbo.ManufactoringRoutes as rvp where rvp.RouteVersionId = rv.id) as ile_rv,
pc.CategoryNumber as cat
from dbo.Products as p
left join dbo.ProductVersions as pv
on pv.ProductId = p.id  and pv.DefaultVersion = 1
left join dbo.RouteVersions as rv
on rv.ProductId = p.id and rv.DefaultVersion = 1
left join dbo.ProductCategories as pc
on p.ProductCategoryId = pc.Id
) as x
where (x.ile_pv = 0 or x.ile_rv = 0)
and x.cat not in ('MIE','SUR','OKS','PREP','NAW')
order by x.cat, x.ProductNumber

