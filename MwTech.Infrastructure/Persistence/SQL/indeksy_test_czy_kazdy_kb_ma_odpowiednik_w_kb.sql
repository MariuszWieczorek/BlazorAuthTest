use mwtech;

declare @grp_kb1 as varchar(10);
set @grp_kb1 = 'KBWK';
declare @grp_kb2 as varchar(10);
set @grp_kb2 = 'KB';

declare @grp_nt as varchar(10);
set @grp_nt = 'NTWK';

with kb_product_numbers as 
(
select x.CategoryNumber, x.grupa, x.PartOfProductNumber 
from 
(
select ca.CategoryNumber, pr.ProductNumber, concat('1_',@grp_kb1) as grupa, replace(pr.ProductNumber,@grp_kb1,'') as PartOfProductNumber
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where 1 = 1
and ca.CategoryNumber in ('DET','DET-KBK','DET-B','DET-LUZ')
and pr.ProductNumber like concat('%',@grp_kb1)
union all
select ca.CategoryNumber, pr.ProductNumber, concat('2_',@grp_kb2) as grupa, replace(pr.ProductNumber,@grp_kb2,'') as PartOfProductNumber
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where 1 = 1
and ca.CategoryNumber in ('DET','DET-KBK','DET-B','DET-LUZ')
and pr.ProductNumber like concat('%',@grp_kb2)

) as x
group by x.CategoryNumber, x.grupa, x.PartOfProductNumber 
),

 nt_product_numbers as 
(
select x.CategoryNumber, x.PartOfProductNumber 
from 
(
select ca.CategoryNumber, pr.ProductNumber, replace(pr.ProductNumber,@grp_nt,'') as PartOfProductNumber
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where 1 = 1
and ca.CategoryNumber in ('DET','DET-KBK','DET-B','DET-LUZ')
and pr.ProductNumber like concat('%',@grp_nt)
) as x
group by x.CategoryNumber, x.PartOfProductNumber 
)


select kb.Grupa, kb.CategoryNumber, kb.PartOfProductNumber  
from kb_product_numbers as kb
left join nt_product_numbers as nt
on kb.CategoryNumber = nt.CategoryNumber
and kb.PartOfProductNumber = nt.PartOfProductNumber
where nt.PartOfProductNumber is null
order by kb.Grupa, kb.CategoryNumber, kb.PartOfProductNumber


