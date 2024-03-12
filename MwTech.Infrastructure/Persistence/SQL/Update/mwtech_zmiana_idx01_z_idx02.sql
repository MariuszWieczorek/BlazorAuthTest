/*
select COUNT(*)
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.id = pr.ProductCategoryId
where 1=1
-- and ca.CategoryNumber in ('DET','DWY','DOB','DST','DAP','DWU','DKJ')
and ca.Name like '%dêtka%'
*/


select ca.CategoryNumber,pr.ProductNumber,pr.OldProductNumber,pr.Idx01,pr.idx02,pr.Description
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.id = pr.ProductCategoryId
where 1=1
and ca.CategoryNumber in ('AKC-POZ')
-- and pr.ProductNumber like '%DEH%'
-- and len(idx02) > len(idx01) 
-- and LEN(idx01) != 0


/*

update pr
set description = Idx01
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.id = pr.ProductCategoryId
where 1=1
and ca.CategoryNumber in ('AKC-POZ')


update pr
set Idx01 = idx02
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.id = pr.ProductCategoryId
where 1=1
and ca.CategoryNumber in ('AKC-POZ')

update pr
set Idx02 = pr.Description
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.id = pr.ProductCategoryId
where 1=1
and ca.CategoryNumber in ('AKC-POZ')

*/