CREATE OR ALTER VIEW mwtech_scada_boms_positions
AS
(
select b.SetVersionId as versionId
,sca.CategoryNumber as SetCategoryNumber
,p.ProductNumber as SetProductNumber
,p.Name as SetProductName
,pca.CategoryNumber as PartCategoryNumber
,pp.ProductNumber as PartProductNumber
,b.PartQty
from dbo.Boms as b
inner join dbo.ProductVersions as v
on v.Id = b.SetVersionId
inner join dbo.Products as p
on p.Id = v.ProductId and v.DefaultVersion = 1
inner join dbo.Products as pp
on pp.Id = b.PartId
inner join dbo.ProductCategories as pca
on pca.Id = pp.ProductCategoryId
inner join dbo.ProductCategories as sca
on sca.Id = p.ProductCategoryId
)
