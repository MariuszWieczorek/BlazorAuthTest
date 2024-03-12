select *
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where ca.CategoryNumber = 'WF-GOT'

update pr
set Idx01 = null
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where ca.CategoryNumber = 'WF-GOT'
