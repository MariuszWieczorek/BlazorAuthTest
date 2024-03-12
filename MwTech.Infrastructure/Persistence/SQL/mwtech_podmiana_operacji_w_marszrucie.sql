/****** Script for SelectTopNRows command from SSMS  ******/
SELECT pr.ProductNumber
      ,ca.CategoryNumber 
	  , we.VersionNumber
      ,[ToIfs]
      ,[ComarchDefaultVersion]
      ,[IfsDefaultVersion]
	  ,rp.OperationId
  FROM [MwTech].[dbo].[RouteVersions] as we
  inner join dbo.Products as pr
  on pr.Id = we.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  inner join dbo.ManufactoringRoutes as rp
  on rp.RouteVersionId = we.id
  where ToIfs = 0 
  and rp.OperationId = 19

  UPDATE rp
  set OperationId = 51
  FROM [MwTech].[dbo].[RouteVersions] as we
  inner join dbo.Products as pr
  on pr.Id = we.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  inner join dbo.ManufactoringRoutes as rp
  on rp.RouteVersionId = we.id
  where ToIfs = 0 
  and rp.OperationId = 20

