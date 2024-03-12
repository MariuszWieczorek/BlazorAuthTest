select p.ProductNumber, v.VersionNumber, v.id, v.Name 
,count(r.id) as ile
from dbo.RouteVersions as v
inner join dbo.Products as p
on p.id = v.ProductId
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.ProductCategories as ca
on ca.id = p.ProductCategoryId
where ca.CategoryNumber = 'NAW'
group by  p.ProductNumber, v.VersionNumber, v.id, v.Name
having count(r.id) > 1

select p.ProductNumber, v.VersionNumber, v.id, v.Name 
,re.ResourceNumber
,replace(v.VersionNumber,'_LM5/M','') as xxx
from dbo.RouteVersions as v
inner join dbo.Products as p
on p.id = v.ProductId
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.ProductCategories as ca
on ca.id = p.ProductCategoryId
inner join dbo.Resources as re
on re.id = r.WorkCenterId
where ca.CategoryNumber = 'NAW' and v.VersionNumber = 1

/*
update v
set Name = trim(v.name) + '_' + trim(re.ResourceNumber)
from dbo.RouteVersions as v
inner join dbo.Products as p
on p.id = v.ProductId
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.ProductCategories as ca
on ca.id = p.ProductCategoryId
inner join dbo.Resources as re
on re.id = r.WorkCenterId
where ca.CategoryNumber = 'NAW' and v.VersionNumber = 1
*/


select x.ProductNumber, x.ile
from (
select p.ProductNumber
,count(v.id) as ile
from dbo.RouteVersions as v
inner join dbo.Products as p
on p.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.id = p.ProductCategoryId
where ca.CategoryNumber = 'NAW'
group by  p.ProductNumber
-- having count(v.id) > 1
) as x
order by x.ile, x.ProductNumber


select x.ProductNumber, x.WordCenterName, x.ResourceId, x.OperationLabourConsumption, x.OperationMachineConsumption, ile
from (
select p.ProductNumber
,re.ResourceNumber as WordCenterName, r.ResourceId, r.OperationLabourConsumption, r.OperationMachineConsumption
,count(v.id) as ile
from dbo.RouteVersions as v
inner join dbo.Products as p
on p.id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.id = p.ProductCategoryId
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join dbo.Resources as re
on re.id = r.WorkCenterId
where ca.CategoryNumber = 'NAW'
group by  p.ProductNumber, re.ResourceNumber, r.ResourceId, r.OperationLabourConsumption, r.OperationMachineConsumption
-- having count(v.id) > 1
) as x
order by x.ile, x.ProductNumber
