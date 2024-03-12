SELECT [Id]
      ,[ProductNumber]
      ,[Name]
      ,[Ean13Code]
  FROM [MwTech].[dbo].[Products]
  where Ean13Code is not null

