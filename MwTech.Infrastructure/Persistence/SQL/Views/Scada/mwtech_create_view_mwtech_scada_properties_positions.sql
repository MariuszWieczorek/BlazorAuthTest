CREATE OR ALTER VIEW mwtech_scada_Properties_positions
AS
(
select pr.Id as ProductId
,sp.ProductPropertiesVersionId as VersionId
,pr.ProductNumber
,pr.Name as ProductName
,s.PropertyNumber
,s.ScadaPropertyNumber
,s.Name as PropertyName
,sp.MinValue
,sp.Value
,sp.MaxValue
,u.Name as Unit
,sp.Text
from dbo.ProductProperties as sp
inner join dbo.Properties as s
on s.Id = sp.PropertyId
inner join dbo.ProductPropertyVersions as v
on v.Id = sp.ProductPropertiesVersionId
inner join dbo.Products as pr
on pr.Id = v.ProductId
inner join dbo.Units as u
on u.Id = s.UnitId
)
