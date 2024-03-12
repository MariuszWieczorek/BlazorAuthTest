use MwTech;

select ca.CategoryNumber ,pr.ProductNumber, pr.Idx01
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where Idx02 is not null
-- and ca.CategoryNumber like 'DE%'
and pr.IsActive = 1
and pr.Idx01 is not null
and pr.Idx01 != ''
order by ca.CategoryNumber, pr.ProductNumber

