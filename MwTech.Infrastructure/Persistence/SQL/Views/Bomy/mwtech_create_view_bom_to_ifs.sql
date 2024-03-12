CREATE OR ALTER VIEW mwtech_bom_ifs
AS
(
select 'KT1' as CONTRACT
,p.ProductNumber as PART_NO
,iif(pw.DefaultVersion = 1, '*', cast(pw.alternativeNo as varchar(5)) ) as ALTERNATIVE_NO
,pw.name  as ALTERNATIVE_DESCRIPTION
,pw.AlternativeNo as ALTERNATIVE
,pw.versionNumber as REVISION
,pw.IsActive  
,b.OrdinalNumber as LINE_SEQUENCE --LINE_SEQUENCE
,comp.ProductNumber as COMPONENT_PART
,b.PartQty as QTY_PER_ASSEMBLY
,b.Excess as SHRINKAGE_FACTOR
,cast(iif(b.OnProductionOrder=1,'Consumed','Not Consumed')  as varchar(15)) as CONSUMPTION_ITEM
-- pola dodatkowe
,b.Layer as Layer
,pw.DefaultVersion as IsDefaultVersion
,pc.CategoryNumber as SetCategory
,pc.Name as SetCategoryName
,pw.ProductQty as SetProductQty
,compCat.CategoryNumber as PartCategory
,compCat.Name as PartCategoryName
,p.Client
,comp.Id as ComponentId
,p.Id as SetId
,comp.Name as ComponentName
,p.Name as SetName
,b.OnProductionOrder
,GETDATE() as ExportDate
,p.IsActive as isProductActive
,p.IsTest as isTest
--
from MwTech.dbo.Products as p
inner join MwTech.dbo.ProductVersions as pw
on pw.ProductId = p.Id
-- and pw.DefaultVersion = 1
inner join MwTech.dbo.ProductCategories as pc
on pc.Id = p.ProductCategoryId
inner join MwTech.dbo.Boms as b
on b.SetId = p.Id and b.SetVersionId = pw.Id
inner join MwTech.dbo.Products as comp
on comp.Id = b.PartId 
inner join MwTech.dbo.ProductCategories as compCat
on compCat.Id = comp.ProductCategoryId
where 1 = 1
and pw.IsActive = 1
-- and p.IsActive = 1
)

-- ,iif(pw.DefaultVersion = 1, '*', cast(pw.versionNumber as varchar(5)) ) as wariant_struktury
-- select * from mwtech_route_ifs order by SetCategory, nr_pozycji, numer_operacji
-- select * from mwtech_bom_ifs order by SetCategory, nr_pozycji_nadrzednej, numer_pozycji_w_linii