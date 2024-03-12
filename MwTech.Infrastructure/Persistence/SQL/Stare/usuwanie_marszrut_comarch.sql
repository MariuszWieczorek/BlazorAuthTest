/****** Script for SelectTopNRows command from SSMS  ******/
SELECT pr.ProductNumber ,v.VersionNumber, v.AlternativeNo
  FROM [MwTech].[dbo].RouteVersions as v
  inner join dbo.Products as pr
  on pr.Id = v.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  inner join dbo.ManufactoringRoutes as r
  on r.RouteVersionId = v.Id
  where ca.CategoryNumber in ('MIE')
  and v.ToIfs = 1
  and v.AlternativeNo > 100

  /*
  DELETE r
  FROM [MwTech].[dbo].RouteVersions as v
  inner join dbo.Products as pr
  on pr.Id = v.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  inner join dbo.ManufactoringRoutes as r
  on r.RouteVersionId = v.Id
  where ca.CategoryNumber in ('MIE')
  and v.ToIfs = 0
  and v.AlternativeNo > 100

  DELETE v
  FROM [MwTech].[dbo].RouteVersions as v
  inner join dbo.Products as pr
  on pr.Id = v.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber in ('MIE')
  and v.ToIfs = 0
  and v.AlternativeNo > 100
  
  */