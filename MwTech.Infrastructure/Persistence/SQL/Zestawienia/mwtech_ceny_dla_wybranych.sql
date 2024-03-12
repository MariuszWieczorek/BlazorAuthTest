/****** Script for SelectTopNRows command from SSMS  ******/
  SELECT 
	  ca.CategoryNumber
     ,p.ProductNumber
	 ,coalesce(p.OldProductNumber,'') as oldProductNumber
	 ,coalesce(p.idx01,'') as idx01
	 ,coalesce(p.idx02,'') as idx02
	 ,c.Cost
  FROM dbo.Products as p
  inner join dbo. ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  left join [MwTech].[dbo].[ProductCosts] as c
  on p.Id = c.ProductId
  where c.AccountingPeriodId = (select Id from dbo.AccountingPeriods where PeriodNumber = '2022-12')
  -- and ca.CategoryNumber in ('DOB')
  -- and ( c.Cost = 0  or c.Cost = null or c.Cost is null)
  and p.ProductNumber in 
  (
  'DAP40060155TR150KB',
'DAP40060155TR15KB',
'DAP40060155TR218KB',
'DC15540060TR150KB',
'DC15540060TR150KBWK',
'DR1553507040060TR15KB',
'DR1553507040060TR15KBWK',
'DR15540060TR15KB',
'DR15540060TR15KBWK',
'DR15540060TR218KB',
'DR15540060TR218KBWK',
'DR15550055TR15KB',
'DR15550055TR15KBWK',
'DR1614065TR15KB',
'DR1614065TR15KBWK',
'DR1614065TR218KB',
'DR1614065TR218KBWK',
'DR16533155TR15KB',
'DR16533155TR15KBWK',
'DC15540060TR150KBK',
'DR1553507040060TR15KBK',
'DR15540060TR15KBK',
'DR15540060TR218KBK',
'DR15550055TR15KBK',
'DR1614065TR15KBK',
'DR1614065TR218KBK',
'DR16533155TR15KBK',
'DKJ40060155TR150KB',
'DKJ40060155TR15KB',
'DKJ40060155TR218KB',
'DOB40060155KB',
'DST40060155TR150KB',
'DST40060155TR15KB',
'DST40060155TR218KB',
'DWU40060155TR150KB',
'DWU40060155TR15KB',
'DWU40060155TR218KB',
'DWY40060155KB'
)
  
  order by ca.OrdinalNumber, p.ProductNumber
