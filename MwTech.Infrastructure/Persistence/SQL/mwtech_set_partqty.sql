select p.ProductNumber, b.PartQty, b.OnProductionOrder
from dbo.Products as p
inner join dbo.ProductCategories as ca
on ca.id = p.ProductCategoryId
inner join dbo.ProductVersions as pv
on pv.ProductId = p.id
inner join dbo.boms as b
on b.SetVersionId = pv.id
where ca.CategoryNumber in ('MOD','MOP','MON')
and b.PartQty >= 1

update b
set OnProductionOrder = 1
from dbo.Products as p
inner join dbo.ProductCategories as ca
on ca.id = p.ProductCategoryId
inner join dbo.ProductVersions as pv
on pv.ProductId = p.id
inner join dbo.boms as b
on b.SetVersionId = pv.id
where ca.CategoryNumber in ('MOD','MOP','MON')
and b.PartQty >= 1

