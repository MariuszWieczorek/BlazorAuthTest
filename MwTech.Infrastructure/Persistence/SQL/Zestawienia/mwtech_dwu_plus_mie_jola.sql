/* Zestawienie - dêtka wulkanizowana plus mieszanka z dêtki wyt³aczanej */
/* ³¹czymy po id wê¿a */

select z.ProductNumber, z.CategoryNumber,  z.Name, z.Idx02, z.PartProductNumber
from
(
select distinct p.ProductNumber, ca.CategoryNumber,  p.Name, p.Idx02, x.PartProductNumber
from dbo.Products as p
inner join dbo.ProductCategories as ca
on ca.Id = p.ProductCategoryId
left join
(
select sca.CategoryNumber as SetCategory
,sp.ProductNumber as SetProductNumber
,sp.Idx01
,sp.Idx02
,pca.CategoryNumber as PartCategory
,pp.ProductNumber as PartProductNumber
from dbo.Boms as b
inner join Products as sp
on sp.Id = b.SetId
inner join dbo.ProductCategories as sca
on sca.Id = sp.ProductCategoryId
inner join Products as pp
on pp.Id = b.PartId 
inner join dbo.ProductCategories as pca
on pca.Id = pp.ProductCategoryId
where sca.CategoryNumber = 'DWY' and pca.CategoryNumber = 'MIE'
) as x
on x.Idx02 = p.Idx02
where ca.CategoryNumber = 'DWU'
) as z
group by z.ProductNumber, z.CategoryNumber,  z.Name, z.Idx02, z.PartProductNumber
order by z.ProductNumber
