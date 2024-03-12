use mwtech;

with rout as
(
select r.WorkCenterId, wc.ResourceNumber as wc, r.ResourceId, ka.ResourceNumber as ka
from dbo.ManufactoringRoutes as r
inner join dbo.Resources as wc
on wc.id = r.WorkCenterId
inner join dbo.Resources as ka
on ka.id = r.ResourceId
group by r.WorkCenterId, r.ResourceId, wc.ResourceNumber, ka.ResourceNumber

),

Two as
(
select WorkCenterId, count(*) as ile
from rout
group by WorkCenterId
having count(*) > 1
),


One as
(
select WorkCenterId, count(*) as ile
from rout
group by WorkCenterId
having count(*) > 1
)

select * 
from rout as r
inner join One as t
on r.WorkCenterId = t.WorkCenterId
where r.wc like 'LM%'
order by r.wc




