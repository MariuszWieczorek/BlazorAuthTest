/****** Script for SelectTopNRows command from SSMS  ******/
/*
select pr.ProductNumber
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
where ca.CategoryNumber in ('OBB','OBC')
inner join (
*/

SELECT pp.ProductId, pp.VersionId
      ,pp.[ProductNumber]
      ,pp.[ProductName]
      ,pp.[PropertyNumber]
      ,pp.[ScadaPropertyNumber]
      ,pp.[PropertyName]
      ,pp.[MinValue]
      ,pp.[Value]
      ,pp.[MaxValue]
      ,pp.[Unit]
      ,pp.[Text]
    from dbo.Products as pr
	inner join dbo.ProductCategories as ca
	on ca.Id = pr.ProductCategoryId and ca.CategoryNumber in ('OBB','OBC')
    left join dbo.mwtech_scada_properties_positions as pp
    on pp.ProductId = pr.Id
    where pp.PropertyNumber like '%listwa%'
	order by pp.[ProductNumber]

