SELECT *
FROM (
SELECT ifs.Contract
,ca.CategoryNumber
,ifs.PartNo
,ifs.AlternativeNo
,ifs.RevisionNo
,ifs.LineSequence
,ifs.ComponentPart as IfsComponentPart
,ISNULL(mw.COMPONENT_PART,'') as MwComponentPart
,ifs.QtyPerAssembly as IfsQtyPerAssembly
,ifs.PartsByWeight as IfsPartsByWeight
,ISNULL(mw.QTY_PER_ASSEMBLY,0) as MwQtyPerAssembly
,ifs.ShrinkageFactor as IfsShrinkageFactor
,ISNULL(mw.SHRINKAGE_FACTOR,0) as MwShrinkageFactor
,ISNULL(pr.id,0) as ProductId
,pr.IsActive as IsProductActive
-- TESTS
,iif(ifs.ShrinkageFactor = mw.SHRINKAGE_FACTOR,1,0) as  TestShrinkageFactor
,iif(round(ifs.PartsByWeight,2) = round(mw.QTY_PER_ASSEMBLY,2),1,0) as  TestQtyPerAssembly
,iif(ifs.ComponentPart = mw.COMPONENT_PART,1,0) as  TestComponentPart
--
,ifs.ConsumptionItemDb as IfsConsumptionItem
,mw.CONSUMPTION_ITEM as MwConsumptionItem
--
,iif(mw.ALTERNATIVE is not null,1,0) as TestExistAlternativeNo
,iif(mw.REVISION is not null,1,0) as TestExistsRevisionNo
--,IIF( EXISTS (select top 1 * from dbo.mwtech_bom_ifs as m where trim(m.part_no) = trim(ifs.PartNo)  and ifs.RevisionNo = m.revision and m.alternative = iif(ifs.AlternativeNo = '*',1,ifs.AlternativeNo)  ), 1 , 0)  as TestRevAndAlt1
,IIF( EXISTS (select top 1 * from dbo.ProductVersions as v1 inner join dbo.Products as p1 on p1.id = v1.ProductId where trim(p1.ProductNumber) = trim(ifs.PartNo)  and ifs.RevisionNo = v1.VersionNumber and v1.AlternativeNo = iif(ifs.AlternativeNo = '*',1,ifs.AlternativeNo)  ), 1 , 0)  as TestRevAndAlt2
--,(select count(*) from dbo.ProductVersions as v1 inner join dbo.Products as p1 on p1.id = v1.ProductId inner join dbo.boms as b on b.SetVersionId = v1.id  where trim(p1.ProductNumber) = trim(ifs.PartNo)  and ifs.RevisionNo = v1.VersionNumber and v1.AlternativeNo = iif(ifs.AlternativeNo = '*',1,ifs.AlternativeNo))   as ile
--,IIF( EXISTS (select top 1 * from dbo.Products as p where p.ProductNumber = ifs.PartNo), 1 , 0)  as ProductExists
--
FROM dbo.IfsProductRecipes as ifs
LEFT JOIN dbo.Products as pr
ON trim(pr.ProductNumber) = trim(ifs.PartNo)
LEFT JOIN dbo.ProductCategories as ca
ON ca.Id = pr.ProductCategoryId

LEFT JOIN dbo.mwtech_bom_ifs as mw
ON 1 = 1
AND mw.QTY_PER_ASSEMBLY > 0 -- bez ujemnych iloœci
AND mw.ALTERNATIVE = iif(ifs.AlternativeNo = '*',1,ifs.AlternativeNo)  -- bo w kursorze mw mam wartoœci * zamiast 1
AND iif(ifs.RevisionNo = '*',1,ifs.RevisionNo) = mw.REVISION
AND mw.PART_NO = ifs.PartNo
AND mw.LINE_SEQUENCE = ifs.LineSequence
--
WHERE 1 = 1
AND (ISNUMERIC(ifs.AlternativeNo)=1 OR ifs.AlternativeNo = '*')
AND (ISNUMERIC(ifs.RevisionNo)=1 OR ifs.RevisionNo = '*')
) as x
WHERE 1 = 1
AND (( TestComponentPart + TestQtyPerAssembly + TestExistsRevisionNo + TestExistAlternativeNo < 4 ) OR isProductActive = 0)
AND x.PartNo not in (select ProductNumber from dbo.IFS_black_list)
AND x.PartNo not like 'KO%G2' -- odrzucamy drugie gatunki
AND x.PartNo not like 'MEM-%' -- odrzucamy membrany
AND x.PartNo not like '%-TE' -- odrzucamy testowe
AND IfsPartsByWeight > 0   