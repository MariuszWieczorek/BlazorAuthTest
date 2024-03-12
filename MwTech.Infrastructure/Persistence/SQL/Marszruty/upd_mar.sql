/*
update dbo.Resources
set  Markup = 2	
where EstimatedMarkup = 1.75
*/

select r.ResourceNumber, r.Name 
, r.EstimatedMarkup as stary_narzut
,r.Markup as nowy_narzut
from dbo.Resources as r
where exists (select * from dbo.ManufactoringRoutes where ResourceId = r.id)
and r.ResourceNumber not in ('LM3','XL.MIKSER')
order by r.ResourceNumber

