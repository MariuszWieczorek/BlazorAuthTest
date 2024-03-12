/* Export operacji marszrut do IFS */
CREATE OR ALTER VIEW mwtech_route_ifs_idx01_headers
AS
(
select 
  p.ProductNumber as PART_NO
, iif(rw.DefaultVersion = 1, '*', cast(rw.alternativeNo as varchar(5)) ) as ALTERNATIVE_NO
, rw.name  as ALTERNATIVE_DESCRIPTION
, CAST('Produkcja' as varchar(15)) as BOM_TYPE
, CAST('M' as varchar(1)) as BOM_TYPE_DB
, 'KT1' as CONTRACT
, rw.versionNumber as ROUTING_REVISION
-- Dodatkowe
, pc.CategoryNumber as ProductCategory
, rw.Id as RouteVersionId
, rw.versionNumber
, rw.alternativeNo
, rw.DefaultVersion
, GETDATE() as ExportDate
-- , CAST(ROW_NUMBER() over(order by pc.Name, p.ProductNumber, rw.versionNumber,rw.alternativeNo) as numeric(10) )as rowno
--
from MwTech.dbo.Products as p
inner join MwTech.dbo.ProductCategories as pc
on pc.Id = p.ProductCategoryId
--
inner join dbo.Products as idx
on idx.ProductNumber = p.idx01
--
inner join MwTech.dbo.RouteVersions as rw
on rw.ProductId = idx.Id and rw.ProductCategoryId = p.ProductCategoryId
--
where 1 = 1
and pc.RouteSource = 1
and p.IsActive = 1
and p.IsTest = 0
and rw.IsActive = 1
and p.idx01 is not null and p.idx01 != ''
)



