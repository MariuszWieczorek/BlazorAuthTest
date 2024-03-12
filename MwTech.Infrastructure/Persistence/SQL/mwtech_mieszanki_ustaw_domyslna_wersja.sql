/* aktualizacja pola iloœæ produktu z nag³ówka wersji */
/* wyliczamy j¹ na podstawie wagi sk³adników w BOM */
/* nie bie¿emy pod uwagê indeksów nierozchodowalnych */

select pr.ProductNumber, we.VersionNumber, we.name, we.ProductQty
,SUM(b.PartQty) as sumQty
from dbo.ProductVersions as we
inner join dbo.Products as pr
on pr.Id = we.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.Boms as b
on b.SetVersionId = we.Id and b.SetId = pr.id
where 1=1
and we.ToIfs = 1
and b.OnProductionOrder = 1
and ca.CategoryNumber in ('MIE','NAW','MIP')
group by pr.ProductNumber, we.VersionNumber, we.name, we.ProductQty
having round(SUM(b.PartQty),2) != we.ProductQty
order by pr.ProductNumber


update we
set we.ProductQty = x.sumQty
from dbo.ProductVersions as we
inner join (
select pr.id as product, we.id as version, 
 round(SUM(b.PartQty),2) as sumQty
from dbo.ProductVersions as we
inner join dbo.Products as pr
on pr.Id = we.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.Boms as b
on b.SetVersionId = we.Id and b.SetId = pr.id
where 1=1
and we.ToIfs = 1
and b.OnProductionOrder = 1
and ca.CategoryNumber in ('MIE','NAW','MIP')
group by pr.id, we.id ) as x
on x.product = we.ProductId  and x.version = we.id

/*
select pr.ProductNumber, we.VersionNumber, we.name, we.ProductQty
,(select SUM(b.PartQty) from dbo.boms as b where b.SetVersionId = we.id and b.SetId = pr.id and b.OnProductionOrder = 1) as waga
from dbo.ProductVersions as we
inner join dbo.Products as pr
on pr.Id = we.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where 1=1
and we.IfsDefaultVersion = 1
and ca.CategoryNumber in ('MIE','NAW','MIP')
order by pr.ProductNumber
*/
