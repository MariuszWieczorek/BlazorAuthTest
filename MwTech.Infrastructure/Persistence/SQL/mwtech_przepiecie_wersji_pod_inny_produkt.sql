select *
from dbo.RouteVersions as v
where productId=240


select *
from dbo.ManufactoringRoutes as v
where  RouteVersionId=22159

/*
update v
set productId = 240
from dbo.RouteVersions as v
where productId=20162


update v
set SetId = 240
from dbo.Boms as v
where SetId=20162 and SetVersionId=22159
*/