-- Receptury - zestawienie ró¿nic tego co jest w IFS z tym co jest w MwTech
-- Wersja z 2023.10.19

USE MwTech;

DECLARE @black_list table 
(
 id int identity(1, 1)
,ProductNumber varchar(30)
);

insert into @black_list  (productNumber) values ('XXXXXXXXXXX');

WITH recipes_compare AS (
SELECT *
FROM (
SELECT ifs.Contract
,ca.CategoryNumber
,ifs.PartNo
,ifs.RevisionNo
,ifs.AlternativeNo
,ifs.LineSequence
,ifs.ComponentPart as IfsComponentPart
,ISNULL(mw.COMPONENT_PART,'') as MwComponentPart
,ifs.PartsByWeight as IfsPartsByWeight
,ISNULL(mw.QTY_PER_ASSEMBLY,0) as MwQtyPerAssembly
,ifs.ShrinkageFactor as IfsShrinkageFactor
,ISNULL(mw.SHRINKAGE_FACTOR,0) as MwShrinkageFactor
,pr.IsActive as IsProductActive
,ifs.AlternativeDescription
-- TESTS
,iif(ifs.ShrinkageFactor = mw.SHRINKAGE_FACTOR,1,0) as  TestShrinkageFactor
,iif(round(ifs.PartsByWeight,2) = round(mw.QTY_PER_ASSEMBLY,2),1,0) as  TestComponentQty
,iif(ifs.ComponentPart = mw.COMPONENT_PART,1,0) as  TestComponentPart
--
,IIF( EXISTS (select top 1 * from dbo.mwtech_bom_ifs as m where trim(m.part_no) = trim(ifs.PartNo)  and ifs.RevisionNo = m.revision and m.alternative = iif(ifs.AlternativeNo = '*',1,ifs.AlternativeNo)  ), 1 , 0)  as TestRevAndAlt1
,IIF( EXISTS (select top 1 * from dbo.ProductVersions as v1 inner join dbo.Products as p1 on p1.id = v1.ProductId where trim(p1.ProductNumber) = trim(ifs.PartNo)  and ifs.RevisionNo = v1.VersionNumber and v1.AlternativeNo = iif(ifs.AlternativeNo = '*',1,ifs.AlternativeNo)  ), 1 , 0)  as TestRevAndAlt2
,(select count(*) from dbo.ProductVersions as v1 inner join dbo.Products as p1 on p1.id = v1.ProductId inner join dbo.boms as b on b.SetVersionId = v1.id  where trim(p1.ProductNumber) = trim(ifs.PartNo)  and ifs.RevisionNo = v1.VersionNumber and v1.AlternativeNo = iif(ifs.AlternativeNo = '*',1,ifs.AlternativeNo))   as ile
,IIF( EXISTS (select top 1 * from dbo.Products as p where p.ProductNumber = ifs.PartNo), 1 , 0)  as ProductExists
--
FROM dbo.IfsProductRecipes as ifs
LEFT JOIN dbo.Products as pr
ON trim(pr.ProductNumber) = trim(ifs.PartNo)
LEFT JOIN dbo.ProductCategories as ca
ON ca.Id = pr.ProductCategoryId
--
LEFT JOIN dbo.mwtech_bom_ifs as mw
ON 1 = 1
AND mw.ALTERNATIVE = iif(ifs.AlternativeNo = '*',1,ifs.AlternativeNo)  -- bo w kursorze mw mam wartoœci * zamiast 1
AND mw.REVISION = ifs.RevisionNo
AND mw.PART_NO = ifs.PartNo
AND mw.LINE_SEQUENCE = ifs.LineSequence
AND ifs.PartsByWeight > 0 -- bez ujemnych iloœci
--
WHERE 1 = 1
AND (ISNUMERIC(ifs.AlternativeNo)=1 OR ifs.AlternativeNo = '*')
AND (ISNUMERIC(ifs.RevisionNo)=1 OR ifs.RevisionNo = '*')
) as x
WHERE 1 = 1
AND (( TestComponentPart + TestComponentQty + ProductExists + TestRevAndAlt1  < 4 ) OR isProductActive = 0) 
AND PartNo not like '%-TE'
AND IfsPartsByWeight > 0
AND AlternativeNo != '1'
)

select * 
from recipes_compare as x
where 1 = 1
AND PartNo not in (select ProductNumber from @black_list)
ORDER BY x.Contract, x.CategoryNumber, x.PartNo, x.RevisionNo, x.AlternativeNo, x.LineSequence

