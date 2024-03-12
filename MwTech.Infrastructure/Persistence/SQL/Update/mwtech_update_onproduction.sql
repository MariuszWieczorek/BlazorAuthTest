/****** Script for SelectTopNRows command from SSMS  ******/
SELECT *
  FROM [MwTech].[dbo].[Boms] as b
  inner join dbo.Products as pr
  on pr.Id = b.PartId
  where pr.ProductNumber like 'MPW016%'
  -- and OnProductionOrder = 0

  update b
  set OnProductionOrder = 0, DoNotIncludeInTkw = 1, DoNotIncludeInWeight = 1
  FROM [MwTech].[dbo].[Boms] as b
  inner join dbo.Products as pr
  on pr.Id = b.PartId
  where pr.ProductNumber like 'MPW016%'
  -- and OnProductionOrder = 1
