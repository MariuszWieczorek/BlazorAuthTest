use MwTech

select *
from(
select pr.Id as ProductId, pr.ProductNumber,ca.CategoryNumber,ca.Name as categoryName
,(select COUNT(*) from dbo.ProductVersions as vx where vx.ProductId = pr.Id and vx.ToIfs = 1) as bom_ileifs
,(select COUNT(*) from dbo.ProductVersions as vx where vx.ProductId = pr.Id and vx.ToIfs = 1 and vx.IfsDefaultVersion = 1) as bom_ileifs_def
,(select COUNT(*) from dbo.ProductVersions as vx where vx.ProductId = pr.Id and vx.DefaultVersion = 1) as bom_iledef
,(select COUNT(*) from dbo.ProductVersions as vx where vx.ProductId = pr.Id) as bom_ile
,(select COUNT(*) from dbo.RouteVersions as vx where vx.ProductId = pr.Id and vx.ToIfs = 1) as route_ileifs
,(select COUNT(*) from dbo.RouteVersions as vx where vx.ProductId = pr.Id and vx.ToIfs = 1 and vx.ifsDefaultVersion = 1) as route_ileifs_def
,(select COUNT(*) from dbo.ProductVersions as vx where vx.ProductId = pr.Id and vx.ToIfs = 0) as bom_ilexl
,(select COUNT(*) from dbo.RouteVersions as vx where vx.ProductId = pr.Id and vx.ToIfs = 0) as route_ilexl
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where 1=1
and ca.CategoryNumber NOT IN ('DWA','DMA')
-- and ca.CategoryNumber IN ('MIE','MON','NAW','MIP','MIR','MIF','MKA','MZE')
-- and ca.CategoryNumber IN ('OKG','OKC','NAW','MIP','MIR','MIF','MKA','MZE')
) as x
where 1=1   
	and x.bom_ile = 0 
-- and (x.bom_ileifs  = 0 or x.route_ileifs = 0)
-- and x.route_ileifs > 0 and  x.route_ileifs_def > 1
-- and x.bom_ileifs > 0 and  x.bom_ileifs_def > 1
-- and x.route_ileifs > 0 and  x.route_ileifs_def = 0
-- and x.bom_ileifs > 0 and  x.bom_ileifs_def = 0
-- and ( x.bom_ileifs = 0 and x.route_ileifs = 0 )
-- and ( x.bom_ileifs = 0 and x.route_ileifs > 0 )
-- and x.bom_ileifs > 0 and  x.bom_ileifs_def = 0
-- and x.route_ileifs > 0 and  x.route_ileifs_def = 0
--where 1 = 1
--and bom_iledef = 0 and bom_ile = 0
order by x.CategoryNumber,x.ProductNumber