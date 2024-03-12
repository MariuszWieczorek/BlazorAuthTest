
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
AND (select ResourceNumber from dbo.Resources where id = r.ChangeOverResourceId) IN ('URKT2', 'UR.BO3')

-- URKT2

update r
set 
    ChangeOverResourceId =  (select Id from dbo.Resources where ResourceNumber = 'URKT2')
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
AND (select ResourceNumber from dbo.Resources where id = r.ChangeOverResourceId) IN ('UR.BO2', 'UR.BO3')