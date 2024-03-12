/****** Script for SelectTopNRows command from SSMS  ******/
SELECT ca.CategoryNumber,p.ProductNumber,v.Name as versionName,v.DefaultVersion,v.IfsDefaultVersion,v.ProductQty
from dbo.ProductVersions as v
inner join dbo.Products as p
on p.Id = v.ProductId
inner join dbo.ProductCategories as ca
on ca.Id = p.ProductCategoryId
where ca.CategoryNumber in ('MIE','NAW','MIP','MIF','MIR')
and v.ToIfs = 1
