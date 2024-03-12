use MwTech;

select ca.CategoryNumber, COUNT(*) as ile, COUNT(co.Cost) as przeliczone
,SUM(IIF(co.Cost>0,1,0)) as niezerowe
,SUM(IIF(co.Cost=0,1,0)) as zerowe
-- ,(CAST(ROUND(COUNT(co.Cost)/COUNT(*),3) as numeric(10,4)))*100 as procent
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
left join dbo.ProductCosts as co
on co.ProductId = pr.Id and co.AccountingPeriodId = 27
where 1 = 1
-- ca.CategoryNumber  like 'D%'
-- in ('DWY','DOB','DAP','DST','DWU','DKJ')
-- and ca.CategoryNumber not in ('DWA','DMA')
-- and co.Cost is not null
and pr.IsActive = 1
group by ca.CategoryNumber
order by ca.CategoryNumber


select ca.CategoryNumber, pr.ProductNumber, pr.Name,
ROUND(co.LabourCost,3) as LabourCost,
ROUND(co.MaterialCost,3) as MaterialCost,
ROUND(co.Cost,3) as TotalCost,
co.CalculatedDate,co.ModifiedDate,co.ImportedDate
-- ,dbo.getProductWeight(pr.Id) as waga
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
left join dbo.ProductCosts as co
on co.ProductId = pr.Id and co.AccountingPeriodId = 27
where 1 = 1
-- ca.CategoryNumber  like 'D%'
-- in ('DWY','DOB','DAP','DST','DWU','DKJ')
-- and ca.CategoryNumber not in ('DWA','DMA')
-- and co.Cost is not null
-- and ca.CategoryNumber in ('OKS')
and co.Cost > 0
and pr.IsActive = 1
order by ca.CategoryNumber, pr.ProductNumber
