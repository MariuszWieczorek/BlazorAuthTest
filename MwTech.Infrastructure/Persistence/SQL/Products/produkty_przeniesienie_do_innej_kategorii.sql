/****** Script for SelectTopNRows command from SSMS  ******/
SELECT p.*
  FROM [MwTech].[dbo].Products as p
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  where ProductNumber like '%-TE'
  -- and ca.CategoryNumber = 'OWU'

 /*
 update p
  set ProductCategoryId = 84
  FROM [MwTech].[dbo].Products as p
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  where ProductNumber like '%-TE'
  and ca.CategoryNumber = 'OWU'
 */