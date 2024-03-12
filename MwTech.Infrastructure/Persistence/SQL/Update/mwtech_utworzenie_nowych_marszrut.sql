select 
'KT1' AS um
,pr.ProductNumber 
,rv.VersionNumber
,rv.Name as versionName
,r.OrdinalNumber
,o.OperationNumber
,w.ResourceNumber
,r.ChangeOverMachineConsumption
,r.ChangeOverLabourConsumption
,ch.ResourceNumber
,0 as ChangeOverNumberOfEmployee
--
,r.OperationMachineConsumption
,r.OperationLabourConsumption
,'Godz./jedn.' as jm
,oo.ResourceNumber
,r.ResourceQty
,r.MoveTime
,r.Overlap
,'x'
,'y'
,rv.DefaultVersion
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId

--
inner join dbo.RouteVersions as rv
on rv.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = rv.id
inner join dbo.Operations as o
on o.Id = r.OperationId
inner join dbo.Resources as w
on w.Id = r.WorkCenterId
left join dbo.Resources as ch
on ch.Id = r.ChangeOverResourceId
inner join dbo.Resources as oo
on oo.Id = r.ResourceId
where ca.CategoryNumber = 'DET-LUZ'


/*
delete rv
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId

--
inner join dbo.RouteVersions as rv
on rv.ProductId = pr.Id
where ca.CategoryNumber = 'DET-LUZ'
*/