
update r
set 
     MoveTime = 0
 	,Overlap = 1
	,ChangeOverLabourConsumption = 1
	,ChangeOverMachineConsumption = 1
    ,ChangeOverResourceId =  (select Id from dbo.Resources where ResourceNumber = 'UR.BO2')
    ,ChangeOverNumberOfEmployee = 2
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
and ca.CategoryNumber = 'BOL-OPO-ZIM'
and r.OrdinalNumber = 70




select ca.CategoryNumber, r.OrdinalNumber
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
-- and ca.CategoryNumber = 'OAP'
and ca.CategoryNumber IN (
 'BOL-BIE-WUZ'
,'BOL-BIE-WYG'
,'BOL-BIE-WYZ'
,'BOL-OPO-GOR'
,'BOL-OPO-ZIM'
,'BOL-SOLID-PP'
,'BOL-SOLID-SK1'
,'BOL-SOLID-SK2'
,'BOL-SOLID-SK3'
,'BOL-SOLID-SKR'
,'BOL-SOLID-WOS1'
,'BOL-SOLID-WOS2'
)

-- and( round(r.ChangeOverLabourConsumption,3) = 0.043 or round(r.ChangeOverLabourConsumption,3) = 0.042)
-- and MoveTime > 20
group by ca.CategoryNumber,  r.MoveTime  , r.Overlap, r.ChangeOverResourceId, r.ChangeOverLabourConsumption, r.ChangeOverNumberOfEmployee, r.OrdinalNumber
order by ca.CategoryNumber, r.OrdinalNumber

/*
select pr.ProductNumber, v.VersionNumber, v.AlternativeNo, ca.CategoryNumber, r.OperationId
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
and ca.CategoryNumber = 'BOL-OPO-GOR'
and r.OrdinalNumber = 90
*/
