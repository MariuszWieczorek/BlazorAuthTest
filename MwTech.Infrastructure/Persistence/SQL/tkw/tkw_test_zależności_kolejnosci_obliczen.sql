use MwTech;


select *
from dbo.Products as pr
where 1=1
-- and pr.ProductCategoryId = 2
and pr.ProductNumber like 'WOS%1'


update pr
set ProductCategoryId = 112
from dbo.Products as pr
where 1=1
and pr.ProductNumber like 'WOS%2'



select sca.CategoryNumber as SetCategory, spr.ProductNumber , sv.AlternativeNo, sv.VersionNumber, pca.CategoryNumber as ComponentCategory
, ppr.ProductNumber
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
where b.OnProductionOrder = 1
and spr.IsActive = 1
and b.PartQty > 0
-- and sca.CategoryNumber in ('NAW')
-- and pca.CategoryNumber in ('MIE-1','MIE')
and sca.OrdinalNumber <=  pca.OrdinalNumber
and pca.CategoryNumber not in ('SUR','MON','PREP','ZAW-S','ZAW-M','OPK', 'MOD','OKS','SMP','OME','OKG-ODP','BOL-KARKAS')
--group by sca.CategoryNumber, pca.CategoryNumber, spr.ProductNumber, sv.AlternativeNo , sv.VersionNumber
order by sca.CategoryNumber, pca.CategoryNumber, spr.ProductNumber , sv.AlternativeNo, sv.VersionNumber
