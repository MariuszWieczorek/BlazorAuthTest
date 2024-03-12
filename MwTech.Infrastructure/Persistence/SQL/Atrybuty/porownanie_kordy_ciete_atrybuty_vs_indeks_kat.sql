use mwtech

select z.*
--update rrr
--set value = z.kat * 0.1
from dbo.ProductSettingVersionPositions as rrr
inner join 
(
select VersionId, PositonId, ProductId, ProductNumber, TechCardNumber, VersionName, SettingNumber
, szer, setting_szer ,iif(szer = setting_szer,1,0) as test_szer 
, kat,  setting_kat ,iif(kat = setting_kat * 10,1,0) as test_kat 
from (
select  x.ProductNumber , x.TechCardNumber, x.ProductId , x.PositonId, x.VersionId
,x.VersionName
,x.SettingNumber
,cast(x.szer as int)  as szer
,cast(x.kat as int) as kat
,iif(x.SettingNumber = 'okc_Szerokosc', x.value,0) as setting_szer 
,iif(x.SettingNumber = 'okc_KatCiecia', x.value,0) as setting_kat
from (
select pr.TechCardNumber, pr.Id as ProductId, pr.ProductNumber, Cast(pv.Name as varchar(50)) as VersionName, ppp.SettingNumber, pp.Value
, pp.id as PositonId, pv.id as VersionId
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
) as x
) as y
where 1 = 1
and y.SettingNumber = 'okc_KatCiecia' and y.kat*0.1 != y.setting_kat
) as z
on rrr.id = z.PositonId
--order by z.ProductNumber