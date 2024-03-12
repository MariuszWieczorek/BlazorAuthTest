use MwTech;

select sca.CategoryNumber as SetCategory, spr.ProductNumber , sv.AlternativeNo, sv.VersionNumber, pca.CategoryNumber as ComponentCategory
, ppr.ProductNumber
,b.PartQty
from dbo.Boms as b
inner join dbo.Products as ppr
on ppr.Id = b.PartId
inner join dbo.ProductCategories as pca
on pca.Id = ppr.ProductCategoryId 
inner join dbo.Products as spr
on spr.Id = b.SetId
inner join dbo.ProductCategories as sca
on sca.Id = spr.ProductCategoryId 
inner join dbo.ProductVersions as sv
on sv.Id = b.SetVersionId
where b.OnProductionOrder = 0
and spr.IsActive = 1
and b.PartQty < 0 and b.PartQty > -0.009