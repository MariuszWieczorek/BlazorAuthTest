
UPDATE b
  set Excess = 0
  FROM [MwTech].[dbo].boms as b
  inner join dbo.ProductVersions as v
  on b.SetVersionId = v.Id
  inner join dbo.Products as p
  on p.Id = v.ProductId
  inner join dbo.ProductCategories as c
  on c.Id = p.ProductCategoryId
  where c.CategoryNumber = 'DAP'



SELECT p.ProductNumber,v.Name,
	   c.CategoryNumber,
	   b.Excess
  FROM [MwTech].[dbo].boms as b
  inner join dbo.ProductVersions as v
  on b.SetVersionId = v.Id
  inner join dbo.Products as p
  on p.Id = v.ProductId
  inner join dbo.ProductCategories as c
  on c.Id = p.ProductCategoryId
  where c.CategoryNumber = 'DAP'
  
  