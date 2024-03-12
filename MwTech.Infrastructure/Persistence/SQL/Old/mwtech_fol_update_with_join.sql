/****** Script for SelectTopNRows command from SSMS  ******/
SELECT b.Id
      ,b.SetId
	  ,pr.ProductNumber
	  ,b.SetVersionId
      ,b.PartId
      ,b.PartQty
      ,[Excess]
      ,[OnProductionOrder]
  FROM [MwTech].[dbo].[Boms] as b
  inner join [MwTech].[dbo].[ProductVersions] as pv
  on pv.Id = b.SetVersionId and pv.ProductId = b.SetId
  inner join [MwTech].[dbo].[Products] as pr
  on pr.Id = pv.ProductId
  where pr.ProductNumber like '%FO%'


  SELECT b.Id
      ,b.SetId
	  ,pr.ProductNumber
	  ,b.SetVersionId
      ,b.PartId
      ,b.PartQty
      ,[Excess]
      ,[OnProductionOrder]
  FROM [MwTech].[dbo].[Boms] as b
  inner join [MwTech].[dbo].[Products] as pr
  on pr.Id = b.PartId
  where pr.ProductNumber like '%FOL%'


  UPDATE b
       set b.PartQty = 0.01
  FROM [MwTech].[dbo].[Boms] as b
  inner join [MwTech].[dbo].[Products] as pr
  on pr.Id = b.PartId
  where pr.ProductNumber like '%FOL%'