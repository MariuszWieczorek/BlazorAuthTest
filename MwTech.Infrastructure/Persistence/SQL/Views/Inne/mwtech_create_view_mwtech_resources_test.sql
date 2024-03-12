CREATE OR ALTER VIEW mwtech_resources_test
AS
(
select re.ResourceNumber, re.Name
,(select COUNT(*) from dbo.ManufactoringRoutes as r where r.WorkCenterId = re.id) as WorkCenters
,(select COUNT(*) from dbo.ManufactoringRoutes as r where r.ResourceId = re.id) as Resources
,(select COUNT(*) from dbo.ManufactoringRoutes as r where r.ChangeOverResourceId = re.id) as ChangeOverResources
from dbo.Resources as re
)
