/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      ,[PropertyId]
      ,[ProductId]
      ,[Value]
      ,[Text]
      ,[MaxValue]
      ,[MinValue]
  FROM [MwTech].[dbo].[ProductProperties]
  where PropertyId = (select Id from dbo.Properties where PropertyNumber = 'dwy_dlugosc')


  update [MwTech].[dbo].[ProductProperties]
  set MinValue = Value, MaxValue = Value
  where PropertyId = (select Id from dbo.Properties where PropertyNumber = 'dst_prad_laczenia')