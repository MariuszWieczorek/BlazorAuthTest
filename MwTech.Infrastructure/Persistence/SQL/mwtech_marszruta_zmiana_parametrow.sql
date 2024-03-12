/****** Script for SelectTopNRows command from SSMS  ******/
SELECT v.Name
   	  ,wc.ResourceNumber
	  ,re.ResourceNumber
	   ,p.ProductNumber
	   ,c.CategoryNumber
	   ,o.OperationNumber
      ,[MoveTime]
      ,[Overlap]
  FROM [MwTech].[dbo].[ManufactoringRoutes] as r
  inner join dbo.RouteVersions as v
  on r.RouteVersionId = v.Id
  inner join dbo.Products as p
  on p.Id = v.ProductId
  inner join dbo.ProductCategories as c
  on c.Id = p.ProductCategoryId
  inner join dbo.Operations as o
  on o.Id = r.OperationId
  inner join dbo.Resources as wc
  on wc.Id = r.WorkCenterId
  inner join dbo.Resources as re
  on re.Id = r.ResourceId
  where c.CategoryNumber = 'OBK'
  and o.OperationNumber = 'PO.20.10'


  UPDATE r
  set ResourceId = (select Id from dbo.Resources as rx where rx.ResourceNumber = 'PC.PO.PP-0')
  FROM [MwTech].[dbo].[ManufactoringRoutes] as r
  inner join dbo.RouteVersions as v
  on r.RouteVersionId = v.Id
  inner join dbo.Products as p
  on p.Id = v.ProductId
  inner join dbo.ProductCategories as c
  on c.Id = p.ProductCategoryId
  inner join dbo.Operations as o
  on o.Id = r.OperationId
  inner join dbo.Resources as wc
  on wc.Id = r.WorkCenterId
  where c.CategoryNumber = 'OBK'
  and o.OperationNumber = 'PO.20.10'

  /*
  UPDATE v
  set [MoveTime] = 0.1666
  FROM [MwTech].[dbo].[ManufactoringRoutes] as r
  inner join dbo.RouteVersions as v
  on r.RouteVersionId = v.Id
  inner join dbo.Products as p
  on p.Id = v.ProductId
  inner join dbo.ProductCategories as c
  on c.Id = p.ProductCategoryId
  inner join dbo.Operations as o
  on o.Id = r.OperationId
  where c.CategoryNumber = 'DAP'
  and o.OperationNumber = 'DAP_MALOWANIE_ZAW'
  */
  /*
  UPDATE r
  set [MoveTime] = 0.1666
  FROM [MwTech].[dbo].[ManufactoringRoutes] as r
  inner join dbo.RouteVersions as v
  on r.RouteVersionId = v.Id
  inner join dbo.Products as p
  on p.Id = v.ProductId
  inner join dbo.ProductCategories as c
  on c.Id = p.ProductCategoryId
  inner join dbo.Operations as o
  on o.Id = r.OperationId
  where c.CategoryNumber = 'DAP'
  and o.OperationNumber = 'DAP_MALOWANIE_ZAW'
  */