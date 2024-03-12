select  v.ProductId, p.ProductNumber,v.VersionNumber,COUNT(*) as ile
from dbo.ProductVersions as v
inner join dbo.Products as p
on p.Id = v.ProductId
where 1 = 1
and v.ToIfs = 0
and v.VersionNumber = 1
and v.DefaultVersion = 0
group by v.ProductId, p.ProductNumber,v.VersionNumber
order by p.ProductNumber,v.VersionNumber


/*
select  v.ProductId, p.ProductNumber,v.VersionNumber,COUNT(*) as ile
from dbo.RouteVersions as v
inner join dbo.Products as p
on p.Id = v.ProductId
where v.ToIfs = 1
group by v.ProductId, p.ProductNumber,v.VersionNumber
having COUNT(*) > 1
order by p.ProductNumber,v.VersionNumber


select  v.ProductId, p.ProductNumber,COUNT(*) as ile
from dbo.ProductVersions as v
inner join dbo.Products as p
on p.Id = v.ProductId
where v.ToIfs = 1
group by v.ProductId, p.ProductNumber
having COUNT(*) > 1
order by p.ProductNumber
*/