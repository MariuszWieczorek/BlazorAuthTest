use MwTech;

-- select pr.ProductNumber, v.VersionNumber, v.AlternativeNo
 update r
-- set ToolQuantity = 1, RoutingToolId = (select t.Id from dbo.RoutingTools as t where t.ToolNumber = 'FODE-15516514')
set RoutingToolId = (select t.Id from dbo.RoutingTools as t where t.ToolNumber = 'FODE-15516514')
from dbo.Products as pr
inner join dbo.RouteVersions as v
on v.ProductId = pr.Id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.Id
inner join dbo.ProductCategories as ca
on ca.Id = v.ProductCategoryId
where 1 = 1
and v.IsActive = 1
and pr.ProductNumber = '15516514TR13'
and ca.CategoryNumber = 'DWU'

