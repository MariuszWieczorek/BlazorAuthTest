use MwTech;

/*
IF OBJECT_ID('dbo.ifs_receptury','U') IS NOT NULL
	DROP TABLE dbo.ifs_receptury;

CREATE TABLE dbo.ifs_receptury
	( 
  LP INT
 , CONTRACT VARCHAR(5)
 , PART_NO VARCHAR(50)
 , REVISION_NO NVARCHAR(10)
 , REVISION_NAME NVARCHAR(255)
 , ALTERNATIVE_NO NVARCHAR(20)
 , ALTERNATIVE_DESCRIPTION NVARCHAR(255)
 , STATE NVARCHAR(255)
--
, LINE_ITEM_NO  NVARCHAR(10)
, LINE_SEQUENCE NVARCHAR(10)
, COMPONENT_PART NVARCHAR(50)
, QTY_PER_ASSEMBLY DECIMAL(10,5)
, PARTS_BY_WEIGHT DECIMAL(10,5)
, CONSUMPTION_ITEM_DB NVARCHAR(25)
, PRINT_UNIT  NVARCHAR(25)
, EFF_PHASE_IN_DATE DATETIME
, EFF_PHASE_OUT_DATE  DATETIME
, COMPONENT_SCRAP   DECIMAL(10,5)
, SHRINKAGE_FACTOR   DECIMAL(10,5)

)


GO


bulk insert dbo.ifs_receptury from 'c:\02\ifs_receptury.csv' 
-- with ( CODEPAGE = '65001', fieldterminator =';' ,rowterminator ='\n', FIRSTROW = 1, LASTROW = 2 )
-- with ( CODEPAGE = '65001', fieldterminator =';' ,rowterminator ='\n', FIRSTROW = 2)
with ( CODEPAGE = 'APC', fieldterminator =';' ,rowterminator ='\n', FIRSTROW = 2)
-- with ( CODEPAGE = 'ACP', fieldterminator =';' ,rowterminator ='\n' ) fieldquote = '"'

*/



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
-- and CategoryNumber not in ('OSM-TE','OSU-TE','OWU-TE','OBC-TE','OBB-TE','OBK-TE','OKG-TE','ODP')
-- and CategoryNumber not in ('DWY','DOB','DST','DAP','DWU','DKJ','DKJ-B','DWU-B','DET','DET-KBK','DET-B','DET-LUZ')
-- and CategoryNumber not in ('OSU')
-- and CategoryNumber not in ('OWU')
-- and CONTRACT = 'KT1'
-- and mw_komponent is not null
and ifs_indeks not like '%-TE'
-- and ( ifs_wariant = '1' or ifs_wariant = '2' or ifs_wariant = '*')
Order by x.CONTRACT, x.CategoryNumber, x.ifs_indeks, x.ifs_wariant, x.ifs_wersja






/*zapytanie IFS
SELECT 
  s.CONTRACT
, s.part_no
, INVENTORY_PART_API.Get_Description(s.Contract,s.PART_NO) as part_name
, h.ENG_CHG_LEVEL as RevisionNo
, PART_REVISION_API.Get_Revision_Text(h.contract,h.part_no,h.ENG_CHG_LEVEL) as RevisionName
, v.ALTERNATIVE_NO
, v.ALTERNATIVE_DESCRIPTION
, MANUF_STRUCT_ALTERNATE_API.Get_State(h.contract,h.part_no,h.eng_chg_level,h.bom_type,v.alternative_no) as State
, s.LINE_ITEM_NO
, s.COMPONENT_PART
, s.QTY_PER_ASSEMBLY
, s.CONSUMPTION_ITEM_DB
, s.PRINT_UNIT
, s.COMPONENT_USAGE_FACTOR
, s.EFF_PHASE_IN_DATE
, s.EFF_PHASE_OUT_DATE
, s.COMPONENT_SCRAP
, s.SHRINKAGE_FACTOR
--
FROM PROD_STRUCTURE_HEAD  h
INNER JOIN PROD_STRUCT_ALTERNATE v
on  v.part_no = h.part_no 
and v.eng_chg_level = h.eng_chg_level
and v.contract = h.contract
--
INNER JOIN PROD_STRUCTURE s
on  s.part_no = v.part_no 
and s.alternative_no = v.alternative_no
and s.eng_chg_level = v.eng_chg_level
and s.contract = v.contract
--
where s.contract = 'KT1'
and  s.EFF_PHASE_OUT_DATE is null
and h.part_no = 'OBC-100-75-153-001'

*/


/*
/*GENEROWANIE PLIKU CSV DO PORÓWNANIA STRUKTÓR PRODUKTOWYCH */
-- INVENTORY_PART_API.Get_Part_Status(h.CONTRACT,h.PART_NO) = 'A' Aktywny Indeks

SELECT 
  h.CONTRACT
, h.PART_NO
-- , INVENTORY_PART_API.Get_Part_Status(h.CONTRACT,h.PART_NO) as PART_STATUS
-- , INVENTORY_PART_API.Get_Description(s.Contract,s.PART_NO) as PART_NAME
, h.ENG_CHG_LEVEL as ENG_CHG_LEVEL
, PART_REVISION_API.Get_Revision_Text(h.contract,h.part_no,h.ENG_CHG_LEVEL) as RevisionName
, v.ALTERNATIVE_NO
, v.ALTERNATIVE_DESCRIPTION
, MANUF_STRUCT_ALTERNATE_API.Get_State(h.contract,h.part_no,h.eng_chg_level,h.bom_type,v.alternative_no) as ALTERNATIVE_STATE
, s.LINE_ITEM_NO
, s.COMPONENT_PART
, trim(to_char(s.QTY_PER_ASSEMBLY,'999990.99999')) as QTY_PER_ASSEMBLY
, s.CONSUMPTION_ITEM_DB
, s.PRINT_UNIT
, trim(to_char(s.EFF_PHASE_IN_DATE,'YYYYMMdd')) as EFF_PHASE_IN_DATE
, trim(to_char(s.EFF_PHASE_OUT_DATE,'YYYYMMdd')) as EFF_PHASE_OUT_DATE
, trim(to_char(s.COMPONENT_SCRAP,'999990.99999')) as COMPONENT_SCRAP
, trim(to_char(s.SHRINKAGE_FACTOR,'999990.99999')) as SHRINKAGE_FACTOR

--
FROM PROD_STRUCTURE_HEAD  h
INNER JOIN PROD_STRUCT_ALTERNATE v
on  v.part_no = h.part_no 
and v.eng_chg_level = h.eng_chg_level
and v.contract = h.contract
--
INNER JOIN PROD_STRUCTURE s
on  s.part_no = v.part_no 
and s.alternative_no = v.alternative_no
and s.eng_chg_level = v.eng_chg_level
and s.contract = v.contract
--
where s.contract = 'KT1'
and  s.EFF_PHASE_OUT_DATE is null
and INVENTORY_PART_API.Get_Part_Status(h.CONTRACT,h.PART_NO) = 'A'
-- and h.part_no LIKE 'OBC%'


*/