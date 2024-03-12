/* Ustawiamy Iloœci w nag³ówkach wersji */

select x.categorynumber, x.ProductId, x.ProductNumber, x.VersionNumber, x.ProductQty, x.weight
,x.ProductQty - x.weight as diff
from (
select pr.id as ProductId, pr.ProductNumber, ca.CategoryNumber, we.id, we.VersionNumber, we.ToIfs, we.IfsDefaultVersion, we.ComarchDefaultVersion, we.ProductQty

,coalesce((select SUM(bx.PartQty) 
	from dbo.Boms as bx
	inner join dbo.ProductVersions as wx 
	on bx.SetVersionId = wx.Id 
	where bx.SetId = pr.Id 
	and wx.DefaultVersion = 1 
	and bx.OnProductionOrder = 1),0)
	as weight

from dbo.Products as pr
left join dbo.ProductVersions as we
on we.ProductId = pr.Id
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where ca.CategoryNumber in ('MIE','NAW','MIP','MIR')
and we.ToIfs = 1
group by pr.id, we.id, pr.ProductNumber, ca.CategoryNumber, we.VersionNumber, we.ToIfs, we.IfsDefaultVersion, we.ComarchDefaultVersion, we.ProductQty
) as x
where x.ProductQty - x.weight != 0
order by  x.CategoryNumber, x.ProductNumber


/* Update na wersjach BOM */


update r
set ProductQty =  coalesce(x.weight,0)
FROM dbo.ProductVersions as r
inner join (
select pr.id, pr.ProductNumber, ca.CategoryNumber, we.id as weid, we.VersionNumber, we.ToIfs, we.IfsDefaultVersion, we.ComarchDefaultVersion, we.ProductQty

,coalesce((select SUM(bx.PartQty) from dbo.Boms as bx
inner join dbo.ProductVersions as wx 
on bx.SetVersionId = wx.Id
where bx.SetId = pr.Id and wx.DefaultVersion = 1
and bx.OnProductionOrder = 1
),1) as weight

from dbo.Products as pr
left join dbo.ProductVersions as we
on we.ProductId = pr.Id
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where ca.CategoryNumber in ('MIE','NAW','MIP','MIR')
and we.toifs = 1
group by pr.id, we.id, pr.ProductNumber, ca.CategoryNumber, we.VersionNumber, we.ToIfs, we.IfsDefaultVersion, we.ComarchDefaultVersion, we.ProductQty
) as x
on x.weid = r.id





