select 'DWY' as CategoryNumber
, x.ProductNumber
, 'dwy_roznica_gruba_cienka' as PropertyNumber
, x.VersionNumber
, x.AlternativeNo
, 1 as IsActive
, 1 as InDefault
, REPLACE(CAST(SUM(ValueX) - 0.1 as varchar(10)),'.',',') as MinValue
, REPLACE(CAST(SUM(ValueX)  as varchar(10)),'.',',') as DefValue
, REPLACE(CAST(SUM(ValueX) + 0.1  as varchar(10)),'.',',') as MaxValue
, '' as TextValue
FROM (
select pr.ProductNumber, v.AlternativeNo, v.VersionNumber, prop.PropertyNumber
,CASE WHEN prop.PropertyNumber = 'dwy_gruboscGrubaScianka' THEN pp.Value WHEN prop.PropertyNumber = 'dwy_gruboscCienkaScianka' THEN pp.Value * (-1) ELSE 0 END as ValueX
from dbo.ProductProperties as pp
inner join dbo.Properties as prop
on prop.Id = pp.PropertyId
inner join dbo.ProductPropertyVersions as v
on v.Id = pp.ProductPropertiesVersionId
inner join dbo.Products as pr
on pr.Id = v.ProductId
where 1 = 1
and v.DefaultVersion = 1
and v.IsActive = 1
and prop.PropertyNumber IN ( 'dwy_gruboscGrubaScianka', 'dwy_gruboscCienkaScianka')
and pp.Value != 0
) as x
group by x.ProductNumber, x.AlternativeNo, x.VersionNumber
having SUM(ValueX) > 0
order by x.ProductNumber, x.AlternativeNo, x.VersionNumber