update v
set Rev = Rev + 1
from dbo.Products as pr
inner join dbo.ProductSettingVersions as v
on v.ProductId = pr.Id
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where ca.CategoryNumber = 'OKC'