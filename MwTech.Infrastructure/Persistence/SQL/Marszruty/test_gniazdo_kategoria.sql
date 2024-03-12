select wc.ResourceNumber as wc, cat.ResourceNumber as category, COUNT(*) as ile
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId 
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.Id
inner join dbo.Resources as wc
on wc.Id = r.WorkCenterId
inner join dbo.Resources as cat
on cat.Id = r.ResourceId
where 1 = 1
and pr.IsActive = 1
and v.IsActive = 1

and 
(
	ca.CategoryNumber like 'MIE%' 
OR  ca.CategoryNumber like 'NAW%' 
)

group by wc.ResourceNumber,  cat.ResourceNumber
order by wc.ResourceNumber,  cat.ResourceNumber

select pr.ProductNumber, pr.Idx01, pr.Idx02,  v.VersionNumber , v.AlternativeNo ,  wc.ResourceNumber as wc, cat.ResourceNumber as category
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId 
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.Id
inner join dbo.Resources as wc
on wc.Id = r.WorkCenterId
inner join dbo.Resources as cat
on cat.Id = r.ResourceId
where 1 = 1
and pr.IsActive = 1
and v.IsActive = 1
and wc.ResourceNumber = 'AP05'
and cat.ResourceNumber = 'PC.PDKON75'