use MwTech;
/*
select pr.ProductNumber, v.VersionNumber, v.AlternativeNo,  o.OperationNumber, r.OperationMachineConsumption, r.OperationLabourConsumption
,(select xr.OperationMachineConsumption
	from dbo.ManufactoringRoutes as xr
	inner join dbo.Operations as xo 
	on xo.Id = xr.OperationId 
	where xr. RouteVersionId = v.Id 
	and xo.OperationNumber = 'PO.100.10_WULKANIZACJA' ) as NewOperationMachineConsumption
*/	

update r
SET OperationMachineConsumption =
(select xr.OperationMachineConsumption
	from dbo.ManufactoringRoutes as xr
	inner join dbo.Operations as xo 
	on xo.Id = xr.OperationId 
	where xr. RouteVersionId = v.Id 
	and xo.OperationNumber = 'PO.100.10_WULKANIZACJA' )

from dbo.Products as pr
inner join dbo. ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.Id
inner join dbo.Resources as wc
on wc.Id = r.WorkCenterId
inner join dbo.Operations as o
on o.Id = r.OperationId
where ca.CategoryNumber = 'OWU-TE'
and o.OperationNumber = 'PO.100.20_KONTROLA_JAKOSCI'
and v.IsActive = 1
--and pr.ProductNumber = 'KOGT1834080121L'