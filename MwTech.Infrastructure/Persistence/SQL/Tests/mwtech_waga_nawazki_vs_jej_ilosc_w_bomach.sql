/* sprawdzamy wag� nawa�ki i por�wnujemy j� z ilo�ci� w BOM */ 
/* powinna by� taka sama */
/* waga nawa�ki jest brana z pola wpisywanego r�cznie */
/* nale�y to pole zaktualizowa� przed por�wnaniem */

select prs.id, prs.ProductNumber
,cap.CategoryNumber
,prp.ProductNumber
,b.PartQty as ile_w_bom
,wep.ProductQty as waga_nawazki
,b.PartQty - wep.ProductQty as roznica
from dbo.Boms as b
inner join dbo.ProductVersions as we
on we.Id = b.SetVersionId
inner join dbo.Products as prs
on prs.Id = b.SetId
inner join dbo.ProductCategories as cas
on cas.Id = prs.ProductCategoryId
inner join dbo.Products as prp
on prp.Id = b.PartId
inner join dbo.ProductCategories as cap
on cap.Id = prp.ProductCategoryId
inner join dbo.ProductVersions as wep
on wep.ProductId = b.PartId and wep.IfsDefaultVersion = 1 and wep.ToIfs = 1
where cap.CategoryNumber in ('NAW')
and wep.ProductQty != b.PartQty
order by roznica

/* szukamy nawa�ek bez woreczk�w */
select x.SetProductNumber, x.wersja, sum(x.ile)
from 
(
select prs.id
,prs.ProductNumber as SetProductNumber
,cap.CategoryNumber
,prp.ProductNumber
,we.Id as wersja
,cast(iif(b.OnProductionOrder=0,1,0) as int) as ile
from dbo.Boms as b
inner join dbo.ProductVersions as we
on we.Id = b.SetVersionId
inner join dbo.Products as prs
on prs.Id = b.SetId
inner join dbo.ProductCategories as cas
on cas.Id = prs.ProductCategoryId
inner join dbo.Products as prp
on prp.Id = b.PartId
inner join dbo.ProductCategories as cap
on cap.Id = prp.ProductCategoryId
inner join dbo.ProductVersions as wep
on wep.ProductId = b.PartId
-- and wep.IfsDefaultVersion = 1 and wep.ToIfs = 1
where cas.CategoryNumber in ('NAW')
) as x
group by x.SetProductNumber, x.wersja
having sum(x.ile) = 0


