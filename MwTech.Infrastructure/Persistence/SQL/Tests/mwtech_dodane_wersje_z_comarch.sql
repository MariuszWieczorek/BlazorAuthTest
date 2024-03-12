/****** Script for SelectTopNRows command from SSMS  ******/
SELECT *
  FROM [MwTech].[dbo].[RouteVersions]
  where year(CreatedDate) = 2022 and MONTH(CreatedDate) = 11 and DAY(CreatedDate) = 25 and CreatedByUserId = '7f2bacf6-564a-4272-a4bb-f76832476024'



  UPDATE [MwTech].[dbo].[RouteVersions]
  set ToIfs = 0
  where year(CreatedDate) = 2022 and MONTH(CreatedDate) = 11 and DAY(CreatedDate) = 25 and CreatedByUserId = '7f2bacf6-564a-4272-a4bb-f76832476024'
