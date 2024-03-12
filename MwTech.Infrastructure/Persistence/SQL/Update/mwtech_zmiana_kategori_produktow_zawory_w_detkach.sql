/* Zamiana Grupy Zaworów dotychczasowa grupa*/

select p.Id
,p.productNumber
,p.Name
from dbo.Products as p
inner join 
(
select x.PartId, x.PartProductNumber,x.PartName,'szt.' as jm, x.PartCategoryNumber
from (
select p.ProductNumber as SetProductNumber, ca.CategoryNumber
, b.PartId
, pp.ProductNumber as PartProductNumber
, pp.Name as PartName
, caca.CategoryNumber as PartCategoryNumber
from dbo.Boms as b
inner join dbo.Products as p
on p.Id = b.SetId
inner join dbo.ProductCategories as ca
on ca.Id = p.ProductCategoryId
inner join dbo.Products as pp
on pp.Id = b.PartId
inner join dbo.ProductCategories as caca
on caca.Id = pp.ProductCategoryId
where ca.CategoryNumber in ('DAP')
and caca.CategoryNumber in ('SUR')
) as x
group by x.PartProductNumber,x.PartName,x.PartCategoryNumber,x.PartId
) as z
on z.PartId = p.id


/*

update  p
set ProductCategoryId = 59
from dbo.Products as p
inner join 
(
select x.PartId, x.PartProductNumber,x.PartName,'szt.' as jm, x.PartCategoryNumber
from (
select p.ProductNumber as SetProductNumber, ca.CategoryNumber
, b.PartId
, pp.ProductNumber as PartProductNumber
, pp.Name as PartName
, caca.CategoryNumber as PartCategoryNumber
from dbo.Boms as b
inner join dbo.Products as p
on p.Id = b.SetId
inner join dbo.ProductCategories as ca
on ca.Id = p.ProductCategoryId
inner join dbo.Products as pp
on pp.Id = b.PartId
inner join dbo.ProductCategories as caca
on caca.Id = pp.ProductCategoryId
where ca.CategoryNumber in ('DAP')
and caca.CategoryNumber in ('SUR')
) as x
group by x.PartProductNumber,x.PartName,x.PartCategoryNumber,x.PartId
) as z
on z.PartId = p.id
*/
