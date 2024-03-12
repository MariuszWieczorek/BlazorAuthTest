/****** Script for SelectTopNRows command from SSMS  ******/
SELECT p.ProductNumber, v.name
  FROM [MwTech].[dbo].[ProductVersions] as v
  inner join dbo.Products as p
  on p.id = v.ProductId
  where year(v.CreatedDate) = 2022 and month(v.CreatedDate) = 12 and day(v.CreatedDate) = 20 