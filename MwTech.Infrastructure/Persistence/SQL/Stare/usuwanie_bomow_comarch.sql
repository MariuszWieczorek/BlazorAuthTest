/****** Script for SelectTopNRows command from SSMS  ******/
SELECT pr.ProductNumber , v.AlternativeNo --, parts.ProductNumber
  FROM [MwTech].[dbo].[ProductVersions] as v
  inner join dbo.Products as pr
  on pr.Id = v.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  --inner join dbo.Boms as b
  -- on b.SetId = pr.Id and b.SetVersionId = v.Id
  -- inner join dbo.Products as parts
  -- on parts.Id = b.PartId
  where ca.CategoryNumber in ('MIE')
  and v.ToIfs =  0
  and v.IsActive = 1
  -- and v.AlternativeNo > 100

  /*
  DELETE b
  FROM [MwTech].[dbo].[ProductVersions] as v
  inner join dbo.Products as pr
  on pr.Id = v.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  inner join dbo.Boms as b
  on b.SetId = pr.Id and b.SetVersionId = v.Id
  inner join dbo.Products as parts
  on parts.Id = b.PartId
  where ca.CategoryNumber in ('MIE')
  and v.ToIfs = 0
  and v.AlternativeNo > 100


  DELETE v
  FROM [MwTech].[dbo].[ProductVersions] as v
  inner join dbo.Products as pr
  on pr.Id = v.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber in ('MIE')
  and v.ToIfs = 0
  and v.AlternativeNo > 100
  
  */