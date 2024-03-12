use MwTech;

IF OBJECT_ID('dbo.temp','U') IS NOT NULL
	DROP TABLE dbo.temp;

  create table dbo.temp
  (
	symbol varchar(10)
  )
  
  insert into dbo.temp (symbol) values ('BNAZIMNO')
 
 select * from dbo.temp


  select COUNT(*)
  FROM [MwTech].[dbo].[Boms] as b
  inner join [MwTech].[dbo].ProductVersions as pv
  on pv.Id = b.SetVersionId
  inner join [MwTech].[dbo].Products as pr
  on pr.Id = pv.ProductId and pr.Id = b.SetId
  inner join [MwTech].[dbo].ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber IN (select symbol from temp)
 
  select COUNT(*)
  FROM  [MwTech].[dbo].ProductVersions as pv
  inner join [MwTech].[dbo].Products as pr
  on pr.Id = pv.ProductId and pr.Id = pv.ProductId
  inner join [MwTech].[dbo].ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber IN (select symbol from temp)

  select COUNT(*)
  FROM [MwTech].[dbo].ManufactoringRoutes as b
  inner join [MwTech].[dbo].RouteVersions as pv
  on pv.Id = b.RouteVersionId
  inner join [MwTech].[dbo].Products as pr
  on pr.Id = pv.ProductId
  inner join [MwTech].[dbo].ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber IN (select symbol from temp)


  select COUNT(*)
  FROM [MwTech].[dbo].RouteVersions as pv
  inner join [MwTech].[dbo].Products as pr
  on pr.Id = pv.ProductId
  inner join [MwTech].[dbo].ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber IN (select symbol from temp)
 
  select COUNT(*)
  FROM [MwTech].[dbo].ProductCosts as pv
  inner join [MwTech].[dbo].Products as pr
  on pr.Id = pv.ProductId
  inner join [MwTech].[dbo].ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber IN (select symbol from temp)

  select COUNT(*)
  FROM [MwTech].[dbo].Products as pr
  inner join [MwTech].[dbo].ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber IN (select symbol from temp)



  DELETE [MwTech].[dbo].[Boms]
  FROM [MwTech].[dbo].[Boms] as b
  inner join [MwTech].[dbo].ProductVersions as pv
  on pv.Id = b.SetVersionId
  inner join [MwTech].[dbo].Products as pr
  on pr.Id = pv.ProductId and pr.Id = b.SetId
  inner join [MwTech].[dbo].ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber IN (select symbol from temp)
  
  DELETE [MwTech].[dbo].ProductVersions
  FROM  [MwTech].[dbo].ProductVersions as pv
  inner join [MwTech].[dbo].Products as pr
  on pr.Id = pv.ProductId and pr.Id = pv.ProductId
  inner join [MwTech].[dbo].ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber IN (select symbol from temp)

  DELETE [MwTech].[dbo].ManufactoringRoutes
  FROM [MwTech].[dbo].ManufactoringRoutes as b
  inner join [MwTech].[dbo].RouteVersions as pv
  on pv.Id = b.RouteVersionId
  inner join [MwTech].[dbo].Products as pr
  on pr.Id = pv.ProductId
  inner join [MwTech].[dbo].ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber IN (select symbol from temp)

  DELETE [MwTech].[dbo].RouteVersions
  FROM [MwTech].[dbo].RouteVersions as pv
  inner join [MwTech].[dbo].Products as pr
  on pr.Id = pv.ProductId
  inner join [MwTech].[dbo].ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber IN (select symbol from temp)

  Delete [MwTech].[dbo].ProductCosts
  FROM [MwTech].[dbo].ProductCosts as pv
  inner join [MwTech].[dbo].Products as pr
  on pr.Id = pv.ProductId
  inner join [MwTech].[dbo].ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber IN (select symbol from temp)

  
  DELETE [MwTech].[dbo].Products
  FROM [MwTech].[dbo].Products as pr
  inner join [MwTech].[dbo].ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber IN (select symbol from temp)
  


