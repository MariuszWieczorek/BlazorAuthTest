select ProductNumber, VersionId, ProductQty,waga, ProductQty - waga as roznica
from (
select v.Id as VersionId, pr.ProductNumber, v.ProductQty, dbo.getProductWeight(pr.Id) as waga
from dbo.Products as pr
inner join dbo.ProductVersions as v
on v.ProductId = pr.Id
where v.ProductQty != 1
and v.DefaultVersion = 1
) as x
where x.waga != x.ProductQty
order by x.ProductNumber
-- and abs(x.waga - x.ProductQty) > 0.05


/*
--select * 
update vv
set ProductQty = x.waga
from dbo.ProductVersions as vv
inner join
(
select ProductNumber, VersionId, ProductQty,waga, ProductQty - waga as roznica
from (
select v.Id as VersionId, pr.ProductNumber, v.ProductQty, dbo.getProductWeight(pr.Id) as waga
from dbo.Products as pr
inner join dbo.ProductVersions as v
on v.ProductId = pr.Id
where v.ProductQty != 1
and v.DefaultVersion = 1
) as x
where x.waga != x.ProductQty
) as x
on x.VersionId = vv.Id

*/
