/****** Script for SelectTopNRows command from SSMS  ******/
use MwTech;

declare @Id as INT
declare @ProductNo as varchar(50)
declare @Weight as decimal(10,2)
declare @RowNo as int
declare @Cat as varchar(10)
set @Cat = 'OBC'
set @RowNo = 1;

declare c cursor fast_forward for
select p.Id, p.ProductNumber
from MwTech.dbo.Products as p
where p.ProductNumber like 'KO%'
order by p.ProductNumber

open c;
fetch next from c into @Id, @ProductNo;

   while @@fetch_status = 0
   begin
    --
    WAITFOR delay '00:00:01';
	print 1;
	--print @ProductNo + cast(dbo.getProductWeight(@Id) as varchar(10));
	--print dbo.getProductWeight(@Id);
	/*
	update p
	set p.TechCardNumber = @RowNo
	from MwTech.dbo.Products as p
	where p.ProductNumber = @ProductNo
	*/
	--
	set @RowNo = @RowNo + 1;
   	fetch next from c into  @Id,@ProductNo;
   end

   close c;
   deallocate c;




