use mwtech;

delete m
--
from MwTech.dbo.Products as p
inner join MwTech.dbo.ProductCategories as pc
on pc.Id = p.ProductCategoryId
--
inner join MwTech.dbo.RouteVersions as rw
on rw.ProductId = p.Id
inner join MwTech.dbo.ManufactoringRoutes as m
on m.RouteVersionId = rw.Id
inner join MwTech.dbo.ProductCategories as ca
on ca.Id = rw.ProductCategoryId
--
where 1 = 1
and pc.CategoryNumber IN ('DWA','DMA')
and ca.CategoryNumber in ('DAP','DST')
