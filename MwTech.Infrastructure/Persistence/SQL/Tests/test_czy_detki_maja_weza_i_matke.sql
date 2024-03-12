/*
select pr.idx01, pr.Idx02, COUNT(*)
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where 1=1
-- and ca.CategoryNumber IN ('DST','DAP','DWU','DKJ','DET')
and idx01 in ('40060155TR15','40060155TR150','40060155TR218','40060155TR15B','40060155TR150B','40060155TR218B')
group by pr.Idx01,pr.Idx02
order by pr.Idx02,pr.Idx01
*/

select x.idx01
,COUNT(*)
from (
select pr.idx01, pr.Idx02
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where 1=1
and ca.CategoryNumber IN ('DST','DAP','DWU','DKJ','DET')
group by pr.Idx01,pr.Idx02
) as x
group by x.idx01
having COUNT(*) > 1


select pr.Idx01,
'matka ' + pr.Idx01 as nazwa,
'szt.' as jm,
'DMA' as grupa,
'',
pr.Idx01,
pr.Idx02,
COUNT(*)
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where 1=1
and ca.CategoryNumber IN ('DST','DAP','DWU','DKJ','DET')
group by pr.Idx02,pr.Idx01
order by pr.Idx01,pr.Idx02

select pr.Id as ProductId, pr.ProductNumber,ca.CategoryNumber,ca.Name as categoryName
,Idx01,Idx02
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where 1=1
and ca.CategoryNumber IN ('DST','DAP','DWU','DKJ','DET')
and ( pr.Idx01 is null OR pr.Idx01 = '')
