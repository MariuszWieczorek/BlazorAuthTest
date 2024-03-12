declare @ProductId as int
declare @ProductQty as int
declare @VersionNumber as int
declare @AlternativeNo as int
declare @Name as varchar(10)
declare @ProductCategoryId as int
declare @RowId as int
declare @CurrentDate as datetime
declare @UserId as varchar(100)
declare @NewId as int

set @CurrentDate = getdate()
set @UserId = '7f2bacf6-564a-4272-a4bb-f76832476024'

declare c cursor fast_forward for

with do_przeniesienia as (

-- pozycje marszruty do przeniesienia
  SELECT ca.CategoryNumber as ProductCategory	
	  ,pr.ProductNumber
	  ,v.ProductId
	  ,v.VersionNumber
      ,r.RouteVersionId
	  ,r.Id as RowId
      ,v.ProductCategoryId
	  ,r.OperationId
	  ,rca.CategoryNumber as RouteProductCategory 
	  ,v.AlternativeNo
	  ,v.name
	  ,wc.ResourceNumber
  FROM [MwTech].[dbo].[ManufactoringRoutes] as r
  inner join dbo.RouteVersions as v
  on v.Id = r.RouteVersionId
  inner join dbo.Products as pr
  on pr.Id = v.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  inner join dbo.ProductCategories as rca
  on rca.Id = v.ProductCategoryId
  inner join dbo.Resources as wc
  on wc.Id = r.WorkCenterId
  where 1 = 1
  and ca.CategoryNumber in ('DMA')
  and v.Name != wc.ResourceNumber
  and (select count(*) from dbo.ManufactoringRoutes as mm where mm.RouteVersionId = v.id) = 2

)

select p.ProductId
  ,1 as ProductQty
  ,p.VersionNumber
  ,p.ResourceNumber as Name
  ,p.ProductCategoryId
  ,p.RowId
from do_przeniesienia as p


open c;
fetch next from c into @ProductId, @ProductQty, @VersionNumber, @Name,  @ProductCategoryId, @RowId;

   while @@fetch_status = 0
   begin
    --
	SET   @AlternativeNo = (select max(vv.AlternativeNo) + 1 from dbo.RouteVersions as vv where vv.ProductId = @ProductId and vv.VersionNumber = @VersionNumber and vv.ProductCategoryId = @ProductCategoryId);
	--
	
	declare @test as int 
	set @test = (select count(*) from [dbo].[RouteVersions] as vv where vv.ProductId = @ProductId and vv.VersionNumber = @VersionNumber and vv.ProductCategoryId = @ProductCategoryId and vv.Name = @Name)

	IF @test = 0
	BEGIN
	INSERT INTO [dbo].[RouteVersions]
           ([ProductId]
		   ,[ProductQty]
		   ,[VersionNumber]
           ,[AlternativeNo]
		   ,[Name]
           
           ,[Description]
           ,[CreatedDate]
           ,[CreatedByUserId]
           ,[IsActive]
           ,[ToIfs]
           ,[DefaultVersion]
		   ,IsAccepted01
		   ,IsAccepted02
           ,[ProductCategoryId])
		   values
		   (
			 @ProductId
			,@ProductQty
			,@VersionNumber
			,@AlternativeNo
			,@Name
			,'#20230711#'+ cast(@RowId as varchar(10))
			,getdate()
			,@UserId
			,1
			,1
			,1
			,0
			,0
			,@ProductCategoryId
		   )
	
	--

	SET @NewId = IDENT_CURRENT('dbo.RouteVersions');
	print 'dodano rekord nag³ówka' + cast(@NewId as varchar(10)) + '#' + cast(@AlternativeNo as varchar(10));
	--

	update m
	set RouteVersionId = @NewId
	FROM [dbo].[ManufactoringRoutes] as m
	where m.Id = @RowId
	
	END
	
	ELSE -- Je¿eli jest ju¿ taki nag³ówek
	
	BEGIN
		
		
		UPDATE m
		set Description = 'do_usuniecia_20230711'
		FROM [dbo].[ManufactoringRoutes] as m
		where m.Id = @RowId

		print 'usuniêto pozycjê id =' + cast( @RowId as varchar(10))
	END

	--

   	fetch next from c into  @ProductId, @ProductQty, @VersionNumber, @Name,  @ProductCategoryId, @RowId;
   end

   close c;
   deallocate c;




