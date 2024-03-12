/* Wyszukiwanie zdublowwanych rekordów z wyliczonym kosztem */
/* Zamieniamy Select Na DELETE i usuwamy zbêdne rekordy */

use mwtech;


select x.ProductNumber, cox.*
-- DELETE cox
from dbo.ProductCosts as cox
inner join (
select  co.ProductId, pr.ProductNumber, co.AccountingPeriodId, COUNT(*) as ile, max(co.CreatedDate) as mindate
from dbo.Products as pr
inner join dbo.ProductCosts as co
on co.ProductId = pr.Id
group by co.ProductId, pr.ProductNumber, co.AccountingPeriodId
having COUNT(*) > 1 ) as x
on x.AccountingPeriodId = cox.AccountingPeriodId
and x.ProductId = cox.ProductId
and cox.CreatedDate = x.mindate

