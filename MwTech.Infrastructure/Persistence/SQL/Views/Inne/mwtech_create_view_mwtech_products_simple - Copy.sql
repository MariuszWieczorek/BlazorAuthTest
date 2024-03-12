/* Export operacji marszrut do IFS */
CREATE OR ALTER VIEW mwtech_products_versions_test
AS
(
select pr.Id
, pr.ProductNumber
, pr.Name as ProductName
, ca.CategoryNumber
, ca.Name as CategoryName
, u.UnitCode
,(select COUNT(*) from dbo.ProductVersions as vx where vx.ProductId = pr.Id and vx.ToIfs = 1) as total_product_versions
,(select COUNT(*) from dbo.ProductVersions as vx where vx.ProductId = pr.Id and vx.ToIfs = 1 and vx.DefaultVersion = 1) as default_product_versions
,(select COUNT(*) from dbo.ProductVersions as vx inner join dbo.Boms as bx on bx.SetVersionId = vx.id where vx.ProductId = pr.Id and vx.ToIfs = 1 and vx.DefaultVersion = 1) as default_product_version_positions
,(select COUNT(*) from dbo.RouteVersions as vx where vx.ProductId = pr.Id and vx.ToIfs = 1) as total_route_versions
,(select COUNT(*) from dbo.RouteVersions as vx where vx.ProductId = pr.Id and vx.ToIfs = 1 and vx.DefaultVersion = 1) as default_route_versions
,(select COUNT(*) from dbo.RouteVersions as vx inner join dbo.ManufactoringRoutes as bx on bx.RouteVersionId = vx.id where vx.ProductId = pr.Id and vx.ToIfs = 1 and vx.DefaultVersion = 1) as default_route_version_positions
, GETDATE() as ExportDate
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.Units as u
on u.Id = pr.UnitId	
)

-- select * from mwtech_route_ifs order by SetCategory, nr_pozycji, numer_operacji
-- select * from mwtech_bom_ifs order by SetCategory, nr_pozycji_nadrzednej, numer_pozycji_w_linii


