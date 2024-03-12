/* Przeliczamy wagi mieszanek aby wpisaæ wagê w nag³ówek wersji */

select pr.ProductNumber, we.VersionNumber, we.name, we.ProductQty
,SUM(b.PartQty) as sumQty
, we.ProductQty - SUM(b.PartQty) as diff
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
and b.DoNotIncludeInTkw = 0
and ca.CategoryNumber in ('MIE','NAW')
and we.IsActive = 1
group by pr.ProductNumber, we.VersionNumber, we.AlternativeNo, we.name, we.ProductQty
having round(SUM(b.PartQty),3) != we.ProductQty
order by pr.ProductNumber


/*

update we
set we.ProductQty = x.sumQty
from dbo.ProductVersions as we
inner join (
select pr.id as product, we.id as version, 
 round(SUM(b.PartQty),3) as sumQty
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
and b.DoNotIncludeInTkw = 0
and ca.CategoryNumber in ('MIE','NAW')
and we.IsActive = 1
group by pr.id, we.id ) as x
on x.product = we.ProductId  and x.version = we.id
*/



/*
update rw
set ProductQty = we.ProductQty
from dbo.ProductVersions as we
inner join dbo.Products as pr
on pr.Id = we.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.RouteVersions as rw
on rw.ProductId = pr.Id and rw.DefaultVersion = 1 
where 1=1
and we.DefaultVersion = 1
and ca.CategoryNumber in ('MIE')
and rw.ProductQty != we.ProductQty
*/


/*
select pr.ProductNumber, we.VersionNumber, we.name, we.ProductQty as bomQty , rw.ProductQty as routeQty
from dbo.ProductVersions as we
inner join dbo.Products as pr
on pr.Id = we.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.RouteVersions as rw
on rw.ProductId = pr.Id and rw.DefaultVersion = 1 
where 1=1
and we.DefaultVersion = 1
and ca.CategoryNumber in ('MIE')
and rw.ProductQty != we.ProductQty
order by pr.ProductNumber
*/






/*
update we
set we.ProductQty = x.sumQty
from dbo.ProductVersions as we
inner join (
select pr.id as product, we.id as version, 
 round(SUM(b.PartQty),3) as sumQty
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
and ca.CategoryNumber in ('MOD','MOP','MON','MIE')
group by pr.id, we.id ) as x
on x.product = we.ProductId  and x.version = we.id
*/
