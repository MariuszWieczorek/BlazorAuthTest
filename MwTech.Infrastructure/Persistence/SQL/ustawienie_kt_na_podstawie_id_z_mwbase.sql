/****** Script for SelectTopNRows command from SSMS  ******/
SELECT max(p.TechCardNumber)
  FROM [MwTech].[dbo].Products as p
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  where ca.CategoryNumber = 'ODR'

--  update dbo.Products set TechCardNumber = 109 where Id = 94622


SELECT p.id,p.ProductNumber, p.TechCardNumber
  FROM [MwTech].[dbo].Products as p
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  where ca.CategoryNumber = 'ODR'
  and p.TechCardNumber is null


 /* 
  update p
  set TechCardNumber = null
  FROM [MwTech].[dbo].Products as p
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  where ca.CategoryNumber = 'ODR'
  */
 
SELECT p.ProductNumber, p.TechCardNumber, dr.pozId
  FROM [MwTech].[dbo].Products as p
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  inner join mwbase.prdkabat.drutowki as dr
  on trim(dr.indeks) = trim(p.ProductNumber)
  where ca.CategoryNumber = 'ODR'


  /*
 update p
 set TechCardNumber = dr.pozId
 FROM [MwTech].[dbo].Products as p
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  inner join mwbase.prdkabat.drutowki as dr
  on trim(dr.indeks) = trim(p.ProductNumber)
  where ca.CategoryNumber = 'ODR'
  */



/* 
 update p
  set ProductCategoryId = 84
  FROM [MwTech].[dbo].Products as p
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  where ProductNumber like '%-TE'
  and ca.CategoryNumber = 'OWU'
 */