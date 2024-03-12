-- Ustawiam jako domyœln¹, wersjê produktu i marszruty
-- Najpierw domyœlna IFS jako domyslna
-- Potem domyslna Comarchowa jako domyslna je¿eli taka istnieje

/*
select p.ProductNumber, pv.DefaultVersion, pv.ToIfs, pv.IfsDefaultVersion
from dbo.Products as p
inner join dbo.ProductCategories as ca
on ca.id = p.ProductCategoryId
inner join dbo.ProductVersions as pv
on pv.ProductId = p.id
where ca.CategoryNumber in ('MIE')
and EXISTS (select pp.ProductId from dbo.ProductVersions as pp where pp.IfsDefaultVersion = 1 and pp.ProductId = p.Id)
*/

-- BOMY

update pv
set DefaultVersion = IfsDefaultVersion
from dbo.Products as p
inner join dbo.ProductCategories as ca
on ca.id = p.ProductCategoryId
inner join dbo.ProductVersions as pv
on pv.ProductId = p.id
where ca.CategoryNumber in ('MIE')
and EXISTS (select pp.ProductId from dbo.ProductVersions as pp where pp.IfsDefaultVersion = 1 and pp.ProductId = p.Id)

update pv
set DefaultVersion = ComarchDefaultVersion
from dbo.Products as p
inner join dbo.ProductCategories as ca
on ca.id = p.ProductCategoryId
inner join dbo.ProductVersions as pv
on pv.ProductId = p.id
where ca.CategoryNumber in ('MIE')
and EXISTS (select pp.ProductId from dbo.ProductVersions as pp where pp.ComarchDefaultVersion = 1 and pp.ProductId = p.Id)

-- Marszruty

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
