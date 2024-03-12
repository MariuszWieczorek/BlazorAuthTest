select pr.ID, pr.ProductNumber, ca.CategoryNumber
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where ca.CategoryNumber = 'AKC-POZ' and pr.ProductNumber like 'AWY%'

update pr
set ProductCategoryId = (select x.Id from dbo.ProductCategories as x where x.CategoryNumber = 'DWY')
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where ca.CategoryNumber = 'AKC-POZ' and pr.ProductNumber like 'AWY%'


