

select pr.Id, ProductNumber, TechCardNumber,x.rownr
from dbo.Products as pr
inner join
(
select pr.Id
,CAST(ROW_NUMBER() over(order by pr.ProductNumber) as numeric(10) ) as rownr
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where ca.CategoryNumber = 'OKA'
) as x
on x.Id = pr.Id


update pr
set TechCardNumber = x.rownr
from dbo.Products as pr
inner join
(
select pr.Id
,CAST(ROW_NUMBER() over(order by pr.ProductNumber) as numeric(10) ) as rownr
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where ca.CategoryNumber = 'OKA'
) as x
on x.Id = pr.Id
