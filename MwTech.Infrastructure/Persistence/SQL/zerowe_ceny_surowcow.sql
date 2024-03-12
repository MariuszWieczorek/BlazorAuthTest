/****** Script for SelectTopNRows command from SSMS  ******/
SELECT pr1.ProductNumber
  FROM [MwTech].[dbo].[Boms] as b
  inner join dbo.Products as pr
  on pr.Id = b.SetId
  inner join dbo.Products as pr1
  on pr1.Id = b.Partid
  left join dbo.ProductCosts as co
  on co.AccountingPeriodId = 17 and co.ProductId = pr1.id
  where 1 = 1
  and ( pr.ProductNumber like 'NAW%' or pr.ProductNumber like 'MIE%')
  and ( co.Cost is null or co.Cost = 0)
  group by pr1.ProductNumber


  /*
  UPDATE b
  SET DoNotExportToIfs = 1
  FROM [MwTech].[dbo].[Boms] as b
  inner join dbo.Products as pr
  on pr.Id = b.SetId
  inner join dbo.Products as pr1
  on pr1.Id = b.Partid
  where DoNotExportToIfs = 0 and PartQty < 0
  and pr.ProductNumber not like 'M.%'
  */
