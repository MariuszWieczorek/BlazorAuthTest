-- Analiza marszrut dêtek dla tego samego weza
-- wszystkie pola

use mwtech;

select x.CategoryNumber,x.Idx02, x.AlternativeNo, x.VersionNumber, COUNT(*)
from (
select ca.CategoryNumber,pr.Idx02, v.AlternativeNo, v.VersionNumber
,r.OperationId
,r.ResourceId
,r.OrdinalNumber
,r.ResourceQty
,r.WorkCenterId
,r.OperationLabourConsumption
,r.OperationMachineConsumption
,r.ChangeOverLabourConsumption
,r.ChangeOverMachineConsumption
,r.ChangeOverNumberOfEmployee
,r.ChangeOverResourceId
-- ,r.MoveTime
-- ,r.Overlap
,r.ProductCategoryId
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
where pr.Idx02 is not null and pr.Idx02 != ''
and v.IsActive = 1
and ca.CategoryNumber in ('DOB')
group by ca.CategoryNumber,pr.Idx02, v.AlternativeNo, v.VersionNumber
,r.OperationId
,r.ResourceId
,r.OrdinalNumber
,r.ResourceQty
,r.WorkCenterId
,r.OperationLabourConsumption
,r.OperationMachineConsumption
,r.ChangeOverLabourConsumption
,r.ChangeOverMachineConsumption
,r.ChangeOverNumberOfEmployee
,r.ChangeOverResourceId
--,r.MoveTime
-- ,r.Overlap
,r.ProductCategoryId
) as x
group by x.CategoryNumber,x.Idx02, x.AlternativeNo, x.VersionNumber
having count(*) > 1

-- Analiza marszrut dêtek dla tego samego weza
-- pola z pominieciem gniazda i numerow wersji
select x.CategoryNumber,x.Idx02, COUNT(*)
from (
select ca.CategoryNumber,pr.Idx02
,r.OperationId
,r.ResourceId
,r.OrdinalNumber
,r.ResourceQty
--,r.WorkCenterId
,r.OperationLabourConsumption
,r.OperationMachineConsumption
,r.ChangeOverLabourConsumption
,r.ChangeOverMachineConsumption
,r.ChangeOverNumberOfEmployee
,r.ChangeOverResourceId
-- ,r.MoveTime
-- ,r.Overlap
,r.ProductCategoryId
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
where pr.Idx02 is not null and pr.Idx02 != ''
and ca.CategoryNumber in ('DOB')
and v.IsActive = 1
 group by 
 ca.CategoryNumber
,pr.Idx02
-- ,v.AlternativeNo
-- ,v.VersionNumber
,r.OperationId
,r.ResourceId
,r.OrdinalNumber
,r.ResourceQty
--,r.WorkCenterId
,r.OperationLabourConsumption
,r.OperationMachineConsumption
,r.ChangeOverLabourConsumption
,r.ChangeOverMachineConsumption
,r.ChangeOverNumberOfEmployee
,r.ChangeOverResourceId
--,r.MoveTime
-- ,r.Overlap
,r.ProductCategoryId
) as x
group by x.CategoryNumber,x.Idx02
having count(*) > 1

/* Wyœwietlenie indeksu gdy potrzebna analiza dlaczego duble*/
/*
select ca.CategoryNumber,pr.Idx02, v.AlternativeNo, v.VersionNumber
,pr.ProductNumber
,r.OperationId
,r.ResourceId
,r.OrdinalNumber
,r.ResourceQty
,r.WorkCenterId
,r.OperationLabourConsumption
,r.OperationMachineConsumption
,r.ChangeOverLabourConsumption
,r.ChangeOverMachineConsumption
,r.ChangeOverNumberOfEmployee
,r.ChangeOverResourceId
-- ,r.MoveTime
-- ,r.Overlap
,r.ProductCategoryId

--update r
--set OperationLabourConsumption = 0.158350, OperationMachineConsumption = 0.158350
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
where pr.Idx02 is not null and pr.Idx02 != ''
and ca.CategoryNumber in ('DWU')
and v.IsActive = 1
AND pr.Idx02 = '1691424VFI70'
*/



select 'KT1' as kt
,pr.Idx02 as ProductNumber
, '' as empty1
,wc.ResourceNumber as Description
,we.no as AlternativeNo
,5 as VersionNumber
,1 as IsActive
,r.OrdinalNumber
,op.OperationNumber
,wc.ResourceNumber as WorkCenter
,replace(r.ChangeOverMachineConsumption, '.', ',') as ChangeOverMachineConsumption
,replace(r.ChangeOverLabourConsumption, '.', ',') as ChangeOverLabourConsumption
,ch.ResourceNumber as ChangeOverResource
,replace(r.ChangeOverNumberOfEmployee, '.', ',') as ChangeOverNumberOfEmployee
,replace(r.OperationMachineConsumption, '.', ',') as OperationMachineConsumption
,replace(r.OperationLabourConsumption, '.', ',') as OperationLabourConsumption
,u.UnitCode -- Jedn	ostka
,res.ResourceNumber as Kategoria
,replace(r.ResourceQty, '.', ',') as ResourceQty
,0 as MoveTime -- ,replace(r.MoveTime, '.', ',') as MoveTime
,1 as OverLap  -- ,replace(r.Overlap, '.', ',') as Overlap
,iif(we.no = 1 , 1 , 0 ) as IsDefaultVersion
from dbo.RouteVersions as rv
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = rv.Id 
inner join dbo.Products as pr
on pr.Id = rv.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId

inner join dbo.Resources as res
on res.Id = r.ResourceId
left join dbo.Resources as ch
on ch.Id = r.ChangeOverResourceId
left join dbo.Operations as op
on op.Id = r.OperationId
left join dbo.Units as u
on u.Id = op.UnitId
--
inner join dbo.weze as we
on we.Idx02 = pr.Idx02 
and we.CategoryNumber = ca.CategoryNumber 
inner join dbo.Resources as wc
on we.WorkCenter = wc.ResourceNumber
--
Where 1 = 1
and rv.IsActive != 0	
and pr.Idx02 is not null and pr.Idx02 != ''
and we.CategoryNumber in ('DOB')
-- and we.Idx02 = '100020TR13'
group by 
 ca.CategoryNumber
,pr.Idx02
-- ,v.AlternativeNo
-- ,v.VersionNumber
,r.OperationId
,r.ResourceId
,r.OrdinalNumber
,r.ResourceQty
--,r.WorkCenterId
,r.OperationLabourConsumption
,r.OperationMachineConsumption
,r.ChangeOverLabourConsumption
,r.ChangeOverMachineConsumption
,r.ChangeOverNumberOfEmployee
,r.ChangeOverResourceId
--,r.MoveTime
-- ,r.Overlap
,r.ProductCategoryId
-- ,pr.ProductNumber
,op.OperationNumber
,res.ResourceNumber
,wc.ResourceNumber
,ch.ResourceNumber
,u.UnitCode
,we.no
Order by r.OrdinalNumber, pr.Idx02,we.no
