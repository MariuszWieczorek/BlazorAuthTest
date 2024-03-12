-- Ustawienie dêtek jako klienckie
use mwtech;

select ProductNumber,Client
from dbo.Products as p
inner join dbo.ProductCategories as ca
on ca.Id = p.ProductCategoryId
where ca.CategoryNumber in ('DWY','DOB','DAP','DST','DWU','DKJ','DET')
and 
not (p.ProductNumber like '%KB%'
or p.ProductNumber like '%KBWK%'
or p.ProductNumber like 'KBK'
)


update p
set Client = 1
from dbo.Products as p
inner join dbo.ProductCategories as ca
on ca.Id = p.ProductCategoryId
where ca.CategoryNumber in ('DWY','DOB','DAP','DST','DWU','DKJ','DET')
and 
not (p.ProductNumber like '%KB%'
or p.ProductNumber like '%KBWK%'
or p.ProductNumber like 'KBK'
)

