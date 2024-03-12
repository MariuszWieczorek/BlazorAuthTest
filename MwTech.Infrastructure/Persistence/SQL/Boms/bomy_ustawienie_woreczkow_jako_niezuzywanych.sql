/* Ustawiamy woreczki foliowe w bom jako niezu¿ywane */

use mwtech;

select 
sp.ProductNumber as SetProductNumber
,sca.CategoryNumber as SetProductCategory
,pp.ProductNumber as PartProductNumber
,pca.CategoryNumber as PartProductCategory
,b.OnProductionOrder
from dbo.Boms as b
inner join dbo.Products as sp
on sp.id = b.SetId
inner join dbo.ProductCategories as sca
on sca.id = sp.ProductCategoryId
--
inner join dbo.Products as pp
on pp.id = b.PartId
inner join dbo.ProductCategories as pca
on pca.id = pp.ProductCategoryId
--
where 1 = 1
and sp.ProductNumber like 'D%'
and pp.ProductNumber like 'Fol%'
and sca.CategoryNumber in ('DET','DET-B', 'DET-KBK', 'DET-LUZ')


update b
set OnProductionOrder = 0
from dbo.Boms as b
inner join dbo.Products as sp
on sp.id = b.SetId
inner join dbo.ProductCategories as sca
on sca.id = sp.ProductCategoryId
--
inner join dbo.Products as pp
on pp.id = b.PartId
inner join dbo.ProductCategories as pca
on pca.id = pp.ProductCategoryId
--
where 1 = 1
and sp.ProductNumber like 'D%'
and pp.ProductNumber like 'Fol%'
and sca.CategoryNumber in ('DET','DET-B', 'DET-KBK', 'DET-LUZ')