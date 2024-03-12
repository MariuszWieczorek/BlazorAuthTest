


SELECT pr.ProductNumber, r.OrdinalNumber, op.OperationNumber, op.No
  FROM [MwTech].[dbo].[ManufactoringRoutes] as r
  inner join dbo.RouteVersions as we
  on r.RouteVersionId = we.Id
  inner join dbo.Products as pr
  on pr.Id = we.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  inner join dbo.Operations as op
  on op.Id = r.OperationId
  where op.No != r.OrdinalNumber
  order by pr.ProductNumber




  Update  r
  set OrdinalNumber = op.No
  FROM [MwTech].[dbo].[ManufactoringRoutes] as r
  inner join dbo.RouteVersions as we
  on r.RouteVersionId = we.Id
  inner join dbo.Products as pr
  on pr.Id = we.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  inner join dbo.Operations as op
  on op.Id = r.OperationId
  where op.No != r.OrdinalNumber
  