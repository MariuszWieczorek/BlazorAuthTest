declare  @ProductId as int
declare  @RouteCategoryId as int 
declare  @VersionNumber as int


declare c cursor fast_forward for
WITH do_ponumerowania AS (

select cp.CategoryNumber as ProductCategoryNumber
,ca.CategoryNumber as RouteCategoryNumber
,r.Id as RouteCategoryId
,p.ProductNumber
,r.ProductId
,r.VersionNumber
,r.AlternativeNo
,r.Id
from dbo.RouteVersions as r
inner join dbo.Products as p
on p.Id = r.ProductId
left join dbo.ProductCategories as ca
on ca.id = r.ProductCategoryId
left join dbo.ProductCategories as cp
on cp.id = p.ProductCategoryId
where r.IsActive = 1
and cp.CategoryNumber in ('DMA','DWA')
and r.VersionNumber = 5
and r.AlternativeNo > 1

)

select p.ProductId, p.RouteCategoryId, p.VersionNumber
from do_ponumerowania as p
where 1=1
group by p.ProductId, p.RouteCategoryId, p.VersionNumber
order by p.ProductId, p.RouteCategoryId, p.VersionNumber;


open c;


fetch next from c into @ProductId, @RouteCategoryId, @VersionNumber;

while @@fetch_status = 0
   begin
	
	print cast(@ProductId as varchar(10)) + '#' +  cast(@RouteCategoryId as varchar(10)) + '#' +  cast(@VersionNumber as varchar(10));



	fetch next from c into @ProductId, @RouteCategoryId, @VersionNumber;	
   end


close c;
deallocate c;
