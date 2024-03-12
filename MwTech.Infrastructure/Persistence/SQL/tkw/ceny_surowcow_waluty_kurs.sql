use mwtech;

declare @period int = 26;

select pe.Name as Period ,ca.CategoryNumber , pr.ProductNumber, pr.Name as ProductName 
  , isnull(co.Cost,0) as cost
  , isnull(cu.CurrencyCode,'') as Currency
  , isnull(r.Rate,0) as rate
  , (select COUNT(*) from dbo.Boms as b WHERE b.PartId = pr.Id ) as uzyte_w_bom
  from dbo.Products as pr
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  left join dbo.ProductCosts as co
  on co.ProductId = pr.Id
  and co.AccountingPeriodId = @period
  inner join dbo.AccountingPeriods as pe
  on pe.Id = @period
  left join dbo.Currencies cu
  on cu.Id = co.CurrencyId
  left join dbo.CurrencyRates as r
  on r.FromCurrencyId = co.CurrencyId and r.AccountingPeriodId = @period
  where 1 = 1
  -- AND pr.ProductNumber like 'OKS%'
  AND ca.CategoryNumber IN ('SUR','DRU','OKS','ZAW-S','OPK','SMP','OME','PREP')
  order by ca.CategoryNumber,pr.ProductNumber
  
