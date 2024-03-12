use mwtech;

declare @period as int 
set @period = 20

select *
from dbo.ProductCosts as pc
where pc.AccountingPeriodId = @period

select * 
from dbo.AccountingPeriods as ac
where id = @period


delete pc
from dbo.ProductCosts as pc
where pc.AccountingPeriodId = @period

delete cu
from dbo.CurrencyRates as cu
where cu.AccountingPeriodId = @period

delete ac
from dbo.AccountingPeriods as ac
where id = @period
