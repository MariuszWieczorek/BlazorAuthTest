/*
SELECT a.ProductNumber as ProductNumber
	   ,a.Name as ProductName	
	   ,u.Name as unit
	   ,ca.CategoryNumber as grupa
	   ,coalesce(a.OldProductNumber,'') as OldProductNumber
	   ,coalesce(a.Idx01,'') as idx01
	   ,coalesce(a.Idx02,'') as idx02
  FROM [MwTech].[dbo].[Products] as a
  left join [192.168.1.186].[MwTech].[dbo].[Products] as b
  on a.Id = b.id
  inner join [MwTech].[dbo].[ProductCategories] as ca
  on ca.Id = a.ProductCategoryId
  inner join dbo.Units as u
  on u.Id = a.UnitId
  where b.ProductNumber is null
  order by ca.CategoryNumber, a.ProductNumber

  select 'KT1' as lok
  ,sp.ProductNumber
  ,'*' as wariant
  ,'*' as opis
  ,b.OrdinalNumber as lp
  ,pp.ProductNumber
  ,b.PartQty
  ,b.Excess
  ,b.OnProductionOrder
  ,'A' as metoda
  from [MwTech].[dbo].boms as b
  inner join [MwTech].[dbo].Products as sp
  on sp.Id = b.SetId 
  inner join [MwTech].[dbo].Products as pp
  on pp.Id = b.PartId 
  where b.SetId in 
  (
  SELECT a.Id
  FROM [MwTech].[dbo].[Products] as a
  left join [192.168.1.186].[MwTech].[dbo].[Products] as b
  on a.Id = b.id
  inner join [MwTech].[dbo].[ProductCategories] as ca
  on ca.Id = a.ProductCategoryId
  where b.ProductNumber is null
  )
  */


  select 'KT1' as lok
  ,p.ProductNumber
  ,v.VersionNumber as wariant
  ,v.Name as opis
  ,r.OrdinalNumber as lp
  ,o.OperationNumber
  ,wc.ResourceNumber as wc
  ,r.ChangeOverMachineConsumption
  ,r.ChangeOverLabourConsumption
  ,ch.ResourceNumber as ch
  ,r.ChangeOverNumberOfEmployee
  ,r.OperationMachineConsumption
  ,r.OperationLabourConsumption
  ,'Godz./jedn.' as jm	
  ,ore.ResourceNumber as ore
  ,r.ResourceQty
  ,r.MoveTime
  ,r.Overlap
  from [MwTech].[dbo].RouteVersions as v
  inner join [MwTech].[dbo].Products as p
  on p.Id = v.ProductId
  inner join  [MwTech].[dbo].ManufactoringRoutes as r
  on r.RouteVersionId = v.id
  inner join [MwTech].[dbo].Operations as o
  on o.Id = r.OperationId
  inner join [MwTech].[dbo].Resources as wc
  on wc.Id = r.WorkCenterId
  inner join [MwTech].[dbo].Resources as ch
  on ch.Id = r.ChangeOverResourceId
  inner join [MwTech].[dbo].Resources as ore
  on ore.Id = r.ResourceId
  where v.ProductId in 
  (
  SELECT a.Id
  FROM [MwTech].[dbo].[Products] as a
  left join [192.168.1.186].[MwTech].[dbo].[Products] as b
  on a.Id = b.id
  inner join [MwTech].[dbo].[ProductCategories] as ca
  on ca.Id = a.ProductCategoryId
  where b.ProductNumber is null
  )


