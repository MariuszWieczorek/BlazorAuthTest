use mwtech;

with kbwk as
(
select ca.CategoryNumber, pr.ProductNumber
, replace(pr.ProductNumber,'KBWK','') as CommonPart
, v.VersionNumber, v.AlternativeNo, v.IsActive
, b.PartQty
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.id = pr.ProductCategoryId
inner join dbo.ProductVersions as v
on v.ProductId = pr.Id
inner join dbo.Boms as b
on b.SetVersionId = v.Id
inner join dbo.Products as pp
on pp.Id = b.PartId
where pr.ProductNumber like '%kbwk'
and v.VersionNumber = 1
and pp.ProductNumber = 'OPK-KARTON-01'
),


kbk as
(
select ca.CategoryNumber, pr.ProductNumber
, replace(pr.ProductNumber,'KBK','') as CommonPart
, v.VersionNumber, v.AlternativeNo, v.IsActive
, b.PartQty
, b.Id as BomId
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.id = pr.ProductCategoryId
inner join dbo.ProductVersions as v
on v.ProductId = pr.Id
inner join dbo.Boms as b
on b.SetVersionId = v.Id
inner join dbo.Products as pp
on pp.Id = b.PartId
where pr.ProductNumber like '%kbk'
and v.VersionNumber = 1
and pp.ProductNumber = 'OPK-KARTON-01'
)

--update bb
-- set PartQty = x.PartQtyKBWK
select bb.* 
from dbo.boms as bb
inner join 
(
select a.ProductNumber as PartNumberKBWK
, a.PartQty as PartQtyKBWK
, b.ProductNumber as PartNumberKBK
, b.PartQty as PartQtyKBK
, b.BomId
from kbwk as a
inner join kbk as b
on a.CommonPart = b.CommonPart
where a.PartQty != b.PartQty
) as x
on x.BomId = bb.Id
