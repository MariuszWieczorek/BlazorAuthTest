select pr.ProductNumber, pr1.ProductNumber
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.id = pr.ProductCategoryId
inner join dbo.Boms as b
on b.PartId = pr.Id
inner join dbo.Products as pr1
on pr1.id = b.SetId
where ca.CategoryNumber like '%ZAW%'
and b.OnProductionOrder = 0



select pr1.ProductNumber,pr.ProductNumber,  b.PartQty
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.id = pr.ProductCategoryId
inner join dbo.Boms as b
on b.PartId = pr.Id
inner join dbo.Products as pr1
on pr1.id = b.SetId
where ca.CategoryNumber like '%ZAW%'
and b.PartQty != 1

/*
inner join dbo.ProductVersions as v
on v.ProductId = pr.Id
inner join dbo.Boms as b
on b.SetVersionId = v.id
*/