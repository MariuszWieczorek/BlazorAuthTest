/****** Script for SelectTopNRows command from SSMS  ******/
SELECT p.ProductNumber
	,v.VersionNumber
	,v.DefaultVersion
	,v.IfsDefaultVersion
	,ca.CategoryNumber
  FROM dbo.RouteVersions as v
  inner join dbo.Products as p
  on p.id = v.ProductId
  inner join dbo.ProductCategories as ca
  on ca.id = p.ProductCategoryId
  where ca.CategoryNumber in ('WW-PROF','WW-PPROF','WW-AKL','WW-ORN')
  

  /*
  update v
  set IfsDefaultVersion = iif(v.VersionNumber = 0,1,0),
  DefaultVersion = iif(v.VersionNumber = 0,1,0)
    FROM [MwTech].[dbo].[ManufactoringRoutes] as r
  inner join dbo.RouteVersions as v
  on v.id = r.RouteVersionId
  inner join dbo.Products as p
  on p.id = v.ProductId
  inner join dbo.ProductCategories as ca
  on ca.id = p.ProductCategoryId
  where ca.CategoryNumber in ('MOD','MOP','MON')
  and v.VersionNumber < 3
  */