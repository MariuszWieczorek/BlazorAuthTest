/****** Script for SelectTopNRows command from SSMS  ******/
SELECT COUNT(*)
  FROM [MwTech].[dbo].[Boms]

  select *
  from
  (
  SELECT b.SetId
	  ,pr.ProductNumber
	  ,b.SetVersionId
      , COUNT(*)  as ile_w_bom
   
  
  From [MwTech].[dbo].[Products] as pr
  inner join [MwTech].[dbo].[ProductVersions] as pv
  on pv.ProductId = pr.Id
  inner join [MwTech].[dbo].[ProductCategories] as pc
  on pc.Id = pr.ProductCategoryId
  
  left join [MwTech].[dbo].[Boms] as b
  on pv.Id = b.SetVersionId and pv.ProductId = b.SetId

  where pc.CategoryNumber in ('DET','DWY','DOB','DAP','DST','DKJ','DWU')
  group by
   b.SetId
  ,pr.ProductNumber
  ,b.SetVersionId
  ) as x
  where x.SetId is null
  order by ProductNumber

  -- where pc.CategoryNumber in ('DET','DWY','DOB','DAP','DST','DKJ','DWU')