  select y.CategoryNumber, COUNT(*) as ile_indeksow , y.ile as ile_komponentow
  from (
  select x.ile, x.CategoryNumber
  from 
  (select b.SetId, count(*) as ile, ca.CategoryNumber
  FROM [MwTech].[dbo].[Boms] as b
  inner join [MwTech].[dbo].ProductVersions as pv
  on pv.Id = b.SetVersionId
  inner join [MwTech].[dbo].Products as pr
  on pr.Id = pv.ProductId and pr.Id = b.SetId
  inner join [MwTech].[dbo].ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber IN ('DWY','DOB','DAP','DST','DWU','DKJ','DET')
  group by b.SetId, ca.CategoryNumber) as x  ) as y
  group by y.ile, y.CategoryNumber
  order by y.CategoryNumber, count(*) desc

  select COUNT(*) as ile_indeksow
  from (
  select b.SetId
  FROM [MwTech].[dbo].[Boms] as b
  inner join [MwTech].[dbo].ProductVersions as pv
  on pv.Id = b.SetVersionId
  inner join [MwTech].[dbo].Products as pr
  on pr.Id = pv.ProductId and pr.Id = b.SetId
  inner join [MwTech].[dbo].ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber IN ('DWY','DOB','DAP','DST','DWU','DKJ','DET')
  group by b.SetId ) as x

  select COUNT(*) as ile_komponentow
  FROM [MwTech].[dbo].[Boms] as b
  inner join [MwTech].[dbo].ProductVersions as pv
  on pv.Id = b.SetVersionId
  inner join [MwTech].[dbo].Products as pr
  on pr.Id = pv.ProductId and pr.Id = b.SetId
  inner join [MwTech].[dbo].ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber IN ('DWY','DOB','DAP','DST','DWU','DKJ','DET')