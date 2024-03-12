use MwTech;
/* Zestawienie ró¿nic tego co jest w IFS z MwTech */

-- Lista indeksów, które pomijamy w porównaniu
DECLARE @black_list table 
(
 id int identity(1, 1)
,ProductNumber varchar(30)
);

insert into @black_list  (productNumber) values ('XXXXXXXXXXX');

/* Zestawienie ró¿nic tego co jest w IFS z tym co jest w MwTech */

-- PARTS_BY_WEIGHT

WITH recipes_compare AS (

SELECT *
FROM (
SELECT ifs.CONTRACT
,ca.CategoryNumber as CATEGORY_NO
,ifs.PART_NO as PART_NO
,ifs.REVISION as REVISION
,ifs.ALTERNATIVE as ALTERNATIVE
,ifs.LINE_SEQUENCE as LINE_SEQUENCE
,ifs.COMPONENT_PART as ifs_COMPONENT_PART
,ISNULL(mw.COMPONENT_PART,'') as mw_COMPONENT_PART
,ifs.PARTS_BY_WEIGHT as ifs_PARTS_BY_WEIGHT
,ISNULL(mw.QTY_PER_ASSEMBLY,0) as mw_QTY_PER_ASSEMBLY
,ifs.SHRINKAGE_FACTOR as ifs_SHRINKAGE_FACTOR
,ISNULL(mw.SHRINKAGE_FACTOR,0) as mw_SHRINKAGE_FACTOR
,pr.IsActive as IsProductActive
,ifs.ALTERNATIVE_DESCRIPTION
-- TESTS
,iif(ifs.SHRINKAGE_FACTOR = mw.SHRINKAGE_FACTOR,1,0) as  SHRINKAGE_FACTOR_TEST
,iif(round(ifs.PARTS_BY_WEIGHT,2) = round(mw.QTY_PER_ASSEMBLY,2),1,0) as  QTY_TEST
,iif(ifs.COMPONENT_PART = mw.COMPONENT_PART,1,0) as  COMPONENT_TEST
--
,IIF( EXISTS (select top 1 * from dbo.mwtech_bom_ifs as m where trim(m.part_no) = trim(ifs.part_no)  and ifs.revision = m.revision and m.alternative = iif(ifs.ALTERNATIVE = '*',1,ifs.ALTERNATIVE)  ), 1 , 0)  as REV_AND_ALT_TEST
,IIF( EXISTS (select top 1 * from dbo.ProductVersions as v1 inner join dbo.Products as p1 on p1.id = v1.ProductId where trim(p1.ProductNumber) = trim(ifs.part_no)  and ifs.revision = v1.VersionNumber and v1.AlternativeNo = iif(ifs.ALTERNATIVE = '*',1,ifs.ALTERNATIVE)  ), 1 , 0)  as REV_AND_ALT_TEST2
,(select count(*) from dbo.ProductVersions as v1 inner join dbo.Products as p1 on p1.id = v1.ProductId inner join dbo.boms as b on b.SetVersionId = v1.id  where trim(p1.ProductNumber) = trim(ifs.part_no)  and ifs.revision = v1.VersionNumber and v1.AlternativeNo = iif(ifs.ALTERNATIVE = '*',1,ifs.ALTERNATIVE))   as ile
,IIF( EXISTS (select top 1 * from dbo.Products as p where p.ProductNumber = ifs.part_no), 1 , 0)  as PRODUCT_EXISTS
--
FROM dbo.ifs_receptury as ifs
LEFT JOIN dbo.Products as pr
ON trim(pr.ProductNumber) = trim(ifs.Part_no)
LEFT JOIN dbo.ProductCategories as ca
ON ca.Id = pr.ProductCategoryId
--
LEFT JOIN dbo.mwtech_bom_ifs as mw
ON 1 = 1
AND ifs.PARTS_BY_WEIGHT > 0 -- bez ujemnych iloœci
AND iif(ifs.ALTERNATIVE = '*',1,ifs.ALTERNATIVE) = mw.ALTERNATIVE -- bo w kursorze mw mam wartoœci * zamiast 1
AND ifs.REVISION = mw.REVISION
AND mw.PART_NO = ifs.PART_NO
--AND mw.COMPONENT_PART = ifs.COMPONENT_PART
AND mw.LINE_SEQUENCE = ifs.LINE_SEQUENCE
--
WHERE 1 = 1
AND (ISNUMERIC(ifs.ALTERNATIVE)=1 OR ifs.ALTERNATIVE = '*')
AND (ISNUMERIC(ifs.REVISION)=1 OR ifs.REVISION = '*')
) as x
WHERE 1 = 1
AND (( COMPONENT_TEST + QTY_TEST + PRODUCT_EXISTS + REV_AND_ALT_TEST  < 4 ) OR isProductActive = 0) 
AND PART_NO not like '%-TE'
AND ifs_PARTS_BY_WEIGHT > 0
AND ALTERNATIVE != '1'
)

select * 
from recipes_compare as x
where 1 = 1
AND PART_NO not in (select ProductNumber from @black_list)
-- AND PART_NO not like '%DS55%'
-- AND PART_NO = 'MIE.GK55-1'
ORDER BY x.CONTRACT, x.CATEGORY_NO, x.PART_NO, x.REVISION, x.ALTERNATIVE, x.LINE_SEQUENCE

