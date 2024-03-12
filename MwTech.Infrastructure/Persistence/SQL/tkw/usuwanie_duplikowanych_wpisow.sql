select *
from dbo.ProductCosts 
where 1 = 1
and AccountingPeriodId = 23
and ProductId in 
(
select ProductId
from dbo.ProductCosts 
where 1 = 1
and AccountingPeriodId = 23
group by ProductId
having count(*) > 1
)

delete from dbo.ProductCosts where id = 862360