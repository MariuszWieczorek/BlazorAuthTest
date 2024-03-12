CREATE OR ALTER VIEW mwtech_bom_ifs_mieszanki
AS
(
select 'KT1' as Umiejscowienie
,REPLACE(p.ProductNumber,'_','-') as nr_pozycji_nadrzednej
,iif(pw.IfsDefaultVersion = 1, '*', cast(pw.versionNumber as varchar(5)) ) as wariant_struktury
,pw.name  as opis_wariantu
,b.OrdinalNumber as numer_pozycji_w_linii
,REPLACE(comp.ProductNumber,'_','-') as numer_komponentu
,b.PartQty as norma_zuzycia
,b.Excess as proc_wsp_nadmiaru
,cast(iif(b.OnProductionOrder=1,'Zu¿ywana','Niezu¿ywana')  as varchar(15)) as zuzycie_komp_na_zlec
,'A' as metoda_wydania
,pc.CategoryNumber
,pc.Name as SetCategory
,pw.ProductQty as SetProductQty
,p.client
,GETDATE() as ExportDate

from MwTech.dbo.Products as p
inner join MwTech.dbo.ProductVersions as pw
on pw.ProductId = p.Id
inner join MwTech.dbo.ProductCategories as pc
on pc.Id = p.ProductCategoryId
inner join MwTech.dbo.Boms as b
on b.SetId = p.Id and b.SetVersionId = pw.Id
inner join MwTech.dbo.Products as comp
on comp.Id = b.PartId 

where pc.CategoryNumber in ('MIE','NWR')
and pw.toifs = 1
and p.IsActive = 1

)