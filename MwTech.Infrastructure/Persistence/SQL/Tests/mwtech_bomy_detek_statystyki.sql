
  

    select y.CategoryNumber, COUNT(*) as ile_indeksow , y.ile as ile_czynnosci
  from (
  select x.ile, x.CategoryNumber
  from 
  (select pr.Id, count(*) as ile, ca.CategoryNumber
  FROM [MwTech].[dbo].ManufactoringRoutes as b
  inner join [MwTech].[dbo].RouteVersions as pv
  on pv.Id = b.RouteVersionId
  inner join [MwTech].[dbo].Products as pr
  on pr.Id = pv.ProductId
  inner join [MwTech].[dbo].ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber IN ('DWY','DOB','DAP','DST','DWU','DKJ','DET')
  group by pr.Id, ca.CategoryNumber) as x  ) as y
  group by y.ile, y.CategoryNumber
  order by y.CategoryNumber, count(*) desc


    select COUNT(*) as ile_indeksow
  from (
  select pr.Id
  FROM [MwTech].[dbo].ManufactoringRoutes as b
  inner join [MwTech].[dbo].RouteVersions as pv
  on pv.Id = b.RouteVersionId
  inner join [MwTech].[dbo].Products as pr
  on pr.Id = pv.ProductId
  inner join [MwTech].[dbo].ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber IN ('DWY','DOB','DAP','DST','DWU','DKJ','DET')
  group by pr.Id ) as x

    select COUNT(*)
  FROM [MwTech].[dbo].ManufactoringRoutes as b
  inner join [MwTech].[dbo].RouteVersions as pv
  on pv.Id = b.RouteVersionId
  inner join [MwTech].[dbo].Products as pr
  on pr.Id = pv.ProductId
  inner join [MwTech].[dbo].ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber IN ('DWY','DOB','DAP','DST','DWU','DKJ','DET')


  /*
  DELETE [MwTech].[dbo].ManufactoringRoutes
  FROM [MwTech].[dbo].ManufactoringRoutes as b
  inner join [MwTech].[dbo].RouteVersions as pv
  on pv.Id = b.RouteVersionId
  inner join [MwTech].[dbo].Products as pr
  on pr.Id = pv.ProductId
  inner join [MwTech].[dbo].ProductCategories as ca
  on ca.Id = pr.ProductCategoryId
  where ca.CategoryNumber IN ('DWY','DOB','DAP','DST','DWU','DKJ','DET')
  */