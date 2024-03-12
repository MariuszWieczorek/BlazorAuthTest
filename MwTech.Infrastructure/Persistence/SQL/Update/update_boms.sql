/****** Script for SelectTopNRows command from SSMS  ******/
SELECT pr.ProductNumber
      ,b.OnProductionOrder
	  ,b.PartQty
  FROM [MwTech].[dbo].[Boms] as b
  inner join dbo.Products as pr
  on pr.Id = b.PartId
  where substring(pr.ProductNumber,1,3) in ('FOL')



  update b
  set PartQty = 0.01
  FROM [MwTech].[dbo].[Boms] as b
  inner join dbo.Products as pr
  on pr.Id = b.PartId
  where substring(pr.ProductNumber,1,3) in ('FOL')



