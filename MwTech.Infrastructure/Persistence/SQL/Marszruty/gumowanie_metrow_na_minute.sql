use mwtech;

select
x.CategoryNumber,x.ProductNumber,x.ProductUnit,
x.OperationNumber,x.OperationLabourConsumption,x.OperationUnit,
x.kg_na_godz,
x.PropertyNumber,x.PropertyValue,x.PropertyUnit,
x.kg_na_m2,
cast(iif(x.kg_na_m2 != 0, round(x.kg_na_godz/x.kg_na_m2,2), 0) as numeric(10,2)) as m2_na_godz,
x.PropertyNumber2,x.PropertyValue2,x.PropertyUnit2,
x.szerokosc_balotu_m2,
cast(iif(x.kg_na_m2 != 0 and x.szerokosc_balotu_m2 !=0, round(x.kg_na_godz/x.kg_na_m2,2)/x.szerokosc_balotu_m2, 0) as numeric(10,2)) as m_na_godz
FROM (
select ca.CategoryNumber, pr.ProductNumber, pu.UnitCode as ProductUnit,
o.OperationNumber, r.OperationLabourConsumption, ou.UnitCode as OperationUnit
,cast((iif(OperationLabourConsumption != 0,round(1/OperationLabourConsumption,0),2))  as numeric(10,2)) as kg_na_godz
,isnull(sub_prop.PropertyNumber,'') as PropertyNumber
,isnull(sub_prop.PropertyValue,0) as PropertyValue
,isnull(sub_prop.PropertyUnit,'') as PropertyUnit
,cast( round(isnull(sub_prop.PropertyValue,0) /1000,3)  as numeric(10,3)) as kg_na_m2
--
,isnull(sub_prop2.PropertyNumber,'') as PropertyNumber2
,isnull(sub_prop2.PropertyValue,0) as PropertyValue2
,isnull(sub_prop2.PropertyUnit,'') as PropertyUnit2
,cast( round(isnull(sub_prop2.PropertyValue,0) /1000,3)  as numeric(10,3)) as szerokosc_balotu_m2
--
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.Id
inner join dbo.Operations as o
on o.id = r.OperationId
inner join dbo.Units as ou
on ou.id = o.UnitId
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.Units as pu
on pu.id = pr.UnitId
--
left join 
(
select ppv.ProductId, pp.value as PropertyValue, prop.PropertyNumber, propu.Name as PropertyUnit
FROM dbo.ProductPropertyVersions as ppv
inner join dbo.ProductProperties as pp
on pp.ProductPropertiesVersionId = ppv.Id
inner join dbo.Properties as prop
on prop.Id = pp.PropertyId and prop.PropertyNumber = 'okg_WagaGramNaM2'
inner join dbo.Units as propu
on propu.id = prop.UnitId
) as sub_prop
on sub_prop.ProductId = pr.Id 
--
left join 
(
select ppv.ProductId, pp.value as PropertyValue, prop.PropertyNumber, propu.Name as PropertyUnit
FROM dbo.ProductPropertyVersions as ppv
inner join dbo.ProductProperties as pp
on pp.ProductPropertiesVersionId = ppv.Id
inner join dbo.Properties as prop
on prop.Id = pp.PropertyId and prop.PropertyNumber = 'okg_SzerokoscBalotu'
inner join dbo.Units as propu
on propu.id = prop.UnitId
) as sub_prop2
on sub_prop2.ProductId = pr.Id 
--
where 1=1
and ca.CategoryNumber = 'OKG'
--and o.OperationNumber = 'PO.100.10_WULKANIZACJA'
and v.DefaultVersion = 1
) as x
order by x.ProductNumber

-- okg_WagaGramNaM2 