use MwTech;


-- Lista indeksów, które pomijamy w porównaniu
DECLARE @black_list table 
(
 id int identity(1, 1)
,ProductNumber varchar(30)
);

-- insert into @black_list  (productNumber) values ('XXXXXXXXXXXXXX');

/* Zestawienie ró¿nic tego co jest w ifsTech z tym co jest w mw */
WITH structures_compare AS (

SELECT *
FROM (
SELECT mw.CONTRACT
,mw.SetCategory as CATEGORY_NO
,mw.PART_NO as mw_PART_NO
,mw.IsProductActive as mw_IsProductActive
,mw.ALTERNATIVE_NO as mw_ALTERNATIVE_NO
,mw.REVISION as mw_REVISION
,mw.LINE_SEQUENCE as mw_LINE_SEQUENCE
,mw.COMPONENT_PART as mw_COMPONENT_PART
,ISNULL(ifs.COMPONENT_PART,'') as ifs_COMPONENT_PART
,mw.QTY_PER_ASSEMBLY as mw_QTY_PER_ASSEMBLY
,ISNULL(ifs.QTY_PER_ASSEMBLY,0) as ifs_QTY_PER_ASSEMBLY
,mw.SHRINKAGE_FACTOR as mw_SHRINKAGE_FACTOR
,ISNULL(ifs.SHRINKAGE_FACTOR,0) as ifs_SHRINKAGE_FACTOR
-- TESTS
,iif(mw.SHRINKAGE_FACTOR = ifs.SHRINKAGE_FACTOR,1,0) as  SHRINKAGE_FACTOR_TEST
,iif(round(mw.QTY_PER_ASSEMBLY,2) = round(ifs.QTY_PER_ASSEMBLY,2),1,0) as  QTY_PER_ASSEMBLY_TEST
,iif(mw.COMPONENT_PART = ifs.COMPONENT_PART,1,0) as  COMPONENT_PART_TEST
,iif(ifs.ALTERNATIVE is not null,1,0) as NO_ALTERNATIVE
,iif(ifs.REVISION is not null,1,0) as NO_REVISION
--
FROM dbo.mwtech_bom_ifs as mw
LEFT JOIN dbo.ifs_struktury as ifs
ON 1 = 1
AND ifs.PART_NO = mw.PART_NO
AND iif(ifs.ALTERNATIVE = '*',1,ifs.ALTERNATIVE) = mw.ALTERNATIVE -- bo w kursorze ifs mam wartoœci * zamiast 1
AND iif(ifs.REVISION = '*',1,ifs.REVISION) = mw.REVISION
AND ifs.LINE_SEQUENCE = mw.LINE_SEQUENCE
--AND ifs.COMPONENT_PART = mw.COMPONENT_PART
--
WHERE 1 = 1
AND mw.isProductActive = 1 -- aktywny produkt
AND ifs.part_status = 'A'
AND mw.IsActive = 1 -- aktywna wersja 
AND mw.QTY_PER_ASSEMBLY > 0 -- bez ujemnych iloœci
and mw.SetCategory not like '%MIE%'
and mw.SetCategory not like '%NAW%'
and mw.SetCategory not in ('MOP','MOD')
) as x
WHERE 1 = 1
AND (( COMPONENT_PART_TEST + QTY_PER_ASSEMBLY_TEST + NO_ALTERNATIVE + NO_REVISION < 4 ))
)

select * 
from structures_compare as x
where 1 = 1
AND mw_PART_NO not in (select ProductNumber from @black_list)
AND mw_PART_NO not like 'KO%G2' -- odrzucamy drugie gatunki
AND mw_PART_NO not like 'MEM-%' -- odrzucamy membrany
AND mw_PART_NO not like '%-TE' -- odrzucamy testowe
AND CATEGORY_NO not like 'US£%' -- odrzucamy us³ugi
-- AND mw_PART_NO = 'DAP6009TR87FI45KB-BUT'
-- and NO_REVISION = 0
-- AND mw_PART_NO like 'OS%'
ORDER BY x.CONTRACT, x.CATEGORY_NO, x.mw_PART_NO, x.mw_REVISION, x.mw_ALTERNATIVE_NO

