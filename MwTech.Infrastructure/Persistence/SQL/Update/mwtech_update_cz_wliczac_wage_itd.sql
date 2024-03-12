UPDATE b
set DoNotIncludeInTkw = 0
,DoNotIncludeInWeight = iif(OnProductionOrder=0,1,0)
,DoNotExportToIfs = 0
from dbo.Boms as b

select *
from dbo.Boms as b
inner join dbo.Products as prs
on prs.Id = b.SetId
inner join dbo.ProductCategories as cas
on cas.Id = prs.ProductCategoryId
inner join dbo.Products as prp
on prp.Id = b.PartId
inner join dbo.ProductCategories as cap
on cap.Id = prp.ProductCategoryId
where cas.CategoryNumber = 'DOB' and cap.CategoryNumber = 'DWY' and PartQty > 0

UPDATE b
set DoNotIncludeInTkw = 1, DoNotIncludeInWeight = 0, DoNotExportToIfs = 1
from dbo.Boms as b
inner join dbo.Products as prs
on prs.Id = b.SetId
inner join dbo.ProductCategories as cas
on cas.Id = prs.ProductCategoryId
inner join dbo.Products as prp
on prp.Id = b.PartId
inner join dbo.ProductCategories as cap
on cap.Id = prp.ProductCategoryId
where cas.CategoryNumber = 'DOB' and cap.CategoryNumber = 'DWY' and PartQty < 0
