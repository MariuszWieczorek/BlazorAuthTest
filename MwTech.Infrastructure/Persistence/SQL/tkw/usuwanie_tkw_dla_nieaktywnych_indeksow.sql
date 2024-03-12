
use mwtech;
declare @PeriodId as int;

set @PeriodId = 25;


select ca.CategoryNumber, pr.ProductNumber, co.Cost, co.CalculatedDate
-- delete co
FROM dbo.ProductCosts as co
inner join dbo.Products as pr
on pr.Id = co.ProductId
inner join dbo.ProductCategories as ca
on ca.id = pr.ProductCategoryId
WHERE co.AccountingPeriodId = @PeriodId
and pr.IsActive = 0
-- order by ca.CategoryNumber, pr.ProductNumber

