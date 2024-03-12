select  x.Category, x.Indeks,x.waz,x.matka, x.MarszrutyWaz, x.MarszrutyMatka
from(
select ca.CategoryNumber as category, pr.ProductNumber as indeks ,pr1.ProductNumber as matka, pr2.ProductNumber as waz
,(select count(*) from dbo.RouteVersions as r where r.ProductId = pr2.Id and r.ProductCategoryId = pr.ProductCategoryId) as MarszrutyWaz
,(select count(*) from dbo.RouteVersions as r where r.ProductId = pr1.Id and r.ProductCategoryId = pr.ProductCategoryId) as MarszrutyMatka
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on pr.ProductCategoryId = ca.Id
left join dbo.Products as pr2
on pr2.ProductNumber = pr.Idx02
left join dbo.Products as pr1
on pr1.ProductNumber = pr.Idx01
where ca.CategoryNumber = 'DWU-B') as x
where x.MarszrutyMatka = 0
order by waz,indeks
