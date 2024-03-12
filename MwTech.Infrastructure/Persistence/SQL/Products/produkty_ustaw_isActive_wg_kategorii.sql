select p.ProductNumber,  p.IsActive
from dbo.Products as p
inner join dbo.ProductCategories as ca
on ca.id = p.ProductCategoryId
inner join dbo.ProductVersions as pv
on pv.ProductId = p.id
where ca.CategoryNumber in ('MOD','MOP','MON')


update p
set IsActive = 1
from dbo.Products as p
inner join dbo.ProductCategories as ca
on ca.id = p.ProductCategoryId
inner join dbo.ProductVersions as pv
on pv.ProductId = p.id
where ca.CategoryNumber in ('MOD','MOP','MON')
