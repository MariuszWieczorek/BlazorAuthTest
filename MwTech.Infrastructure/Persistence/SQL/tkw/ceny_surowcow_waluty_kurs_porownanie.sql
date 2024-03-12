use mwtech;

declare @periodNew int = 27;
declare @periodOld int = 26;

select z.CategoryNumber,z.ProductNumber,z.ProductName,z.uzyte_w_bom
,z.NewPeriod, z.NewCostInCurrency, z.NewCurrency, z.NewRate, z.NewCostInPLN
,z.OldPeriod, z.OldCostInCurrency, z.OldCurrency, z.OldRate, z.OldCostInPLN
,z.OldCostInPLN - z.NewCostInPLN as Roznica 
FROM (
select n.CategoryNumber,n.ProductNumber,n.ProductName,n.uzyte_w_bom
,n.Period NewPeriod, n.cost as NewCostInCurrency, n.Currency as NewCurrency, n.rate as NewRate
,Round(IIF(n.Currency = 'PLN', n.Cost, n.Cost * n.Rate),2) as NewCostInPLN
,o.Period OldPeriod, o.cost as OldCostInCurrency, o.Currency as OldCurrency, o.rate as OldRate
,Round(IIF(o.Currency = 'PLN', o.Cost, o.Cost * o.Rate),2) as OldCostInPLN
from
(
select pe.Name as Period ,ca.CategoryNumber , pr.ProductNumber, pr.Name as ProductName 
  , isnull(co.Cost,0) as cost
  , isnull(cu.CurrencyCode,'') as Currency
  , isnull(r.Rate,0) as rate
  , (select COUNT(*) from dbo.Boms as b WHERE b.PartId = pr.Id ) as uzyte_w_bom
  , co.CalculatedDate
  , co.ModifiedDate
  from dbo.Products as pr
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  left join dbo.ProductCosts as co
  on co.ProductId = pr.Id
  and co.AccountingPeriodId = @periodNew
  inner join dbo.AccountingPeriods as pe
  on pe.Id = @periodNew
  left join dbo.Currencies cu
  on cu.Id = co.CurrencyId
  left join dbo.CurrencyRates as r
  on r.FromCurrencyId = co.CurrencyId and r.AccountingPeriodId = @periodNew
  where 1 = 1
--  AND ca.CategoryNumber IN ('SUR','DRU','OKS','ZAW-S','OPK','SMP','OME','PREP','MON')
 ) as n
INNER JOIN
(
select pe.Name as Period ,ca.CategoryNumber , pr.ProductNumber, pr.Name as ProductName 
  , isnull(co.Cost,0) as cost
  , isnull(cu.CurrencyCode,'') as Currency
  , isnull(r.Rate,0) as rate
  , (select COUNT(*) from dbo.Boms as b WHERE b.PartId = pr.Id ) as uzyte_w_bom
  , co.CalculatedDate
  , co.ModifiedDate
  from dbo.Products as pr
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  left join dbo.ProductCosts as co
  on co.ProductId = pr.Id
  and co.AccountingPeriodId = @periodOld
  inner join dbo.AccountingPeriods as pe
  on pe.Id = @periodOld
  left join dbo.Currencies cu
  on cu.Id = co.CurrencyId
  left join dbo.CurrencyRates as r
  on r.FromCurrencyId = co.CurrencyId and r.AccountingPeriodId = @periodOld
  where 1 = 1
--  AND ca.CategoryNumber IN ('SUR','DRU','OKS','ZAW-S','OPK','SMP','OME','PREP','MON')
  ) as o
 on o.ProductNumber = n.ProductNumber
 ) as z
 order by z.CategoryNumber,z.ProductNumber 
