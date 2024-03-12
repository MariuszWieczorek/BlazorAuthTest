/****** Script for SelectTopNRows command from SSMS  ******/
SELECT 
       m.PropertyId
	   ,pr.PropertyNumber
      ,m.ProductCategoryId
	  ,ca.CategoryNumber
  FROM [MwTech].[dbo].[PropertiesProductCategoriesMaps] as m
  inner join dbo.Properties as pr
  on pr.Id = m.PropertyId
  inner join dbo.ProductCategories as ca
  on ca.Id = m.ProductCategoryId

  /*
  insert into [MwTech].[dbo].[PropertiesProductCategoriesMaps]
  (PropertyId,ProductCategoryId)
  values (
  (Select Id from dbo.Properties where PropertyNumber = 'dst_dlugosc_po_styknieciu') ,
  (Select Id from dbo.ProductCategories where CategoryNumber = 'DWU') 
  )
  */