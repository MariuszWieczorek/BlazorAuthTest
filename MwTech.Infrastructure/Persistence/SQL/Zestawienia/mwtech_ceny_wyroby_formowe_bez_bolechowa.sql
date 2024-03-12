/****** Script for SelectTopNRows command from SSMS  ******/
  SELECT 
	  ca.CategoryNumber
	  ,ca.Name
     ,p.ProductNumber
	 ,coalesce(p.OldProductNumber,'') as oldProductNumber
	 ,coalesce(p.idx01,'') as idx01
	 ,coalesce(p.idx02,'') as idx02
	 ,c.Cost
  FROM [MwTech].[dbo].[ProductCosts] as c
  inner join dbo.Products as p
  on p.Id = c.ProductId
  inner join dbo. ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  where c.AccountingPeriodId = (select Id from dbo.AccountingPeriods where PeriodNumber = '2023-01-B-IFS')
   and ca.CategoryNumber in ('MIE','NAW','MIP','MIR','MIF')
  order by ca.OrdinalNumber, p.ProductNumber
