
/* Kategorie zaszeregowania */
/****** Script for SelectTopNRows command from SSMS  ******/
SELECT r.ResourceNumber
      ,r.Name
      ,ca.CategoryNumber
      ,cast( replace(r.Cost,'.',',') as varchar(10)) as cost
      ,u.UnitCode
      ,cast( replace(r.Markup,'.',',') as varchar(10)) as Markup
	  ,r.Description
  FROM [MwTech].[dbo].[Resources] as r
  left join dbo.Units as u
  on u.id = r.UnitId
  left join dbo.ProductCategories as ca
  on ca.Id = r.ProductCategoryId
  where r.cost != 0