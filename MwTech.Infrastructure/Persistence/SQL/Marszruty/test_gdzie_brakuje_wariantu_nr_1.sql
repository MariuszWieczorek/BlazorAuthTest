  /*
  -- czy s¹ wszêdzie wersje nr 1
  SELECT ca.CategoryNumber as ProductCategory	
	  ,pr.ProductNumber
	  ,rca.CategoryNumber as RouteCategory
	  ,min(v.AlternativeNo) as minAltNo
  FROM dbo.Products as pr
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
 inner join dbo.RouteVersions as v
 on pr.Id = v.ProductId
  inner join dbo.ProductCategories as rca
  on rca.Id = v.ProductCategoryId
  where 1 = 1
 -- and ca.CategoryNumber in ('DMA','DWA')
  and v.IsActive = 1
  group by ca.CategoryNumber,pr.ProductNumber,rca.CategoryNumber 
  having min(v.AlternativeNo) > 1

  -- czy ka¿da wersja marszruty nr 1 jest wersj¹ domyœln¹
  SELECT ca.CategoryNumber as ProductCategory	
	  ,pr.ProductNumber
	  ,rca.CategoryNumber as RouteProductCategory 
	  ,v.AlternativeNo
	  ,v.DefaultVersion
  FROM dbo.RouteVersions as v
  inner join dbo.Products as pr
  on pr.Id = v.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  inner join dbo.ProductCategories as rca
  on rca.Id = v.ProductCategoryId
  where 1 = 1
  --and ca.CategoryNumber in ('DMA','DWA')
  and v.IsActive = 1
  -- and v.AlternativeNo != 1 and v.DefaultVersion = 1
  and v.AlternativeNo = 1 and v.DefaultVersion = 0


   -- czy istnieje nie 1 jest wersj¹ domyœln¹
  SELECT ca.CategoryNumber as ProductCategory	
	  ,pr.ProductNumber
	  ,rca.CategoryNumber as RouteProductCategory 
	  ,v.AlternativeNo
	  ,v.DefaultVersion
  -- UPDATE v
  --SET DefaultVersion = 0
  FROM dbo.RouteVersions as v
  inner join dbo.Products as pr
  on pr.Id = v.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  inner join dbo.ProductCategories as rca
  on rca.Id = v.ProductCategoryId
  where 1 = 1
 -- and ca.CategoryNumber in ('DMA','DWA')
  and v.IsActive = 1
  and v.AlternativeNo != 1 and v.DefaultVersion = 1
*/

-- powielone
   SELECT ca.CategoryNumber as ProductCategory	
	  ,pr.ProductNumber
	  ,rca.CategoryNumber as RouteProductCategory 
	--  ,v.AlternativeNo
	  ,v.name
	  ,count(*)
  -- UPDATE v
  --SET DefaultVersion = 0
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
	 -- ,v.AlternativeNo
	  ,v.name
having count(*) > 1
