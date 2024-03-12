use MwTech



select *
from(
select pr.Id as ProductId, pr.ProductNumber,ca.CategoryNumber,ca.Name as categoryName
,(select COUNT(*) from dbo.ProductVersions as vx where vx.ProductId = pr.Id and vx.IsActive = 1) as bom_ile
,(select COUNT(*) from dbo.ProductVersions as vx where vx.ProductId = pr.Id and vx.IsActive = 1 and vx.ToIfs = 1) as bom_ifs_ile
,(select COUNT(*) from dbo.ProductVersions as vx where vx.ProductId = pr.Id and vx.IsActive = 1 and vx.DefaultVersion = 1) as bom_def_ile
,(select COUNT(*) from dbo.RouteVersions as vx where vx.ProductId = pr.Id  and vx.IsActive = 1) as route_ile
,(select COUNT(*) from dbo.RouteVersions as vx where vx.ProductId = pr.Id  and vx.IsActive = 1 and vx.ToIfs = 1) as route_ifs_ile
,(select COUNT(*) from dbo.RouteVersions as vx where vx.ProductId = pr.Id  and vx.IsActive = 1 and vx.ToIfs = 1 and vx.DefaultVersion = 1) as route_def_ile
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where 1=1
and ca.CategoryNumber IN ('DWY','DOB','DST','DAP','DWU','DKJ','DET','DET-KBK','DET-LUZ', 'DWU-B', 'DKJ-B')
) as x
where 1=1   
--	and x.bom_ile = 0 
--  and (x.bom_ifs_ile  = 0 or x.route_ifs_ile = 0)
    and (x.bom_def_ile  = 0 or x.route_def_ile = 0)
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


/*
select *
from dbo.ProductVersions as v
where v.DefaultVersion = 1 and v.IsActive = 0

update v
set IsActive = 1
from dbo.ProductVersions as v
where v.DefaultVersion = 1 and v.IsActive = 0
*/



/*
insert into dbo.ProductVersions
(
ProductId,
ProductQty,
ToIfs,
DefaultVersion,
IfsDefaultVersion,
VersionNumber,
Name,
CreatedByUserId,
CreatedDate,
Description
)
(
select 
x.ProductId,
1 as ProductQty,
1 as ToIfs,
1 as DefaultVersion,
1 as IfsDefaultVersion,
1 as VersionNumber,
'wariant 1' as name,
'7f2bacf6-564a-4272-a4bb-f76832476024' as CreatedByUserId,
GETDATE() as CreatedDate,
'20221115a' as description
from
(
select pr.Id as ProductId, pr.ProductNumber,ca.CategoryNumber
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
-- and ca.CategoryNumber = 'MIE'
) as x
where 1 = 1
and bom_iledef = 0 and bom_ile = 0
)*/





-- and x.bom_ileifs  = 0

-- and x.route_ileifs > 0 and  x.route_ileifs_def > 1
-- and x.bom_ileifs > 0 and  x.bom_ileifs_def > 1
-- and x.route_ileifs > 0 and  x.route_ileifs_def = 0
-- and x.bom_ileifs > 0 and  x.bom_ileifs_def = 0
-- and ( x.bom_ileifs = 0 and x.route_ileifs = 0 )
-- and ( x.bom_ileifs = 0 and x.route_ileifs > 0 )
-- and x.bom_ileifs > 0 and  x.bom_ileifs_def = 0
-- and x.route_ileifs > 0 and  x.route_ileifs_def = 0



