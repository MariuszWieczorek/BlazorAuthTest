CREATE OR ALTER VIEW mwtech_scada_properties_headers
AS
(
SELECT v.Id as versionId
      ,v.AlternativeNo
	  ,v.VersionNumber
      ,v.IsActive
	  ,v.DefaultVersion
	  ,v.IsAccepted01
	  ,v.IsAccepted02
      ,v.Name as versionName
      ,pr.ProductNumber
	  ,pr.TechCardNumber
	  ,pr.Name as ProductName
      ,v.Description
      ,v.Accepted01Date
	  ,trim(ua1.FirstName) + ' ' + TRIM(ua1.LastName) as Accepted01By
      ,v.Accepted02Date
	  ,trim(ua2.FirstName) + ' ' + TRIM(ua2.LastName) as Accepted02By
	  ,v.CreatedDate
	  ,trim(uc.FirstName) + ' ' + TRIM(uc.LastName) as CreatedBy
  FROM [MwTech].[dbo].ProductPropertyVersions as v
  inner join dbo.Products as pr
  on pr.Id = v.ProductId
  left join dbo.AspNetUsers as ua1
  on ua1.Id = v.Accepted01ByUserId
  left join dbo.AspNetUsers as ua2
  on ua2.Id = v.Accepted02ByUserId
  left join dbo.AspNetUsers as uc
  on uc.Id = v.CreatedByUserId
  Where v.IsActive = 1
)