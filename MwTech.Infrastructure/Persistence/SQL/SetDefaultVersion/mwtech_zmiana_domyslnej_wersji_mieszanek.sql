

/*
select p.ProductNumber, pv.DefaultVersion, pv.ToIfs, pv.IfsDefaultVersion
from dbo.Products as p
inner join dbo.ProductCategories as ca
on ca.id = p.ProductCategoryId
inner join dbo.ProductVersions as pv
on pv.ProductId = p.id
where ca.CategoryNumber in ('WW-PROF','WW-PPROF','WW-AKL','WW-ORN','WF-GOT')

update pv
set DefaultVersion = 1
,IfsDefaultVersion = 1
,ToIfs = 1
,ComarchDefaultVersion = 0
from dbo.Products as p
inner join dbo.ProductCategories as ca
on ca.id = p.ProductCategoryId
inner join dbo.ProductVersions as pv
on pv.ProductId = p.id
where ca.CategoryNumber in ('WW-PROF','WW-PPROF','WW-AKL','WW-ORN','WF-GOT')

*/


select p.ProductNumber, pv.DefaultVersion, pv.ToIfs, pv.IfsDefaultVersion
from dbo.Products as p
inner join dbo.ProductCategories as ca
on ca.id = p.ProductCategoryId
inner join dbo.RouteVersions as pv
on pv.ProductId = p.id
where ca.CategoryNumber in ('MIE')



-- Ustawiam jako domyœln¹, wersjê produktu bêd¹c¹ domyœln¹ w IFS
-- Pod warunkiem
update pv
set DefaultVersion = IfsDefaultVersion
from dbo.Products as p
inner join dbo.ProductCategories as ca
on ca.id = p.ProductCategoryId
inner join dbo.RouteVersions as pv
on pv.ProductId = p.id
where ca.CategoryNumber in ('MIE')
and EXISTS (select pp.ProductId from dbo.ProductVersions as pp where pp.IfsDefaultVersion = 1 and pp.ProductId = p.Id)


update pv
set DefaultVersion = ComarchDefaultVersion
from dbo.Products as p
inner join dbo.ProductCategories as ca
on ca.id = p.ProductCategoryId
inner join dbo.RouteVersions as pv
on pv.ProductId = p.id
where ca.CategoryNumber in ('MIE')
and EXISTS (select pp.ProductId from dbo.ProductVersions as pp where pp.ComarchDefaultVersion = 1 and pp.ProductId = p.Id)



/*
update p
set OldProductNumber = null
from dbo.Products as p
inner join dbo.ProductCategories as ca
on ca.id = p.ProductCategoryId
where ca.CategoryNumber in ('DKJ')
*/