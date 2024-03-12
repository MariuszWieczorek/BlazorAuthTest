/* Export operacji marszrut do IFS */
CREATE OR ALTER VIEW mwtech_products_simple
AS
(
select pr.Id
, pr.ProductNumber
, pr.Name as ProductName
, ca.CategoryNumber
, ca.Name as CategoryName
, u.UnitCode
, pr.OldProductNumber
, pr.Idx01
, pr.Idx02
, pr.IsTest
, pr.IsActive
, GETDATE() as ExportDate
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.Units as u
on u.Id = pr.UnitId
)

-- select * from mwtech_route_ifs order by SetCategory, nr_pozycji, numer_operacji
-- select * from mwtech_bom_ifs order by SetCategory, nr_pozycji_nadrzednej, numer_pozycji_w_linii


