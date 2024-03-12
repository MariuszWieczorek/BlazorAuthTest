/****** Script for SelectTopNRows command from SSMS  ******/
  SELECT *
  FROM [MwTech].[dbo].[ProductVersions]
  where Accepted01ByUserId is not null
  and CreatedByUserId is null

  update x
  set CreatedByUserId = Accepted01ByUserId,
  CreatedDate = Accepted01Date
  FROM [MwTech].[dbo].[ProductVersions] as x
  where Accepted01ByUserId is not null
  and CreatedByUserId is null


  