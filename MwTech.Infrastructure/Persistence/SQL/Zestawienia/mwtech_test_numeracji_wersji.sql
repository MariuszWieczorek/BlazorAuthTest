/****** Script for SelectTopNRows command from SSMS  ******/
select x.ProductNumber,x.VersionNumber ,COUNT(*) 
from (
SELECT ca.CategoryNumber, p.ProductNumber
       ,r.VersionNumber
      ,r.DefaultVersion
      ,r.Name
      ,r.ProductId
      ,r.ToIfs
      ,r.ComarchDefaultVersion
      ,r.IfsDefaultVersion
  FROM [MwTech].[dbo].[RouteVersions] as r
  inner join dbo.Products as p
  on p.Id = r.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId) as x
  group by x.ProductNumber,x.VersionNumber
  having COUNT(*) > 1 
  order by x.ProductNumber


  select x.ProductNumber,x.VersionNumber ,COUNT(*) 
from (
SELECT ca.CategoryNumber, p.ProductNumber
       ,r.VersionNumber
      ,r.DefaultVersion
      ,r.Name
      ,r.ProductId
      ,r.ToIfs
      ,r.ComarchDefaultVersion
      ,r.IfsDefaultVersion
  FROM [MwTech].[dbo].[ProductVersions] as r
  inner join dbo.Products as p
  on p.Id = r.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId) as x
  group by x.ProductNumber,x.VersionNumber
  having COUNT(*) > 1 
  order by x.ProductNumber
  -- r.VersionNumber = 1 and r.DefaultVersion = 0
  -- and ca.CategoryNumber in ('BSOLID','BSOLIDP')

  /*
  UPDATE r
  set DefaultVersion = 1 , IfsDefaultVersion = 1
    FROM [MwTech].[dbo].[RouteVersions] as r
  inner join dbo.Products as p
  on p.Id = r.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  where r.VersionNumber = 1 and r.DefaultVersion = 0
  and ca.CategoryNumber in ('BSOLID','BSOLIDP')
  */