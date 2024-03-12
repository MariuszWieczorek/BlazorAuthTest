/* OPONY - Aktualizacja parametrów przezbrojenia */
/* 2023.10.23 */

update r
set 
     MoveTime = 2
 	,Overlap = 1
    ,ChangeOverResourceId =  (select Id from dbo.Resources where ResourceNumber = 'UR.WP')
	,ChangeOverLabourConsumption = 0
	,ChangeOverMachineConsumption = 0
    ,ChangeOverNumberOfEmployee = 1
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
WHERE 1 = 1
and ca.CategoryNumber IN ('OAP','OBB','OBB-TE','OBC','OBC-TE','OKA','OSM','OSM-TE') and r.OrdinalNumber = 10



update r
set 
     MoveTime = 0
 	,Overlap = 1
    ,ChangeOverResourceId =  (select Id from dbo.Resources where ResourceNumber = 'UR.WP')
	,ChangeOverLabourConsumption = 0
	,ChangeOverMachineConsumption = 0
    ,ChangeOverNumberOfEmployee = 1
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
WHERE 1 = 1
and ca.CategoryNumber IN ('OBK','OBK-TE','ODA','OKC','OKD')


update r
set 
     MoveTime = 0
 	,Overlap = 1
    ,ChangeOverResourceId =  (select Id from dbo.Resources where ResourceNumber = 'URS05')
	,ChangeOverLabourConsumption = 0.1
	,ChangeOverMachineConsumption = 0.1
    ,ChangeOverNumberOfEmployee = 1
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
WHERE 1 = 1
and ca.CategoryNumber IN ('ODR','OSU','OSU-TE','OWU','OWU-TE') and r.OrdinalNumber = 10


update r
set 
     MoveTime = 0
 	,Overlap = 1
    ,ChangeOverResourceId =  (select Id from dbo.Resources where ResourceNumber = 'PC.PO.KJ')
	,ChangeOverLabourConsumption = 0
	,ChangeOverMachineConsumption = 0
    ,ChangeOverNumberOfEmployee = 1
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
WHERE 1 = 1
and ca.CategoryNumber IN ('ODR','OSU','OSU-TE','OWU','OWU-TE') and r.OrdinalNumber = 20


update r
set 
     MoveTime = 12
 	,Overlap = 1
    ,ChangeOverResourceId =  (select Id from dbo.Resources where ResourceNumber = 'UR.WP')
	,ChangeOverLabourConsumption = 0
	,ChangeOverMachineConsumption = 0
    ,ChangeOverNumberOfEmployee = 1
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
WHERE 1 = 1
and ca.CategoryNumber IN ('OKG','OKG-TE','OTT') and r.OrdinalNumber = 10


SELECT CategoryNumber,MoveTime,Overlap,przezbrojenie_kat,czas_przezbrojenia,przezbrojenie_brygada,OrdinalNumber,ile
FROM 
( select ca.CategoryNumber
, r.MoveTime
, r.Overlap
, (select ResourceNumber from dbo.Resources where id = r.ChangeOverResourceId) as przezbrojenie_kat
, r.ChangeOverLabourConsumption as czas_przezbrojenia
, r.ChangeOverNumberOfEmployee as przezbrojenie_brygada
, count(*) as ile
,r.OrdinalNumber
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where 1=1
and ca.CategoryNumber not in ('DWA','DMA')
and ca.CategoryNumber IN 
(
 'OAP'
,'OBB'
,'OBB-TE'
,'OBC'
,'OBC-TE'
,'OBK'
,'OBK'
,'OBK-TE'
,'OBK-TE'
,'ODA'
,'ODR'
,'ODR'
,'OKA'
,'OKC'
,'OKD'
,'OKG'
,'OKG-TE'
,'OSM'
,'OSM-TE'
,'OSU'
,'OSU'
,'OSU-TE'
,'OSU-TE'
,'OTT'
,'OWU'
,'OWU'
,'OWU-TE'
,'OWU-TE'
)

-- and ca.CategoryNumber = 'OAP'
group by ca.CategoryNumber, r.MoveTime  , r.Overlap, r.ChangeOverResourceId, r.ChangeOverLabourConsumption, r.ChangeOverNumberOfEmployee, r.OrdinalNumber
) as x
order by x.CategoryNumber,  x.OrdinalNumber




