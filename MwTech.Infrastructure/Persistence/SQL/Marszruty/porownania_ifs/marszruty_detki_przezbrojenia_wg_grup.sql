/* Uaktualnienie parametrów przezbrojeñ dla dêtek */
/* 2023.10.23 */

use MwTech;

update r
set MoveTime = 0
, Overlap = 1 
, ChangeOverLabourConsumption = 0
, ChangeOverMachineConsumption = 0
-- , ChangeOverResourceId = (select Id from dbo.Resources where ResourceNumber = 'UR.BO2')
, ChangeOverResourceId = ResourceId
, ChangeOverNumberOfEmployee = 1
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.ProductCategories as ca1
on ca1.Id = v.ProductCategoryId
where 1=1
and ca.CategoryNumber = 'DWA'
and ca1.CategoryNumber in ('DOB')


update r
set MoveTime = 4
, Overlap = 0 
, ChangeOverLabourConsumption = 0.1
, ChangeOverMachineConsumption = 0.1
-- , ChangeOverResourceId = (select Id from dbo.Resources where ResourceNumber = 'UR.BO2')
, ChangeOverResourceId = ResourceId
, ChangeOverNumberOfEmployee = 1
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.ProductCategories as ca1
on ca1.Id = v.ProductCategoryId
where 1=1
and ca.CategoryNumber = 'DWA'
and ca1.CategoryNumber in ('DWY')

update r
set MoveTime = 0
, Overlap = 1 
, ChangeOverLabourConsumption = 0
, ChangeOverMachineConsumption = 0
-- , ChangeOverResourceId = (select Id from dbo.Resources where ResourceNumber = 'UR.BO2')
, ChangeOverResourceId = ResourceId
, ChangeOverNumberOfEmployee = 1
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.ProductCategories as ca1
on ca1.Id = v.ProductCategoryId
where 1=1
and ca.CategoryNumber = 'DMA'
and ca1.CategoryNumber in ('DAP')


update r
set MoveTime = 0
, Overlap = 1 
, ChangeOverLabourConsumption = 0
, ChangeOverMachineConsumption = 0
, ChangeOverResourceId = (select Id from dbo.Resources where ResourceNumber = 'KP.MA.PAK')
, ChangeOverNumberOfEmployee = 1
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.ProductCategories as ca1
on ca1.Id = v.ProductCategoryId
where 1=1
and ca.CategoryNumber = 'DMA'
and ca1.CategoryNumber in ('DET','DET-B','DET-KBK')

update r
set MoveTime = 0
, Overlap = 1 
, ChangeOverLabourConsumption = 0
, ChangeOverMachineConsumption = 0
, ChangeOverResourceId = (select Id from dbo.Resources where ResourceNumber = 'PC.PD.KJ')
, ChangeOverNumberOfEmployee = 1
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.ProductCategories as ca1
on ca1.Id = v.ProductCategoryId
where 1=1
and ca.CategoryNumber = 'DMA'
and ca1.CategoryNumber in ('DET-LUZ')


update r
set MoveTime = 0
, Overlap = 1 
, ChangeOverLabourConsumption = 0
, ChangeOverMachineConsumption = 0
, ChangeOverResourceId = (select Id from dbo.Resources where ResourceNumber = 'PC.PD.KJ')
, ChangeOverNumberOfEmployee = 1
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.ProductCategories as ca1
on ca1.Id = v.ProductCategoryId
where 1=1
and ca.CategoryNumber = 'DMA'
and ca1.CategoryNumber in ('DKJ','DKJ-B')


update r
set MoveTime = 1
, Overlap = 0 
, ChangeOverLabourConsumption = 0
, ChangeOverMachineConsumption = 0
, ChangeOverResourceId = ResourceId
, ChangeOverNumberOfEmployee = 1
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.ProductCategories as ca1
on ca1.Id = v.ProductCategoryId
where 1=1
and ca.CategoryNumber = 'DMA'
and ca1.CategoryNumber in ('DST')

update r
set MoveTime = 0
, Overlap = 1 
, ChangeOverLabourConsumption = 0.1
, ChangeOverMachineConsumption = 0.1
, ChangeOverResourceId = (select Id from dbo.Resources where ResourceNumber = 'URS04')
, ChangeOverNumberOfEmployee = 2
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.ProductCategories as ca1
on ca1.Id = v.ProductCategoryId
where 1=1
and ca.CategoryNumber = 'DMA'
and ca1.CategoryNumber in ('DWU')

update r
set MoveTime = 0
, Overlap = 1 
, ChangeOverLabourConsumption = 0.1
, ChangeOverMachineConsumption = 0.1
, ChangeOverResourceId = (select Id from dbo.Resources where ResourceNumber = 'URKT2')
, ChangeOverNumberOfEmployee = 2
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.ProductCategories as ca1
on ca1.Id = v.ProductCategoryId
where 1=1
and ca.CategoryNumber = 'DMA'
and ca1.CategoryNumber in ('DWU-B')



SELECT Grupa,CategoryNumber,MoveTime,Overlap,przezbrojenie_kat,czas_przezbrojenia,przezbrojenie_brygada, ile
FROM 
( select ca.CategoryNumber as Grupa, ca1.CategoryNumber
, r.MoveTime
, r.Overlap
, (select ResourceNumber from dbo.Resources where id = r.ChangeOverResourceId) as przezbrojenie_kat
, r.ChangeOverLabourConsumption as czas_przezbrojenia
, r.ChangeOverNumberOfEmployee as przezbrojenie_brygada
,count(*) as ile
from dbo.RouteVersions as v
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Products as pr
on pr.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.ProductCategories as ca1
on ca1.Id = v.ProductCategoryId
where 1=1
and ca.CategoryNumber in ('DWA','DMA')
-- and ca1.CategoryNumber like 'DKJ%'
group by ca.CategoryNumber, ca1.CategoryNumber, r.MoveTime  , r.Overlap, r.ChangeOverResourceId, r.ChangeOverLabourConsumption, r.ChangeOverNumberOfEmployee
) as x
order by x.Grupa, x.CategoryNumber, x.przezbrojenie_kat

