/****** Script for SelectTopNRows command from SSMS  ******/
SELECT [ProductNumber]
      ,[Name]
	  ,REPLACE(ProductNumber,'KB','00') as newProductNumber
	  ,REPLACE(Name,'KB','00') as newName
  FROM [MwTech].[dbo].[Products]
  where ProductNumber like 'D__OT%'

  -- 'D__LS%', 'D__OT%'

  update [MwTech].[dbo].[Products]
  set 
  ProductNumber = REPLACE(ProductNumber,'KB','00'),
  Name = REPLACE(Name,'KB','00')
  where ProductNumber like 'D__OT%'
