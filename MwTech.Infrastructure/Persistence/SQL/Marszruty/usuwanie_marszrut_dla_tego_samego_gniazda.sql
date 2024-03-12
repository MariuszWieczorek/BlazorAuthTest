declare @ProductCategory as varchar(50)
declare @ProductNumber as varchar(50)
declare @RouteProductCategory as varchar(50)

declare @ProductId as int
declare @RouteProductCategoryId as int
declare @VersionNumber as int
declare @Name as varchar(50)
declare @MaxAlternativeNo as int
declare @MinAlternativeNo as int

/*

declare c cursor fast_forward for
WITH do_usuniecia AS (
  
   SELECT ca.CategoryNumber as ProductCategory	
	  ,pr.ProductNumber
	  ,rca.CategoryNumber as RouteProductCategory 
	  ,v.ProductId
	  ,v.ProductCategoryId as RouteProductCategoryId 
	  ,v.VersionNumber
	  ,v.name
	  ,max(v.AlternativeNo)  as MaxAlternativeNo
	  ,min(v.AlternativeNo)  as MinAlternativeNo
  FROM dbo.RouteVersions as v
  inner join dbo.Products as pr
  on pr.Id = v.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  inner join dbo.ProductCategories as rca
  on rca.Id = v.ProductCategoryId
  where 1 = 1
  and v.IsActive = 1
  group by
  ca.CategoryNumber
  ,pr.ProductNumber
  ,rca.CategoryNumber
  ,v.VersionNumber
  ,v.name
  ,v.ProductId
  ,v.ProductCategoryId
  having count(*) > 1
  
)

select * from do_usuniecia;

open c;
fetch next from c into @ProductCategory, @ProductNumber, @RouteProductCategory, @ProductId,@RouteProductCategoryId, @VersionNumber, @Name, @MaxAlternativeNo, @MinAlternativeNo;

while @@fetch_status = 0
   begin
   	
	declare @RouteVersionIdToDelete as int;
	set @RouteVersionIdToDelete = (select id from dbo.RouteVersions as vv where vv.ProductId = @ProductId and vv.ProductCategoryId = @RouteProductCategoryId and vv.VersionNumber = @VersionNumber and vv.AlternativeNo = @MaxAlternativeNo)
	
	print @RouteVersionIdToDelete;

	delete from dbo.ManufactoringRoutes where RouteVersionId = @RouteVersionIdToDelete
	delete from dbo.RouteVersions where Id = @RouteVersionIdToDelete
	
	fetch next from c into  @ProductCategory, @ProductNumber, @RouteProductCategory, @ProductId, @RouteProductCategoryId,@VersionNumber, @Name, @MaxAlternativeNo, @MinAlternativeNo;
   end

   close c;
   deallocate c;

   */