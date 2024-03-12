
-- BOM - Produkt nie jest mieszankπ a nie ma wskaünika do IFS

select  ca.CategoryNumber, v.ProductId, p.ProductNumber,v.VersionNumber,COUNT(*) as ile
from dbo.RouteVersions as v
inner join dbo.Products as p
on p.Id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = p.ProductCategoryId
where 1 = 1
and v.ToIfs = 0
-- and v.VersionNumber = 1
-- and v.DefaultVersion = 0
and ca.CategoryNumber not in ('MIE')
group by ca.CategoryNumber, v.ProductId, p.ProductNumber,v.VersionNumber
order by ca.CategoryNumber, p.ProductNumber,v.VersionNumber


-- Marszruta - Produkt nie jest mieszankπ a nie ma wskaünika do IFS

select ca.CategoryNumber, v.ProductId, p.ProductNumber,v.VersionNumber,COUNT(*) as ile
from dbo.ProductVersions as v
inner join dbo.Products as p
on p.Id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = p.ProductCategoryId
where 1 = 1
and v.ToIfs = 0
-- and v.VersionNumber = 1
-- and v.DefaultVersion = 0
and ca.CategoryNumber not in ('MIE')
group by ca.CategoryNumber,v.ProductId, p.ProductNumber,v.VersionNumber
order by ca.CategoryNumber, p.ProductNumber,v.VersionNumber