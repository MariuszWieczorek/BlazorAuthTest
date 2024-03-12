/****** Script for SelectTopNRows command from SSMS  ******/
declare @ProductNo as varchar(50)
declare @RowNo as int
declare @Cat as varchar(10)
set @Cat = 'OBC'
set @RowNo = 1;

update p
set p.TechCardNumber = null
from MwTech.dbo.Products as p
inner join MwTech.dbo.ProductCategories as pc
on pc.Id = p.ProductCategoryId
where pc.CategoryNumber in (@Cat)

declare c cursor fast_forward for
select ProductNumber
from MwTech.dbo.Products as p
inner join MwTech.dbo.ProductCategories as pc
on pc.Id = p.ProductCategoryId
where pc.CategoryNumber in (@Cat)
and not ProductNumber like '%-TE%'
order by p.ProductNumber

open c;
fetch next from c into @ProductNo;

   while @@fetch_status = 0
   begin
    --
	print @ProductNo;


	update p
	set p.TechCardNumber = @RowNo
	from MwTech.dbo.Products as p
	where p.ProductNumber = @ProductNo

	--
	set @RowNo = @RowNo + 1;
   	fetch next from c into  @ProductNo;
   end

   close c;
   deallocate c;





select ProductNumber,  TechCardNumber
from MwTech.dbo.Products as p
inner join MwTech.dbo.ProductCategories as pc
on pc.Id = p.ProductCategoryId
where pc.CategoryNumber in (@Cat)
order by p.ProductNumber


