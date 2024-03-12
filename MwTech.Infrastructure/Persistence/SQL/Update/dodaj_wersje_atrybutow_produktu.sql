/****** Script for SelectTopNRows command from SSMS  ******/
SELECT 
        *
  FROM [MwTech].[dbo].[ProductProperties] as p
  inner join [MwTech].[dbo].[ProductPropertyVersions] as v
  on v.ProductId = p.ProductId

  update p
  set ProductPropertiesVersionId = v.id
  FROM [MwTech].[dbo].[ProductProperties] as p
  inner join [MwTech].[dbo].[ProductPropertyVersions] as v
  on v.ProductId = p.ProductId


  




  /*
  
  insert into [MwTech].[dbo].[ProductPropertyVersions]
  (
  VersionNumber,
  Name,
  DefaultVersion,
  ProductId,
  CreatedByUserId,
  CreatedDate,
  IsAccepted01,
  IsAccepted02
  )
  (
  SELECT 
        1 as VersionNumber,
	  'wersja 1',
	  1 as DefaultVersion, 
	   ProductId,
	  '0f043573-46eb-43ff-8288-388c062a6759' as CreatedByUser,
	  GETDATE() as CreatedDate,
	   0 as IsAccepted01,
	   0 as IsAccepted02
  FROM [MwTech].[dbo].[ProductProperties]
  group by ProductId
  )
  */
