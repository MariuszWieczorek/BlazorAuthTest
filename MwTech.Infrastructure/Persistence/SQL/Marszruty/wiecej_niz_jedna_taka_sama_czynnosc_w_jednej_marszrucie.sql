-- Po dwie takie same czynnoœci w obrêbie jednej wersji marszruty

SELECT ca.CategoryNumber as ProductCategory	
	  ,pr.ProductNumber
      ,r.RouteVersionId
      ,r.ProductCategoryId
	  ,r.OperationId
	  ,rca.CategoryNumber as RouteProductCategory 
	  ,v.AlternativeNo
	  ,v.name
--	  ,wc.ResourceNumber
	  ,COUNT(*) as ile
  FROM [MwTech].[dbo].[ManufactoringRoutes] as r
  inner join dbo.RouteVersions as v
  on v.Id = r.RouteVersionId
  inner join dbo.Products as pr
  on pr.Id = v.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  inner join dbo.ProductCategories as rca
  on rca.Id = v.ProductCategoryId
--  inner join dbo.Resources as wc
--  on wc.Id = r.WorkCenterId
  where 1 = 1
 -- and ca.CategoryNumber in ('DMA')
  group by 
       r.RouteVersionId
      ,r.ProductCategoryId
	  ,pr.ProductNumber
	  ,r.OperationId
	  ,ca.CategoryNumber
	  ,rca.CategoryNumber 
	  ,v.AlternativeNo
	  ,v.name
	  --,wc.ResourceNumber
	  having COUNT(*) > 1


-- wyszukuje marszruty, które maj¹ jedn¹ czynnoœæ
-- ale gniazdo w tej czynnoœci jest niezgodne z opisem w wariancie marszruty
SELECT ca.CategoryNumber as ProductCategory	
	  ,pr.ProductNumber
      ,r.RouteVersionId
      ,r.ProductCategoryId
	  ,r.OperationId
	  ,rca.CategoryNumber as RouteProductCategory 
	  ,v.AlternativeNo
	  ,v.name
	  ,wc.ResourceNumber
	  ,(select count(*) from dbo.ManufactoringRoutes as mm where mm.RouteVersionId = v.id) as ile
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
 -- and ca.CategoryNumber in ('DMA')
  and v.Name != wc.ResourceNumber
  and (select count(*) from dbo.ManufactoringRoutes as mm where mm.RouteVersionId = v.id) = 1

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
--  and ca.CategoryNumber in ('DMA')
--  and v.Name != wc.ResourceNumber
  and (select count(*) from dbo.ManufactoringRoutes as mm where mm.RouteVersionId = v.id) = 2

