SELECT p.ProductNumber, v.Name
	  ,v.DefaultVersion, v.IfsDefaultVersion, v.ComarchDefaultVersion	
   	  ,wc.ResourceNumber
	  ,re.ResourceNumber
	   ,p.ProductNumber
	   ,c.CategoryNumber
	   ,o.OperationNumber
      ,[MoveTime]
      ,[Overlap]
	  ,r.ChangeOverNumberOfEmployee
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
  where c.CategoryNumber = 'NAW'



  select x.ProductNumber
  ,sum(iif(x.DefaultVersion=1,1,0))
  from (
  SELECT p.ProductNumber, v.Name
	  ,v.DefaultVersion, v.IfsDefaultVersion, v.ComarchDefaultVersion	
   	  ,wc.ResourceNumber
	   ,c.CategoryNumber
	   ,o.OperationNumber
      ,[MoveTime]
      ,[Overlap]
	  ,r.ChangeOverNumberOfEmployee
  FROM dbo.Products as p 
  left join dbo.RouteVersions as v
  on p.Id = v.ProductId
  left join [MwTech].[dbo].[ManufactoringRoutes] as r
  on r.RouteVersionId = v.id
  inner join dbo.ProductCategories as c
  on c.Id = p.ProductCategoryId
  left join dbo.Operations as o
  on o.Id = r.OperationId
  left join dbo.Resources as wc
  on wc.Id = r.WorkCenterId
  left join dbo.Resources as re
  on re.Id = r.ResourceId
  where c.CategoryNumber = 'NAW'
  ) as x
  group by x.ProductNumber
  having sum(iif(x.DefaultVersion=1,1,0)) != 1



    select x.ProductNumber
  ,sum(iif(x.DefaultVersion=1,1,0))
  from (
  SELECT p.ProductNumber, v.Name
	  ,v.DefaultVersion, v.IfsDefaultVersion, v.ComarchDefaultVersion	
  FROM dbo.Products as p 
  left join dbo.ProductVersions as v
  on p.Id = v.ProductId
  inner join dbo.ProductCategories as c
  on c.Id = p.ProductCategoryId
  where c.CategoryNumber = 'NAW'
  ) as x
  group by x.ProductNumber
  having sum(iif(x.DefaultVersion=1,1,0)) != 1


  /*
update r
	  set ChangeOverNumberOfEmployee = 2
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
  where c.CategoryNumber = 'NAW'
  */