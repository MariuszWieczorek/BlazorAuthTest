/****** Script for SelectTopNRows command from SSMS  ******/
SELECT 
	   pr.ProductNumber
	  ,ca.CategoryNumber
  FROM dbo.products as pr
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where pr.ProductNumber like 'F%'

/*
SELECT 
	   pr.ProductNumber
	  ,ca.CategoryNumber
	  ,trim(substring(d.ProductNumber,4,50)) as Part
	  ,d.d1
	  ,d.d2
  FROM dbo.products as pr
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  inner join dbo.temp_days as d
  on pr.ProductNumber like '%'+trim(substring(d.ProductNumber,4,50))+'%'
  order by pr.ProductNumber
*/


  