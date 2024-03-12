--select pr.ProductNumber, wc.ResourceNumber, crew.ResourceNumber as crew, r.ChangeOverMachineConsumption, r.ChangeOverLabourConsumption, r.ChangeOverNumberOfEmployee, chcrew.ResourceNumber as chcrew



UPDATE r
SET 
MoveTime = 0
, Overlap = 1


--    ChangeOverLabourConsumption = 0.1
--  , ChangeOverMachineConsumption = 0.1
--  , ChangeOverNumberOfEmployee = 1
--  , ChangeOverResourceId = (select Id from dbo.Resources where ResourceNumber = 'PC.PD.WYT')
 --  , ChangeOverResourceId = ResourceId


from dbo.Products as pr
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Resources as wc
on wc.Id = r.WorkCenterId
inner join dbo.Resources as chcrew
on chcrew.Id = ChangeOverResourceId  
inner join dbo.Resources as crew
on crew.Id = r.ResourceId
where 1 = 1
AND ca.CategoryNumber = 'DMA'
AND wc.ResourceNumber like 'PA%' 
-- AND wc.ResourceNumber not like 'AP%' 


SELECT wc.ResourceNumber as WorkCenter
,crew.ResourceNumber as LaborClass 
,r.ChangeOverLabourConsumption
,r.ChangeOverMachineConsumption
,chcrew.ResourceNumber as SetupClass
,r.ChangeOverNumberOfEmployee
,r.MoveTime
,r.Overlap
, COUNT(*) as ile
from dbo.Products as pr
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Resources as wc
on wc.Id = r.WorkCenterId
inner join dbo.Resources as chcrew
on chcrew.Id = ChangeOverResourceId  
inner join dbo.Resources as crew
on crew.Id = r.ResourceId
where 1 = 1
AND ca.CategoryNumber = 'DMA'
AND wc.ResourceNumber like 'PA%' 
--AND v.IsActive = 1
GROUP BY wc.ResourceNumber,crew.ResourceNumber ,r.ChangeOverLabourConsumption, r.ChangeOverMachineConsumption,chcrew.ResourceNumber, r.ChangeOverNumberOfEmployee
,r.MoveTime
,r.Overlap
ORDER BY wc.ResourceNumber,crew.ResourceNumber


/*
Parametry przezbrojenia 
GNIAZDO		CZAS	KATEGORIA	BRYGADA			MT	OV
		
C%	    	0,1	0,1	URS04		2				0	1
A%	!= AP%  0,1	0,1	URS04		2				0	1
B%	    	0,1	0,1	URS04		2				0	1
D%	    	0,1	0,1	URS04		2				0	1
WAZ01		0,1	0,1	URKT2		2				0	1
WF17		0,1	0,1	URS03		2				0	1
LWD%		0,1	0,1	PC.PD.WYT	1				4	0
KJDE%		0	0	PC.PD.KJ	1				0	1
PA0%		0	0	KP.MA.PAK	1				0	1	?	


OB%			0	0	*KAT_WYK	1				0	1	
AP%			0	0	*KAT_WYK	1				0	1
ST%			0	0	*KAT_WYK	1				1	0

* KAT_WYK - KATEGORIA taka jak w wykonaniu
*/

