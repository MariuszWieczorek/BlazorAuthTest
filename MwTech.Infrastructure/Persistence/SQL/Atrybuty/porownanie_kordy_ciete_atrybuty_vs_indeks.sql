select pr.TechCardNumber, pr.ProductNumber, pv.Name as VersionName, ppp.SettingNumber, pp.Value
, SUBSTRING(pr.ProductNumber, CHARINDEX('-', pr.ProductNumber,5) + 1, 4)  as szer
, SUBSTRING(pr.ProductNumber, CHARINDEX('-', pr.ProductNumber,10) + 1, 4) as kat
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.id = pr.ProductCategoryId 
inner join dbo.ProductSettingVersions as pv
on pv.ProductId = pr.id and pv.DefaultVersion = 1 and pv.IsActive = 1
inner join dbo.ProductSettingVersionPositions as pp
on pp.ProductSettingVersionId = pv.id
inner join dbo.Settings as ppp
on ppp.id = pp.SettingId
where ca.CategoryNumber = 'OKC'
and pr.IsActive = 1
and pr.ProductNumber not like '%-TE'