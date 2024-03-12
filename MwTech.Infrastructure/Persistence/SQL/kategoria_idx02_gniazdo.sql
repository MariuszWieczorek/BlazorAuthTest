select x.CategoryNumber,x.idx02,x.ResourceNumber,x.Waz,COUNT(*) as ile_marszrut
from (
select ca.CategoryNumber,pr.idx02
,r.WorkCenterId
,wc.ResourceNumber
,LEFT(trim(SUBSTRING(pr.ProductNumber,4,20)),LEN( trim(SUBSTRING(pr.ProductNumber,4,20)))-2) as Waz
,RIGHT(trim(SUBSTRING(pr.ProductNumber,4,20)),2) as stempel
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Resources as wc
on wc.Id = r.WorkCenterId
where pr.Idx02 is not null and pr.idx02 != ''
and (pr.Idx01 is null or pr.idx01 = '')
group by ca.CategoryNumber,pr.idx02
,r.WorkCenterId
,wc.ResourceNumber
,pr.ProductNumber
) as x
group by x.CategoryNumber,x.idx02,x.WorkCenterId,x.ResourceNumber,x.waz
order by x.CategoryNumber,x.idx02,x.ResourceNumber
