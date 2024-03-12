/****** Script for SelectTopNRows command from SSMS  ******/
SELECT pr.ProductNumber,v.VersionNumber,v.AlternativeNo, v.DefaultVersion
  FROM [MwTech].[dbo].[RouteVersions] as v
  inner join dbo.Products as pr
  on pr.Id = v.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where v.AlternativeNo != 1 and DefaultVersion = 1
--   and ca.CategoryNumber in ('DWU')

SELECT pr.ProductNumber,COUNT(*)
  FROM [MwTech].[dbo].[RouteVersions] as v
  inner join dbo.Products as pr
  on pr.Id = v.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where v.DefaultVersion = 1
  group by pr.ProductNumber
  having COUNT(*) > 1


/*
SELECT pr.ProductNumber,v.VersionNumber,v.AlternativeNo, v.DefaultVersion
  FROM dbo.Products as pr
  left join [dbo].[RouteVersions] as v
  on pr.Id = v.ProductId and v.DefaultVersion = 1
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where v.AlternativeNo is null
*/

