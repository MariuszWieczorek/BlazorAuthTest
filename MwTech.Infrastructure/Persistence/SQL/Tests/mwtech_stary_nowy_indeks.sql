  SELECT 
	  ca.CategoryNumber, ca.Name
     ,p.ProductNumber
	 ,coalesce(p.OldProductNumber,'') as oldProductNumber
	 ,coalesce(p.idx01,'') as idx01
	 ,coalesce(p.idx02,'') as idx02
  FROM dbo.Products as p
  inner join dbo. ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  order by ca.OrdinalNumber, p.ProductNumber


  select COUNT(*)
  FROM dbo.Products as p
