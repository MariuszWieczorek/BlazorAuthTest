
select se.ProductNumber, pa.ProductNumber
from dbo.Boms as b
inner join dbo.Products as se
on se.Id = b.SetId
inner join dbo.ProductCategories as ca
on ca.Id = se.ProductCategoryId
inner join dbo.Products as pa
on pa.Id = b.PartId
inner join dbo.ProductCategories as cca
on cca.Id = pa.ProductCategoryId
where ca.CategoryNumber = 'ZAW-M'
and pa.ProductNumber = 'WOK.KLEJ-KLE-19'


update b
set OnProductionOrder = 0, PartQty = 0.001
from dbo.Boms as b
inner join dbo.Products as se
on se.Id = b.SetId
inner join dbo.ProductCategories as ca
on ca.Id = se.ProductCategoryId
inner join dbo.Products as pa
on pa.Id = b.PartId
inner join dbo.ProductCategories as cca
on cca.Id = pa.ProductCategoryId
where 1=1
and ca.CategoryNumber = 'DET-KBK'
and pa.ProductNumber = 'OPK-KARTON-01'


/*
update b
set PartQty = 1
from dbo.ProductVersions as v
inner join dbo.Products as p
on p.Id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = p.ProductCategoryId
inner join dbo.Boms as b
on b.SetId = p.Id
where ca.CategoryNumber = 'MIR'
*/
/*
UPDATE v
set ProductQty = 1, ProductWeight = 0
from dbo.ProductVersions as v
inner join dbo.Products as p
on p.Id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = p.ProductCategoryId
where ca.CategoryNumber = 'MIR'


UPDATE v
set ProductQty = 1
from dbo.RouteVersions as v
inner join dbo.Products as p
on p.Id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = p.ProductCategoryId
where ca.CategoryNumber = 'MIR'
*/
