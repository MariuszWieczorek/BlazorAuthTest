/* Export operacji marszrut do IFS */
CREATE OR ALTER VIEW mwtech_route_ifs_headers
AS
(
SELECT 
  p.ProductNumber AS PART_NO
, iif(rw.DefaultVersion = 1, '*', cast(rw.alternativeNo as varchar(5)) ) as ALTERNATIVE_NO
, rw.name  as ALTERNATIVE_DESCRIPTION
, CAST('Produkcja' as varchar(15)) as BOM_TYPE
, CAST('M' as varchar(1)) as BOM_TYPE_DB
, 'KT1' as CONTRACT
, rw.versionNumber as ROUTING_REVISION
-- Dodatkowe
, rw.versionNumber as versionNo
, rw.alternativeNo
, rw.DefaultVersion
, rw.Id as RouteVersionId
, pc.CategoryNumber as ProductCategory
, GETDATE() as ExportDate
--
from MwTech.dbo.Products as p
inner join MwTech.dbo.ProductCategories as pc
on pc.Id = p.ProductCategoryId
--
inner join MwTech.dbo.RouteVersions as rw
on rw.ProductId = p.Id
--
where 1 = 1
and pc.RouteSource = 0
and p.IsActive = 1
and rw.IsActive = 1
and p.IsTest = 0
)