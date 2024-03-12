DECLARE @ProductId int =  103421 --769 

select SUM(x.FinalPartProductQty) as SumQty,  SUM(x.FinalPartProductWeight) as SumWeight, COUNT(*) as ile
from dbo.mwtech_bom_cte(@ProductId,NULL) as x
where x.HowManyParts = 0
--group by x.PartProductNumber,  x.PartProductId

select SUM(z.SumQty) as TotalQTY, SUM(z.SumWeight) as TotalWeight, SUM(z.TotalCostPln) as TotalCost
from (
SELECT y.PartProductNumber,y.SumQty, cu.CurrencyCode, co.Cost, co.Cost * isnull(ra.Rate,1) * y.SumQty as TotalCostPln, pe.PeriodNumber
, isnull(ra.Rate,1) as rate
, y.SumWeight
FROM (
select x.PartProductNumber,  PartProductId,  SUM(x.FinalPartProductQty) as SumQty,  SUM(x.FinalPartProductWeight) as SumWeight
from dbo.mwtech_bom_cte(@ProductId,NULL) as x
where x.HowManyParts = 0
group by x.PartProductNumber,  x.PartProductId
) as y
left join dbo.ProductCosts as co
on co.ProductId = y.PartProductId
left join dbo.AccountingPeriods as pe
on pe.Id = co.AccountingPeriodId 
left join dbo.Currencies as cu
on cu.Id = co.CurrencyId
left join dbo.CurrencyRates as ra
on ra.AccountingPeriodId = pe.Id and ra.FromCurrencyId = cu.Id
where 1 = 1
and ( pe.PeriodNumber = '2024-01-A' or  pe.id IS NULL)
) as z


SELECT y.PartProductNumber,y.SumQty, cu.CurrencyCode, co.Cost, co.Cost * isnull(ra.Rate,1) * y.SumQty as TotalCostPln, pe.PeriodNumber
, isnull(ra.Rate,1) as rate
, y.SumWeight
FROM (
select x.PartProductNumber,  PartProductId,  SUM(x.FinalPartProductQty) as SumQty,  SUM(x.FinalPartProductWeight) as SumWeight
from dbo.mwtech_bom_cte(@ProductId,NULL) as x
where x.HowManyParts = 0
group by x.PartProductNumber,  x.PartProductId
) as y
left join dbo.ProductCosts as co
on co.ProductId = y.PartProductId
left join dbo.AccountingPeriods as pe
on pe.Id = co.AccountingPeriodId 
left join dbo.Currencies as cu
on cu.Id = co.CurrencyId
left join dbo.CurrencyRates as ra
on ra.AccountingPeriodId = pe.Id and ra.FromCurrencyId = cu.Id
where 1 = 1
and ( pe.PeriodNumber = '2024-01-A' or  pe.id IS NULL)


select y.*, r.OperationLabourConsumption, r.ResourceQty as CrewSize, w.ResourceNumber, w.Cost, w.Markup
, (y.sumX * r.OperationLabourConsumption * r.ResourceQty * w.Cost) as Labour
, (y.sumX * r.OperationLabourConsumption * r.ResourceQty * w.Cost * w.Markup * 0.01) as Markup
from (
select SetProductNumber, PartProductNumber, PartProductId, SUM(sumx) as sumX
from (
select ParentId, SetCategoryNumber,  SetProductNumber, PartProductNumber, PartProductId, sum(FinalPartProductQty) as sumx  from dbo.mwtech_bom_cte(@ProductId,NULL)
where SetCategorynumber not in ('SUR','PREP','NAW','MIE-1','MIE-2','MIE-PAS','MIE-0','MON','MOD')
and PartCategoryNumber not in ('SUR','PREP','NAW','MIE-1','MIE-2','MIE-PAS','MIE-0','MON','MOD')
and HowManyParts > 0
group by ParentId, SetCategoryNumber,  SetProductNumber, PartProductNumber, PartProductId 
) as x
group by SetProductNumber, PartProductNumber, PartProductId
) as y
inner join dbo.RouteVersions as v
on v.ProductId = y.PartProductId and v.DefaultVersion = 1
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.Id
inner join dbo.Resources as w
on w.Id = r.ResourceId

select y.*, r.OperationLabourConsumption, r.ResourceQty as CrewSize, w.ResourceNumber, w.Cost, w.Markup
, (y.sumX * (1/r.OperationLabourConsumption) * r.ResourceQty * w.Cost) as Labour
, (y.sumX * (1/r.OperationLabourConsumption) * r.ResourceQty * w.Cost * w.Markup * 0.01) as Markup
from (
select SetProductNumber, PartProductNumber, PartProductId, SUM(sumx) as sumX
from (
select ParentId, SetCategoryNumber,  SetProductNumber, PartProductNumber, PartProductId, sum(FinalPartProductQty) as sumx  from dbo.mwtech_bom_cte(@ProductId,NULL)
where SetCategorynumber in ('NAW','MIE-1','MIE-2','MIE-PAS','MIE-0','MON','MOD')
and PartCategoryNumber not in ('SUR','PREP')
and HowManyParts > 0
group by ParentId, SetCategoryNumber,  SetProductNumber, PartProductNumber, PartProductId 
) as x
group by SetProductNumber, PartProductNumber, PartProductId
) as y
inner join dbo.RouteVersions as v
on v.ProductId = y.PartProductId and v.DefaultVersion = 1
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.Id
inner join dbo.Resources as w
on w.Id = r.ResourceId


select pr.ProductNumber, r.OperationLabourConsumption, r.ResourceQty as CrewSize, w.ResourceNumber, w.Cost, w.Markup
, ( r.OperationLabourConsumption * r.ResourceQty * w.Cost) as Labour
, ( r.OperationLabourConsumption * r.ResourceQty * w.Cost * w.Markup * 0.01) as Markup
from dbo.Products as pr
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id and v.DefaultVersion = 1
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.Id
inner join dbo.Resources as w
on w.Id = r.ResourceId
where pr.Id = @ProductId
