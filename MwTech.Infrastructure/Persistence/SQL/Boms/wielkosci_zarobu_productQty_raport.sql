select ca.CategoryNumber, pr.ProductNumber, v.ProductQty 
from dbo.ProductVersions as v
inner join dbo.Products as pr
on pr.Id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where v.ProductQty < 1
and v.IsActive = 1
and v.DefaultVersion = 1
order by ca.CategoryNumber, pr.ProductNumber

