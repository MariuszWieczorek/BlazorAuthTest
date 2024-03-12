CREATE OR ALTER VIEW mwtech_scada_settings_headers
AS
(
SELECT v.Id as versionId
      ,v.AlternativeNo
	  ,v.ProductSettingVersionNumber as versionNumber
	  ,v.Rev
      ,v.IsActive
	  ,v.DefaultVersion
	  ,v.IsAccepted01
	  ,v.IsAccepted02
	  ,v.IsAccepted03
      ,v.Name as versionName
      ,pr.ProductNumber
	  ,pr.TechCardNumber
	  ,pr.Name as ProductName
	  ,v.MachineCategoryId
	  ,mc.MachineCategoryNumber
	  ,mc.Name as MachineCategoryName
	  ,v.MachineId
	  ,ma.MachineNumber	    
      ,ma.Name as MachineName
	  ,v.WorkCenterId
	  ,res.ResourceNumber as WORK_CENTER_NO
      ,v.Description versionDescription
      ,v.Accepted01Date
	  ,trim(ua1.FirstName) + ' ' + TRIM(ua1.LastName) as Accepted01By
      ,v.Accepted02Date
	  ,trim(ua2.FirstName) + ' ' + TRIM(ua2.LastName) as Accepted02By
      ,v.Accepted03Date
	  ,trim(ua3.FirstName) + ' ' + TRIM(ua3.LastName) as Accepted03By
	  ,v.CreatedDate
	  ,trim(uc.FirstName) + ' ' + TRIM(uc.LastName) as CreatedBy
  FROM [MwTech].[dbo].[ProductSettingVersions] as v
  inner join dbo.Products as pr
  on pr.Id = v.ProductId
  inner join dbo.MachineCategories as mc
  on mc.Id = v.MachineCategoryId
  inner join dbo.Machines as ma
  on ma.Id = v.MachineId
  inner join dbo.Resources as res
  on res.Id = v.WorkCenterId
  left join dbo.AspNetUsers as ua1
  on ua1.Id = v.Accepted01ByUserId
  left join dbo.AspNetUsers as ua2
  on ua2.Id = v.Accepted02ByUserId
  left join dbo.AspNetUsers as ua3
  on ua3.Id = v.Accepted03ByUserId
  left join dbo.AspNetUsers as uc
  on uc.Id = v.CreatedByUserId
  Where v.IsActive = 1
)