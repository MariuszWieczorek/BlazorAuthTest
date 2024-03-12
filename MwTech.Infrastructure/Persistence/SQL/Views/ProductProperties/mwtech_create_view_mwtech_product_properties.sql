/* Export w³aœciwoœci do IFS */

CREATE OR ALTER VIEW mwtech_product_properties
AS
(
SELECT ca.CategoryNumber 
	  ,ca.Name as CategoryName 	
	  ,pr.ProductNumber
	  ,pr.Name as ProductName
	  ,prop.PropertyNumber
	  ,prop.Name as PropertyName
	  ,v.VersionNumber
	  ,v.AlternativeNo
	  ,v.DefaultVersion
	  ,v.IsActive
	  ,cast(coalesce(p.Text,'') as varchar(200)) as text
      ,p.MinValue
	  ,p.Value
      ,p.MaxValue
	  ,u.Name as UnitName
	  ,pr.OldProductNumber
	  ,pr.Idx01
	  ,pr.Idx02
	  ,prop.ProductCategoryId as PropertyProductCategoryId
  FROM [MwTech].[dbo].[ProductProperties] as p
  inner join [MwTech].[dbo].[ProductPropertyVersions] as v
  on v.Id = p.ProductPropertiesVersionId
  inner join dbo.Products as pr
  on pr.Id = v.ProductId
  inner join dbo.Properties as prop
  on prop.Id = p.PropertyId 
  inner join dbo.ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  inner join dbo.Units as u
  on u.Id = prop.UnitId
)


-- select * from mwtech_route_ifs order by SetCategory, nr_pozycji, numer_operacji
-- select * from mwtech_bom_ifs order by SetCategory, nr_pozycji_nadrzednej, numer_pozycji_w_linii


