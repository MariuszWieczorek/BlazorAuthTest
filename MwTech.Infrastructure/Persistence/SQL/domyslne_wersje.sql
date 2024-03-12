
select z.VersionId, z.ProductNumber, z.AlternativeNo,z.VersionNumber, z.IsActive,z.DefaultVersion, z.CreatedDate, z.ile_wersji
from dbo.ProductVersions as vx
inner join
(
select y.VersionId, y.ProductNumber, y.AlternativeNo,y.VersionNumber, y.IsActive,y.DefaultVersion, y.CreatedDate, y.ile_wersji
from (
SELECT v.Id as VersionId, pr.ProductNumber, v.AlternativeNo,v.VersionNumber, v.IsActive,v.DefaultVersion, v.CreatedDate,
(select COUNT(*) 
  FROM [MwTech].[dbo].[ProductVersions] as vv
  inner join dbo.Products as prr
  on prr.Id = vv.ProductId
  inner join dbo.ProductCategories as caa
  on caa.Id = prr.ProductCategoryId
  where prr.Id = pr.Id and vv.AlternativeNo = v.AlternativeNo) as ile_wersji
  FROM [MwTech].[dbo].[ProductVersions] as v
  inner join dbo.Products as pr
  on pr.Id = v.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where v.VersionNumber = 2 and ca.CategoryNumber = 'OBC' 
  ) as y
  where y.ile_wersji > 1
  ) as z
  on z.VersionId = vx.Id
  

  
UPDATE vx
set DefaultVersion = 0, IsActive = 0
from dbo.ProductVersions as vx
inner join
(
select y.VersionId, y.ProductNumber, y.AlternativeNo,y.VersionNumber, y.IsActive,y.DefaultVersion, y.CreatedDate, y.ile_wersji
from (
SELECT v.Id as VersionId, pr.ProductNumber, v.AlternativeNo,v.VersionNumber, v.IsActive,v.DefaultVersion, v.CreatedDate,
(select COUNT(*) 
  FROM [MwTech].[dbo].[ProductVersions] as vv
  inner join dbo.Products as prr
  on prr.Id = vv.ProductId
  inner join dbo.ProductCategories as caa
  on caa.Id = prr.ProductCategoryId
  where prr.Id = pr.Id and vv.AlternativeNo = v.AlternativeNo) as ile_wersji
  FROM [MwTech].[dbo].[ProductVersions] as v
  inner join dbo.Products as pr
  on pr.Id = v.ProductId
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where v.VersionNumber = 1 and ca.CategoryNumber = 'OBC' 
  ) as y
  where y.ile_wersji > 1
  ) as z
  on z.VersionId = vx.Id