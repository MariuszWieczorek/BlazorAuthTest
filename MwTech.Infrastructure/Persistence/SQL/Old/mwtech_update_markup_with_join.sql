/****** Script for SelectTopNRows command from SSMS  ******/
SELECT pr.ProductNumber
		, re.ResourceNumber
      ,[ProductVersionId]
      ,[OperationId]
      ,[ResourceId]
      ,[Value]
      ,[ResourceQty]
      ,[Markup]
  FROM [MwTech].[dbo].[ManufactoringRoutes] as r
  inner join [MwTech].[dbo].[ProductVersions] as pv
  on pv.Id = r.ProductVersionId
  inner join [MwTech].[dbo].[Products] as pr
  on pr.Id = pv.ProductId
  inner join [MwTech].[dbo].[ProductCategories] as ca
  on ca.Id = pr.ProductCategoryId
  inner join [MwTech].[dbo].Resources as re
  on re.Id = r.ResourceId
  where ca.CategoryNumber in ('DWY','DOB','DAP','DST','DWU','DKJ','DET')
  and substring(re.ResourceNumber,1,3) in ('PC.','KP.')

    UPDATE r
       set r.Markup = 170
  FROM [MwTech].[dbo].[ManufactoringRoutes] as r
  inner join [MwTech].[dbo].[ProductVersions] as pv
  on pv.Id = r.ProductVersionId
  inner join [MwTech].[dbo].[Products] as pr
  on pr.Id = pv.ProductId
  inner join [MwTech].[dbo].[ProductCategories] as ca
  on ca.Id = pr.ProductCategoryId
  inner join [MwTech].[dbo].Resources as re
  on re.Id = r.ResourceId
  where ca.CategoryNumber in ('DWY','DOB','DAP','DST','DWU','DKJ','DET')
  and substring(re.ResourceNumber,1,3) in ('PC.','KP.')
