/****** Script for SelectTopNRows command from SSMS  ******/
SELECT pr.ProductNumber, pr.TechCardNumber
      ,[AlternativeNo]
	  ,[ProductSettingVersionNumber]
      ,[MachineCategoryId]
      ,[MachineId]
      ,[WorkCenterId]
      ,[DefaultVersion]
      ,s.Name 
  FROM [MwTech].[dbo].[ProductSettingVersions] as s
  inner join dbo.Products as pr
  on pr.Id = s.ProductId 
  where MachineCategoryId = 8
  -- and MachineId = 64
  -- and AlternativeNo = 1
  and s.LastCsvFileDate is null


  /*
  update s
  set WorkCenterId = 9
  FROM [MwTech].[dbo].[ProductSettingVersions] as s
  inner join dbo.Products as pr
  on pr.Id = s.ProductId 
  where MachineCategoryId = 8
  -- and MachineId = 64
  and AlternativeNo = 1
  */