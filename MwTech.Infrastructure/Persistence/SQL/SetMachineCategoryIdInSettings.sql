/****** Script for SelectTopNRows command from SSMS  ******/
SELECT 
       s.SettingNumber
      ,s.Name
      ,s.SettingCategoryId
      ,s.MachineCategoryId
	  ,c.MachineCategoryId
  FROM [MwTech].[dbo].[Settings] as s
  inner join dbo.SettingCategories as c
  on c.Id = s.SettingCategoryId

  update s
  set machinecategoryid = c.MachineCategoryId
  FROM [MwTech].[dbo].[Settings] as s
  inner join dbo.SettingCategories as c
  on c.Id = s.SettingCategoryId
