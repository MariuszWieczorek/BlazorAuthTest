/*
update r
set 
--    MoveTime = 24.01
--	 Overlap = 0 
--	 ChangeOverLabourConsumption = 0.043
--	,ChangeOverMachineConsumption = 0.043
--    ,ChangeOverResourceId = (select Id from dbo.Resources where ResourceNumber = 'UR.BO3')
     ChangeOverNumberOfEmployee = 0
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where 1=1
and ca.CategoryNumber not in ('DWA','DMA')
and ( ca.CategoryNumber like 'MIE%' OR ca.CategoryNumber IN ('NAW','MON','MOP','MZE'))
-- and( round(r.ChangeOverLabourConsumption,3) = 0.043 or round(r.ChangeOverLabourConsumption,3) = 0.042)
-- and MoveTime > 20
and r.ChangeOverResourceId is null
*/



select ca.CategoryNumber
, r.MoveTime  as czas_transportu
, r.Overlap as zachodzenie
, (select ResourceNumber from dbo.Resources where id = r.ChangeOverResourceId) as przezbrojenie_kat
, r.ChangeOverLabourConsumption as czas_przezbrojenia
, r.ChangeOverNumberOfEmployee as przezbrojenie_brygada
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where 1=1
and v.IsActive = 1
and pr.IsActive = 1
and ca.CategoryNumber not in ('DWA','DMA')
and ( ca.CategoryNumber like 'MIE%' OR ca.CategoryNumber IN ('NAW','MON','MOP','MZE'))
-- and( round(r.ChangeOverLabourConsumption,3) = 0.043 or round(r.ChangeOverLabourConsumption,3) = 0.042)
-- and MoveTime > 20
group by ca.CategoryNumber,  r.MoveTime  , r.Overlap, r.ChangeOverResourceId, r.ChangeOverLabourConsumption, r.ChangeOverNumberOfEmployee
order by ca.CategoryNumber

/*
select pr.ProductNumber, v.VersionNumber, v.AlternativeNo, ca.CategoryNumber
, r.MoveTime  as czas_transportu
, r.Overlap as zachodzenie
, (select ResourceNumber from dbo.Resources where id = r.ChangeOverResourceId) as przezbrojenie_kat
, r.ChangeOverLabourConsumption as czas_przezbrojenia
, r.ChangeOverNumberOfEmployee as przezbrojenie_brygada
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where 1=1
and v.IsActive = 1
and pr.IsActive = 1
and ca.CategoryNumber not in ('DWA','DMA')
and ( ca.CategoryNumber like 'MIE%' OR ca.CategoryNumber IN ('NAW','MON','MOP','MZE'))
-- and( round(r.ChangeOverLabourConsumption,3) = 0.043 or round(r.ChangeOverLabourConsumption,3) = 0.042)
-- and MoveTime > 20
and r.ChangeOverResourceId is null
*/
