select sca.CategoryNumber,
--spr.ProductNumber,
pca.CategoryNumber,
ppr.ProductNumber 

from dbo.Boms as b
inner join dbo.Products as ppr
on ppr.Id = b.PartId
inner join dbo.ProductCategories as pca
on pca.Id = ppr.ProductCategoryId 
inner join dbo.Products as spr
on spr.Id = b.SetId
inner join dbo.ProductCategories as sca
on sca.Id = spr.ProductCategoryId 
where b.OnProductionOrder = 0 and b.PartQty > 0
and pca.CategoryNumber not in ('OME','OPK','PREP','SMP')
group by sca.CategoryNumber, pca.CategoryNumber, ppr.ProductNumber --, spr.ProductNumber 
order by sca.CategoryNumber, pca.CategoryNumber, ppr.ProductNumber 


select sca.CategoryNumber, pca.CategoryNumber, ppr.ProductNumber 
-- spr.ProductNumber
from dbo.Boms as b
inner join dbo.Products as ppr
on ppr.Id = b.PartId
inner join dbo.ProductCategories as pca
on pca.Id = ppr.ProductCategoryId 
inner join dbo.Products as spr
on spr.Id = b.SetId
inner join dbo.ProductCategories as sca
on sca.Id = spr.ProductCategoryId 
where b.OnProductionOrder = 1
and spr.IsActive = 1
and b.PartQty > 0
and pca.CategoryNumber not in ('MIE','MIP','WW-PPROF')
and sca.CategoryNumber in ('WW-PROF','WW-PPROF')
group by sca.CategoryNumber, pca.CategoryNumber, ppr.ProductNumber 
order by sca.CategoryNumber, pca.CategoryNumber, ppr.ProductNumber 


