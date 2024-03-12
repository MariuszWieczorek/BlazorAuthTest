/****** Script for SelectTopNRows command from SSMS  ******/
SELECT ca.CategoryNumber
      ,pr.TechCardNumber
	  ,COUNT(*) as ile
  FROM [MwTech].[dbo].[Products] as pr
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where pr.TechCardNumber is not null
 -- and ca.CategoryNumber = 'OKC'
  group by pr.TechCardNumber, CategoryNumber
  having COUNT(*) > 1



  SELECT ca.CategoryNumber
	  ,MAX(pr.TechCardNumber) as mx
  FROM [MwTech].[dbo].[Products] as pr
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where pr.TechCardNumber is not null
  and ca.CategoryNumber = 'OBK'
  group by ca.CategoryNumber

