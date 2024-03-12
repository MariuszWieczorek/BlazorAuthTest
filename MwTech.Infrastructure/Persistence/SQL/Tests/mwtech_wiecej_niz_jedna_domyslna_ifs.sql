select coalesce(x.ProductNumber,y.ProductNumber) as Indeks, x.Ile as ile_domyslnych_struktur,
 y.Ile as ile_domyslnych_marszrut
from
(select p.ProductNumber, sum(iif(IfsDefaultVersion=1,1,0)) as Ile
from dbo.Products as p
inner join dbo.ProductVersions as v
on v.ProductId = p.Id
group by p.ProductNumber
having sum(iif(IfsDefaultVersion=1,1,0)) > 1) as y
full join
(
select p.ProductNumber, sum(iif(IfsDefaultVersion=1,1,0)) as Ile
from dbo.Products as p
inner join dbo.RouteVersions as v
on v.ProductId = p.Id
group by p.ProductNumber
having sum(iif(IfsDefaultVersion=1,1,0)) > 1
) as x
on x.ProductNumber = y.ProductNumber
order by coalesce(x.ProductNumber,y.ProductNumber)


select coalesce(x.ProductNumber,y.ProductNumber) as Indeks, x.Ile as ile_domyslnych_struktur,
 y.Ile as ile_domyslnych_marszrut
from
(select p.ProductNumber, sum(iif(ComarchDefaultVersion=1,1,0)) as Ile
from dbo.Products as p
inner join dbo.ProductVersions as v
on v.ProductId = p.Id
group by p.ProductNumber
having sum(iif(ComarchDefaultVersion=1,1,0)) > 1) as y
full join
(
select p.ProductNumber, sum(iif(ComarchDefaultVersion=1,1,0)) as Ile
from dbo.Products as p
inner join dbo.RouteVersions as v
on v.ProductId = p.Id
group by p.ProductNumber
having sum(iif(ComarchDefaultVersion=1,1,0)) > 1
) as x
on x.ProductNumber = y.ProductNumber
order by coalesce(x.ProductNumber,y.ProductNumber)

