/****** Script for SelectTopNRows command from SSMS  ******/
SELECT 
       [SetId]
      ,[PartId]
      ,[PartQty]
      ,[SetVersionId]
      ,[Excess]
      ,[OnProductionOrder]
      ,[Layer]
  FROM [MwTech].[dbo].[Boms] as b
  inner join dbo.Products as p
  on p.Id = b.SetId
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  inner join dbo.Products as pp
  on pp.Id = b.PartId
  inner join dbo.ProductCategories as cp
  on cp.Id = pp.ProductCategoryId
  where cp.CategoryNumber = 'DWU'

  /*
  UPDATE b
  set Excess = 1.5
  FROM [MwTech].[dbo].[Boms] as b
  inner join dbo.Products as p
  on p.Id = b.SetId
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  inner join dbo.Products as pp
  on pp.Id = b.PartId
  inner join dbo.ProductCategories as cp
  on cp.Id = pp.ProductCategoryId
  where cp.CategoryNumber = 'DWU'
  */