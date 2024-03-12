use mwtech;



update vv
set IsActive = 0, DefaultVersion = 0
from dbo.RouteVersions as vv
where vv.ProductId in 
(
select pr.Id
from dbo.RouteVersions as v
inner join dbo.Products as pr
on pr.Id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = PR.ProductCategoryId
where 1 = 1
and v.VersionNumber = 5
and cast(getdate() as date) = cast(v.CreatedDate as date)
)
and vv.VersionNumber != 5