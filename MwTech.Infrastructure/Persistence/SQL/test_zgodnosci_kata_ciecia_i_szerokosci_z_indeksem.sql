select *
from(
select pr.ProductNumber,
(RIGHT(pr.ProductNumber,3)) as x,
CAST( (RIGHT(trim(pr.ProductNumber),3)) as numeric(10,2)) as kat,
p.Value,
p.SettingId
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.ProductSettingVersions as v
on v.ProductId = pr.Id
inner join dbo.ProductSettingVersionPositions as p
on p.ProductSettingVersionId = v.id
where ca.CategoryNumber = 'OKC' and p.SettingId = 372
) as x
where kat/10 != value

select *
from(
select pr.ProductNumber,
(RIGHT(pr.ProductNumber,3)) as x,
CAST(LEFT(RIGHT( trim(pr.ProductNumber) ,8),4) as int) as szer,
p.Value,
p.SettingId
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.ProductSettingVersions as v
on v.ProductId = pr.Id
inner join dbo.ProductSettingVersionPositions as p
on p.ProductSettingVersionId = v.id
where ca.CategoryNumber = 'OKC' and p.SettingId = 371
) as x
where szer != value