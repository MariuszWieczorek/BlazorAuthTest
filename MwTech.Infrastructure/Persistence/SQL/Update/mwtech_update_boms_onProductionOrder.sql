/****** Script for SelectTopNRows command from SSMS  ******/
SELECT p.ProductNumber,v.Name,
	   c.CategoryNumber,
	   b.Excess,
	   pp.ProductNumber,
	   b.OnProductionOrder
  FROM [MwTech].[dbo].boms as b
  inner join dbo.ProductVersions as v
  on b.SetVersionId = v.Id
  inner join dbo.Products as p
  on p.Id = v.ProductId
  inner join dbo.ProductCategories as c
  on c.Id = p.ProductCategoryId
  inner join dbo.Products as pp
  on pp.Id = b.PartId
  where pp.ProductNumber like 'MPO%'


  UPDATE b
  set b.OnProductionOrder = 0
    FROM [MwTech].[dbo].boms as b
  inner join dbo.ProductVersions as v
  on b.SetVersionId = v.Id
  inner join dbo.Products as p
  on p.Id = v.ProductId
  inner join dbo.ProductCategories as c
  on c.Id = p.ProductCategoryId
  inner join dbo.Products as pp
  on pp.Id = b.PartId
  where pp.ProductNumber like 'MPO%'
  
 