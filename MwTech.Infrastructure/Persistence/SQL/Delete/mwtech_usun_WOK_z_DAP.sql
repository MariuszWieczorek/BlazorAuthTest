select pr.Id, pr.ProductNumber
,b.PartId, pa.ProductNumber
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.Boms as b
on b.SetId = pr.id
inner join dbo.Products as pa
on pa.Id = b.PartId
where ca.CategoryNumber in ('AKC-POZ') 
and substring(pr.productNumber,1,3)='DAP'
and substring(pa.productNumber,1,3)='WOK'



delete b
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.Boms as b
on b.SetId = pr.id
inner join dbo.Products as pa
on pa.Id = b.PartId
where ca.CategoryNumber in ('AKC-POZ') 
and substring(pr.productNumber,1,3)='DAP'
and substring(pa.productNumber,1,3)='WOK'
