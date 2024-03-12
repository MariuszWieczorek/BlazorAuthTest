select x.pa,x.pa_matka,y.se
FROM
(
Select paca.CategoryNumber as paca,
pa.ProductNumber as pa,
pa.Idx01 as pa_matka,
seca.CategoryNumber as seca,
se.ProductNumber as se,
se.Idx01 as se_matka
from dbo.Boms as b
inner join dbo.ProductVersions as v
on v.Id = b.SetVersionId and v.ProductId = b.SetId and v.DefaultVersion = 1
inner join dbo.Products as pa
on pa.Id = b.PartId
inner join dbo.ProductCategories as paca
on paca.Id = pa.ProductCategoryId
inner join dbo.Products as se
on se.Id = b.SetId
inner join dbo.ProductCategories as seca
on seca.Id = se.ProductCategoryId
where paca.CategoryNumber in ('DKJ','DWU') and seca.CategoryNumber not in ('DKJ')
--order by paca.CategoryNumber, pa.ProductNumber
) as x
inner join 
(
Select paca.CategoryNumber as paca,
pa.ProductNumber as pa,
pa.Idx01 as pa_matka,
seca.CategoryNumber as seca,
se.ProductNumber as se,
se.Idx01 as se_matka
from dbo.Boms as b
inner join dbo.ProductVersions as v
on v.Id = b.SetVersionId and v.ProductId = b.SetId and v.DefaultVersion = 1
inner join dbo.Products as pa
on pa.Id = b.PartId
inner join dbo.ProductCategories as paca
on paca.Id = pa.ProductCategoryId
inner join dbo.Products as se
on se.Id = b.SetId
inner join dbo.ProductCategories as seca
on seca.Id = se.ProductCategoryId
where paca.CategoryNumber in ('DKJ','DWU') and seca.CategoryNumber not in ('DKJ')
-- order by paca.CategoryNumber, pa.ProductNumber
) as y
on y.pa_matka = x.pa_matka
