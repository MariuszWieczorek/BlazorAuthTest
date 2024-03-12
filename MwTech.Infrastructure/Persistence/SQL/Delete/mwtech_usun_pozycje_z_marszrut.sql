/****** Script for SelectTopNRows command from SSMS  ******/
SELECT pr.ProductNumber, r.OperationLabourConsumption
  FROM [MwTech].[dbo].[ManufactoringRoutes] as r
  inner join dbo.RouteVersions as we
  on r.RouteVersionId = we.Id
  inner join dbo.Products as pr
  on pr.Id = we.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber = 'MIR' and OperationLabourConsumption < 1
  order by pr.ProductNumber

  DELETE [MwTech].[dbo].[ManufactoringRoutes]
  FROM [MwTech].[dbo].[ManufactoringRoutes] as r
  inner join dbo.RouteVersions as we
  on r.RouteVersionId = we.Id
  inner join dbo.Products as pr
  on pr.Id = we.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber = 'MIR' and OperationLabourConsumption < 1
