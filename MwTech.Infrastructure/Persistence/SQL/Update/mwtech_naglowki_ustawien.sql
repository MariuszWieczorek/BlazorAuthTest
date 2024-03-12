/****** Script for SelectTopNRows command from SSMS  ******/
use MwTech

SELECT pr.ProductNumber
	  , s.Name
      , [AlternativeNo]
      ,[ProductSettingVersionNumber]
      ,[DefaultVersion]
      ,[ProductId]
      ,[MachineCategoryId]
      ,[MachineId]
      ,[WorkCenterId],
	  ca.TechCardNumber+'-'+trim(cast(pr.TechCardNumber as char(10)))+'-'+w.ResourceNumber+'-'+trim(cast(AlternativeNo as char(2)))+'-'+trim(cast(ProductSettingVersionNumber as char(2)))+'-'+trim(cast(Rev as char(2)))
  FROM [MwTech].[dbo].[ProductSettingVersions] as s
  inner join dbo.Products as pr
  on pr.Id = s.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  inner join dbo.Resources as w
  on w.Id = s.WorkCenterId
  where 1 = 1
  and ca.CategoryNumber in ('OKC')
 
 /*
  UPDATE s
  set AlternativeNo = 1
  FROM [MwTech].[dbo].[ProductSettingVersions] as s
  inner join dbo.Products as pr
  on pr.Id = s.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  inner join dbo.Resources as w
  on w.Id = s.WorkCenterId
  where 1 = 1
  -- and ca.CategoryNumber = 'ODR'
  and AlternativeNo = 0
  */
  /*
delete p
  FROM [MwTech].[dbo].[ProductSettingVersions] as s
  inner join dbo.Products as pr
  on pr.Id = s.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  inner join dbo.Resources as w
  on w.Id = s.WorkCenterId
  inner join dbo.ProductSettingVersionPositions as p
  on p.ProductSettingVersionId = s.id
  inner join dbo.Settings as se
  on se.Id = p.SettingId
  where ca.CategoryNumber = 'OKG'
  --and p.MaxValue = 999
  */

  /*
  UPDATE s
  set DefaultVersion = 1, ProductSettingVersionNumber = 1
  FROM [MwTech].[dbo].[ProductSettingVersions] as s
  inner join dbo.Products as pr
  on pr.Id = s.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber = 'ODR'

  */

  /*
  UPDATE s
  SET
  --set IsAccepted01 = 1, IsAccepted02 = 1, IsAccepted03 = 1, Accepted01Date = GETDATE(), Accepted02Date = GETDATE(), Accepted03Date = GETDATE() 
  -- ,Accepted01ByUserId = '7f194520-56b2-4684-b304-4fc98884c35b',Accepted02ByUserId = '7f194520-56b2-4684-b304-4fc98884c35b',Accepted03ByUserId = '7f194520-56b2-4684-b304-4fc98884c35b'
 Name =	  ca.TechCardNumber+'-'+trim(cast(pr.TechCardNumber as char(10)))+'-'+w.ResourceNumber+'-'+trim(cast(AlternativeNo as char(2)))+'.'+trim(cast(ProductSettingVersionNumber as char(2)))+'.'+trim(cast(Rev as char(2)))
  FROM [MwTech].[dbo].[ProductSettingVersions] as s
  inner join dbo.Products as pr
  on pr.Id = s.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  inner join dbo.Resources as w
  on w.Id = s.WorkCenterId
  where 1 = 1
  and ca.CategoryNumber in ('OKC')
  */
