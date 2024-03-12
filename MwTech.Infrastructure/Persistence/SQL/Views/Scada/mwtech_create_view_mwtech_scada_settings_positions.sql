CREATE OR ALTER VIEW mwtech_scada_settings_positions
AS
(
select sp.ProductSettingVersionId as versionId
,mc.MachineCategoryNumber
,m.MachineNumber
,w.ResourceNumber as WorkCenterNumber
,ca.CategoryNumber as ProductCategoryNumber
,pr.ProductNumber
,sc.SettingCategoryNumber
,sc.Name as SettingCategoryName
,sc.Color as SettingCategoryColor
,cast(sc.OrdinalNumber as varchar(3)) + '.' + cast(s.OrdinalNumber as varchar(3)) as OrdinalNo
,s.SettingNumber
,s.Name as SettingName
,sp.MinValue
,sp.Value
,sp.MaxValue
,u.Name as Unit
,sp.Text
from dbo.ProductSettingVersionPositions as sp
inner join dbo.ProductSettingVersions as v
on v.Id = sp.ProductSettingVersionId
inner join dbo.Products as pr
on pr.Id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.Settings as s
on s.Id = sp.SettingId
inner join dbo.SettingCategories as sc
on sc.Id = s.SettingCategoryId
inner join dbo.Units as u
on u.Id = s.UnitId
inner join dbo.Machines as m
on m.Id = v.MachineId
inner join dbo.MachineCategories as mc
on mc.Id = m.MachineCategoryId
inner join dbo.Resources as w
on w.Id = v.WorkCenterId
)
