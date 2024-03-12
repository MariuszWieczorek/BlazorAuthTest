use mwtech

select *
,iif(szer = prop_szer,1,0) as test_szer 
,iif(kat = prop_kat * 10,1,0) as test_kat 
from (
select  x.ProductNumber 
,x.PropertyNumber
,cast(x.szer as int)  as szer
,cast(x.kat as int) as kat
,iif(x.PropertyNumber = 'okc_Szerokosc', x.value,0) as prop_szer 
,iif(x.PropertyNumber = 'okc_KatCiecia', x.value,0) as prop_kat
from (
select pr.TechCardNumber, pr.ProductNumber, ppp.PropertyNumber, pp.Value
, SUBSTRING(pr.ProductNumber, CHARINDEX('-', pr.ProductNumber,5) + 1, 4)  as szer
, SUBSTRING(pr.ProductNumber, CHARINDEX('-', pr.ProductNumber,10) + 1, 4) as kat
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.id = pr.ProductCategoryId 
inner join dbo.ProductPropertyVersions as pv
on pv.ProductId = pr.id and pv.DefaultVersion = 1 and pv.IsActive = 1
inner join dbo.ProductProperties as pp
on pp.ProductPropertiesVersionId = pv.id
inner join dbo.Properties as ppp
on ppp.id = pp.PropertyId
where ca.CategoryNumber = 'OKC'
and pr.IsActive = 1
and pr.ProductNumber not like '%-TE'
) as x
) as y
-- where y.PropertyNumber = 'okc_Szerokosc'
order by y.ProductNumber