/* WYT£ACZANE i FORMOWANE - Aktualizacja parametrów przezbrojenia */
/* 2023.10.23 */

update r
set 
     MoveTime = 0
 	,Overlap = 1
	,ChangeOverLabourConsumption = 0
	,ChangeOverMachineConsumption = 0
    ,ChangeOverResourceId =  null
    ,ChangeOverNumberOfEmployee = 0
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
and ca.CategoryNumber = 'ZAW-P'

update r
set 
     MoveTime = 0.1666
 	,Overlap = 0
	,ChangeOverLabourConsumption = 0
	,ChangeOverMachineConsumption = 0
    ,ChangeOverResourceId =  (select Id from dbo.Resources where ResourceNumber = 'PC.PD.MAL')
    ,ChangeOverNumberOfEmployee = 1
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
and ca.CategoryNumber = 'ZAW-M'

update r
set 
     MoveTime = 0
 	,Overlap = 1
	,ChangeOverLabourConsumption = 0
	,ChangeOverMachineConsumption = 0
    ,ChangeOverResourceId =  (select Id from dbo.Resources where ResourceNumber = 'PC.BO.POB')
    ,ChangeOverNumberOfEmployee = 1
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
and ca.CategoryNumber = 'BOL-REC'

update r
set 
     MoveTime = 0
 	,Overlap = 1
	,ChangeOverLabourConsumption = 0.1
	,ChangeOverMachineConsumption = 0.1
    ,ChangeOverResourceId =  (select Id from dbo.Resources where ResourceNumber = 'UR.BO3')
    ,ChangeOverNumberOfEmployee = 2
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
and ca.CategoryNumber IN (
 'USL-BOL'
,'WF-AKC-B'
)

update r
set 
     MoveTime = 0
 	,Overlap = 1
	,ChangeOverLabourConsumption = 0.1
	,ChangeOverMachineConsumption = 0.1
    ,ChangeOverResourceId =  (select Id from dbo.Resources where ResourceNumber = 'UR.BO2')
    ,ChangeOverNumberOfEmployee = 2
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
and ca.CategoryNumber IN (
 'WF-AKL'
)

update r
set 
     MoveTime = 0
 	,Overlap = 1
	,ChangeOverLabourConsumption = 0.1
	,ChangeOverMachineConsumption = 0.1
    ,ChangeOverResourceId =  (select Id from dbo.Resources where ResourceNumber = 'UR.BO2')
    ,ChangeOverNumberOfEmployee = 2
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
and ca.CategoryNumber IN (
 'WW-AKL'
)

update r
set 
     MoveTime = 0
 	,Overlap = 1
	,ChangeOverLabourConsumption = 0.1
	,ChangeOverMachineConsumption = 0.1
    ,ChangeOverResourceId =  (select Id from dbo.Resources where ResourceNumber = 'PC.PW.WW')
    ,ChangeOverNumberOfEmployee = 1
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
and ca.CategoryNumber IN (
 'WW-ORN'
,'WW-PPROF'
,'WW-PROF'
)

update r
set 
     MoveTime = 0
 	,Overlap = 1
	,ChangeOverLabourConsumption = 0.1
	,ChangeOverMachineConsumption = 0.1
    ,ChangeOverResourceId =  (select Id from dbo.Resources where ResourceNumber = 'URS02')
    ,ChangeOverNumberOfEmployee = 2
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
and ca.CategoryNumber IN (
 'WF-AKC'
,'USL-WF'
)

update r
set 
     MoveTime = 0
 	,Overlap = 1
	,ChangeOverLabourConsumption = 0.1
	,ChangeOverMachineConsumption = 0.1
    ,ChangeOverResourceId =  (select Id from dbo.Resources where ResourceNumber = 'PC.PW.WW')
    ,ChangeOverNumberOfEmployee = 1
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
and ca.CategoryNumber IN (
 'WW-ORN'
,'WW-PPROF'
,'WW-PROF'
)


update r
set 
     MoveTime = 0
 	,Overlap = 1
	,ChangeOverLabourConsumption = 0
	,ChangeOverMachineConsumption = 0
    ,ChangeOverResourceId =  (select Id from dbo.Resources where ResourceNumber = 'KP.MA.PAK')
    ,ChangeOverNumberOfEmployee = 1
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
and ca.CategoryNumber = 'WF-AKC-POZ'

update r
set 
     MoveTime = 0
 	,Overlap = 1
	,ChangeOverLabourConsumption = 0
	,ChangeOverMachineConsumption = 0
    ,ChangeOverResourceId =  (select Id from dbo.Resources where ResourceNumber = 'PC.PDKON75')
    ,ChangeOverNumberOfEmployee = 1
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
and ca.CategoryNumber = 'WOK'

update r
set 
     MoveTime = 0
 	,Overlap = 1
	,ChangeOverLabourConsumption = 0
	,ChangeOverMachineConsumption = 0
    ,ChangeOverResourceId =  (select Id from dbo.Resources where ResourceNumber = 'PC.BO.POB')
    ,ChangeOverNumberOfEmployee = 1
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
and ca.CategoryNumber = 'WOK-BOL'


SELECT CategoryNumber,MoveTime,Overlap,przezbrojenie_kat,czas_przezbrojenia,przezbrojenie_brygada
FROM 
( select ca.CategoryNumber
, r.MoveTime
, r.Overlap
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
and ca.CategoryNumber not in ('DWA','DMA')
and ca.CategoryNumber IN (
 'BOL-REC'
,'USL-BOL'
,'USL-WF'
,'WF-AKC'
,'WF-AKC-B'
,'WF-AKC-POZ'
,'WOK'
,'WOK-BOL'
,'WW-AKL'
,'WW-ORN'
,'WW-PPROF'
,'WW-PROF'
,'ZAW-M'
,'ZAW-P'
)

-- and ca.CategoryNumber = 'OAP'
group by ca.CategoryNumber, r.MoveTime  , r.Overlap, r.ChangeOverResourceId, r.ChangeOverLabourConsumption, r.ChangeOverNumberOfEmployee
) as x
order by x.CategoryNumber, x.przezbrojenie_kat




