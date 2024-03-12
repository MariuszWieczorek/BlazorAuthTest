select z.ProductNumber, z.x
from ( 
select pr.ProductNumber, v.Id, COUNT(*) as ile
,( select COUNT(*) from dbo.ProductProperties as pp where pp.ProductPropertiesVersionId = v.Id) as x
from dbo.ProductPropertyVersions as v
inner join dbo.Products as pr
on pr.Id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where v.DefaultVersion = 1
and v.IsActive = 1
and ca.CategoryNumber = 'OKC'
group by pr.ProductNumber, v.Id
) as z
where z.x = 0

/*
UPDATE v
set IsActive = 1
from dbo.ProductPropertyVersions as v
inner join dbo.Products as pr
on pr.Id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where v.DefaultVersion = 1 and v.IsActive = 0
and ca.CategoryNumber = 'OKC'
*/

