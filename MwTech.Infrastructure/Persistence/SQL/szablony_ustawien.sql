select ca.OrdinalNumber, ca.SettingCategoryNumber as CategoryName, s.OrdinalNumber, s.SettingNumber, s.Name as SettingName, u.Name as Unit
from dbo.Settings as s
inner join dbo.SettingCategories as ca
on ca.Id = s.SettingCategoryId 
inner join dbo.Units as u
on u.Id = s.UnitId
where ca.MachineCategoryId = 11
order by ca.OrdinalNumber,s.OrdinalNumber