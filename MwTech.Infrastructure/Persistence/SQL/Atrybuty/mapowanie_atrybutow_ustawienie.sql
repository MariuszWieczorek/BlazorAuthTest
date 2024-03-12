SELECT m.id, m.ProductCategoryId, ca.CategoryNumber, m.PropertyId, pp.PropertyNumber
  FROM [MwTech].[dbo].[PropertiesProductCategoriesMaps] as m
  inner join dbo.ProductCategories as ca
  on ca.id = m.ProductCategoryId
  inner join dbo.Properties as pp
  on pp.id = m.PropertyId

  /*
  insert into [dbo].[PropertiesProductCategoriesMaps]
  ( ProductCategoryId, PropertyId)
  values
  ( 25, 29)
  */

  select * from ProductCategories where CategoryNumber = 'DAP'
  select * from dbo.Properties where name like '%grubo%'


