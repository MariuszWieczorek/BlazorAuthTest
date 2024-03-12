/* Export operacji marszrut do IFS */
CREATE OR ALTER VIEW mwtech_route_xl_mieszanki
AS
(
select 'KT1' as Umiejscowienie
,REPLACE(p.ProductNumber,'_','-') as nr_pozycji
,iif(rw.IfsDefaultVersion = 1, '*', cast(rw.versionNumber as varchar(5)) ) as wariant_struktury
,rw.name  as opis_wariantu
-- operacja
, m.OrdinalNumber as numer_operacji
, CAST(o.OperationNumber as varchar(35)) as opis_operacji
-- gniazdo
, wc.ResourceNumber as nr_gniazda
-- przezbrojenie
, m.ChangeOverMachineConsumption as przezbr_maszynochlonnosc
, m.ChangeOverLabourConsumption as przezbr_pracochlonnosc
, ch.ResourceNumber as przezbr_kategoria_zaszeregownia
, m.ChangeOverNumberOfEmployee as przezbr_ilosc_pracownikow
-- wyk
, m.OperationMachineConsumption as maszynochlonnosc
-- , u.UnitCode as maszynochlonnosc_jm
, m.OperationLabourConsumption as pracochlonnosc
, u.UnitCode as pracochlonnosc_jm
, r.ResourceNumber as kat_zaszeregowania
, m.ResourceQty as wielkosc_brygady
-- inne
, m.MoveTime as czas_transportu
, m.Overlap as zachodzenie
, CAST('Jednostki' as varchar(10)) as jedn_zachodzenia
, CAST('Nierównoleg³a' as varchar(15)) as operacja_rownolegla
-- dodatkowe pola

,pc.CategoryNumber
,pc.Name as SetCategory
,rw.ProductQty as SetProductQty
,p.client
,GETDATE() as ExportDate
--
from MwTech.dbo.Products as p
inner join MwTech.dbo.RouteVersions as rw
on rw.ProductId = p.Id 
inner join MwTech.dbo.ProductCategories as pc
on pc.Id = p.ProductCategoryId
inner join MwTech.dbo.ManufactoringRoutes as m
on m.RouteVersionId = rw.Id
inner join MwTech.dbo.Operations as o
on o.Id = m.OperationId
inner join MwTech.dbo.Resources as r
on r.Id = m.ResourceId
inner join MwTech.dbo.Resources as wc
on wc.Id = m.WorkCenterId
left join MwTech.dbo.Resources as ch
on ch.Id = m.ChangeOverResourceId
inner join MwTech.dbo.Units as u
on u.Id = o.UnitId
where pc.CategoryNumber in ('MIE','NWR')
and rw.toifs=0
)

-- select * from mwtech_route_ifs order by SetCategory, nr_pozycji, numer_operacji
-- select * from mwtech_bom_ifs order by SetCategory, nr_pozycji_nadrzednej, numer_pozycji_w_linii


