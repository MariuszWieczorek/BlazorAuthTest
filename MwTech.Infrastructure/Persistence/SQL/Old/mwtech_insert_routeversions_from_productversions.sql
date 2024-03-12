/****** Script for SelectTopNRows command from SSMS  ******/

SET IDENTITY_INSERT [MwTech].[dbo].[RouteVersions] ON

  insert into [MwTech].[dbo].[RouteVersions]
  (
       [Id]
      ,[VersionNumber]
      ,[DefaultVersion]
      ,[Name]
      ,[ProductId]
      ,[ProductQty]
      ,[Description]
      ,[IsAccepted01]
      ,[Accepted01ByUserId]
      ,[Accepted01Date]
      ,[IsAccepted02]
      ,[Accepted02ByUserId]
      ,[Accepted02Date]
      ,[CreatedDate]
      ,[CreatedByUserId]
      ,[ModifiedDate]
      ,[ModifiedByUserId]
  )
(
  SELECT [Id]
      ,[VersionNumber]
      ,[DefaultVersion]
      ,[Name]
      ,[ProductId]
      ,[ProductQty]
      ,[Description]
      ,[IsAccepted01]
      ,[Accepted01ByUserId]
      ,[Accepted01Date]
      ,[IsAccepted02]
      ,[Accepted02ByUserId]
      ,[Accepted02Date]
      ,[CreatedDate]
      ,[CreatedByUserId]
      ,[ModifiedDate]
      ,[ModifiedByUserId]
  FROM [MwTech].[dbo].[ProductVersions]
  where Id = 13971
  )

  SET IDENTITY_INSERT [MwTech].[dbo].[RouteVersions] OFF
  
  /*
  SELECT Id,ProductVersionId,RouteVersionId
  FROM [MwTech].[dbo].[ManufactoringRoutes]
  where ProductVersionId = 11397

  SELECT *
  FROM [MwTech].[dbo].ProductVersions
  where Id = 11397

  SELECT max(id)
  FROM [MwTech].[dbo].ProductVersions

  SELECT *
  FROM [MwTech].[dbo].RouteVersions
  where Id = 11397

  SELECT max(id) 
  FROM [MwTech].[dbo].RouteVersions
  
    
  update [MwTech].[dbo].[ManufactoringRoutes]
  set RouteVersionId = ProductVersionId 
  
  */