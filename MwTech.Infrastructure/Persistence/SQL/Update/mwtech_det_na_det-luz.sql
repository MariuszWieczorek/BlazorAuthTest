select ca.CategoryNumber, pr.ProductNumber,
(select id from dbo.ProductCategories where CategoryNumber = 'DET-LUZ') as x
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where 1=1
and ca.CategoryNumber in ('DET')
and (
pr.ProductNumber like '%KB'
or
pr.ProductNumber like '%00'
or
pr.ProductNumber like '%NT'
)

update pr
set ProductCategoryId = (select id from dbo.ProductCategories where CategoryNumber = 'DET-LUZ')
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where 1=1
and ca.CategoryNumber in ('DET')
and (
pr.ProductNumber like '%KB'
or
pr.ProductNumber like '%00'
or
pr.ProductNumber like '%NT'
)