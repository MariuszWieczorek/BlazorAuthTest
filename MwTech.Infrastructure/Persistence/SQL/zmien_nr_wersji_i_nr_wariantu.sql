/****** Script for SelectTopNRows command from SSMS  ******/
select pr.ProductNumber, v.AlternativeNo, v.VersionNumber, v.DefaultVersion
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.ProductVersions as v
on v.ProductId = pr.Id
where ca.CategoryNumber = 'DET-LUZ'
and v.DefaultVersion = 1

/*
UPDATE v
SET VersionNumber = 2, AlternativeNo = 1
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.ProductVersions as v
on v.ProductId = pr.Id
where ca.CategoryNumber = 'DET-LUZ'
and v.DefaultVersion = 1
*/