use MwTech;

IF OBJECT_ID('dbo.ifs_receptury','U') IS NOT NULL
	DROP TABLE dbo.ifs_receptury;

CREATE TABLE dbo.ifs_receptury
	( 
   LP VARCHAR(10)
 , CONTRACT VARCHAR(5)
 , PART_NO VARCHAR(50)
 , REVISION VARCHAR(10)
 , REVISION_NAME VARCHAR(255)
 , ALTERNATIVE VARCHAR(20)
 , ALTERNATIVE_DESCRIPTION VARCHAR(255)
 , STATE VARCHAR(255)
--
, LINE_ITEM_NO  VARCHAR(10)
, LINE_SEQUENCE VARCHAR(10)
, COMPONENT_PART VARCHAR(50)
, QTY_PER_ASSEMBLY DECIMAL(10,5)
, PARTS_BY_WEIGHT DECIMAL(10,5)
, CONSUMPTION_ITEM_DB VARCHAR(25)
, PRINT_UNIT  VARCHAR(25)
, EFF_PHASE_IN_DATE DATETIME
, EFF_PHASE_OUT_DATE  DATETIME
, COMPONENT_SCRAP   DECIMAL(10,5)
, SHRINKAGE_FACTOR   DECIMAL(10,5)
, PART_STATUS VARCHAR(10)
)


GO


bulk insert dbo.ifs_receptury from 'c:\02\ifs_receptury.csv' 
-- with ( CODEPAGE = '65001', fieldterminator =';' ,rowterminator ='\n', FIRSTROW = 1, LASTROW = 2 )
with ( CODEPAGE = 'APC', fieldterminator =';' ,rowterminator ='\n', FIRSTROW = 2)
-- with ( CODEPAGE = 'ACP', fieldterminator =';' ,rowterminator ='\n' ) fieldquote = '"'




/*Wy³owienie nienumerycznych numerów wersji i wariantów*/
/*
SELECT PART_NO,REVISION_NO,REVISION_NAME,ALTERNATIVE_NO,ALTERNATIVE_DESCRIPTION
from dbo.ifs_receptury as ifs
WHERE 1 = 1
AND ifs.ALTERNATIVE_NO != '*'
AND ifs.REVISION_NO != '*'
AND
(
ISNUMERIC(ifs.ALTERNATIVE_NO )=0
OR ISNUMERIC(ifs.REVISION_NO)=0
)
GROUP BY PART_NO,REVISION_NO,REVISION_NAME,ALTERNATIVE_NO,ALTERNATIVE_DESCRIPTION
ORDER BY PART_NO,REVISION_NO,REVISION_NAME,ALTERNATIVE_NO,ALTERNATIVE_DESCRIPTION
*/


/*
select ifs.PART_NO, ifs.COMPONENT_PART, ifs.LINE_SEQUENCE
from dbo.ifs_struktury as ifs
where ifs.COMPONENT_PART = 'OPK-KARTON-01'
and ifs.LINE_SEQUENCE = 3
*/

/* Zestawienie ró¿nic tego co jest w IFS z MwTech */

/*
select *
from (
select ifs.CONTRACT, ca.CategoryNumber, ifs.Part_no as ifs_indeks
,ifs.ALTERNATIVE_NO as ifs_wariant
--, mw.wariant as mw_wariant
,ifs.REVISION_NO as ifs_wersja
--,mw.wersja as mw_wersja
--,ifs.LINE_ITEM_NO as ifs_lp, mw.numer_pozycji_w_linii as mw_lp
,ifs.LINE_SEQUENCE as ifs_lp
--, mw.numer_pozycji_w_linii as mw_lp
,ifs.COMPONENT_PART as ifs_komponent, mw.numer_komponentu as mw_komponent
-- ,ifs.QTY_PER_ASSEMBLY as ifs_ilosc_1
,ifs.PARTS_BY_WEIGHT as ifs_ilosc
, mw.norma_zuzycia as mw_ilosc
,ifs.SHRINKAGE_FACTOR as ifs_nadmiar, mw.proc_wsp_nadmiaru as mw_nadmiar
-- ,iif(ifs.SHRINKAGE_FACTOR = mw.proc_wsp_nadmiaru,0,1) as  test_nadmiar
,iif(round(ifs.PARTS_BY_WEIGHT,2) = round(mw.norma_zuzycia,2),0,1) as  test_ilosc
,iif(ifs.COMPONENT_PART = mw.numer_komponentu,0,1) as  test_indeks
,iif(pr.ProductNumber is null,1,0) as brak_indeksu
,iif(mw.wariant is null,1,0) as brak_wariantu
,iif(mw.wersja is null,1,0) as brak_wersji
from dbo.ifs_receptury as ifs
left join dbo.Products as pr
on pr.ProductNumber = ifs.Part_no
left join dbo.mwtech_bom_ifs as mw
on mw.nr_pozycji_nadrzednej = ifs.part_no and mw.norma_zuzycia > 0
and iif(ifs.ALTERNATIVE_NO = '*',1,ifs.ALTERNATIVE_NO) = mw.wariant
and iif(ifs.REVISION_NO = '*',1,ifs.REVISION_NO) = mw.wersja
-- and ifs.LINE_ITEM_NO = mw.numer_pozycji_w_linii
and ifs.LINE_SEQUENCE = mw.numer_pozycji_w_linii
AND mw.IsActive = 1
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
--
WHERE 1 = 1
AND (ISNUMERIC(ifs.ALTERNATIVE_NO)=1 OR ifs.ALTERNATIVE_NO = '*')
AND (ISNUMERIC(ifs.REVISION_NO)=1 OR ifs.REVISION_NO = '*')
) as x
where ( test_indeks + test_ilosc + brak_indeksu + brak_wariantu + brak_wersji > 0 )
and CategoryNumber not in ('OSM-TE','OSU-TE','OWU-TE','OBC-TE','OBB-TE','OBK-TE','OKG-TE','ODP')
-- and CategoryNumber not in ('DWY','DOB','DST','DAP','DWU','DKJ','DKJ-B','DWU-B','DET','DET-KBK','DET-B','DET-LUZ')
-- and CategoryNumber not in ('OSU')
-- and CategoryNumber not in ('OWU')
-- and CONTRACT = 'KT1'
and mw_komponent is not null
and ifs_indeks not like '%-TE'
-- and ( ifs_wariant = '1' or ifs_wariant = '2' or ifs_wariant = '*')
Order by x.CONTRACT, x.CategoryNumber, x.ifs_indeks, x.ifs_wariant, x.ifs_wersja
*/





