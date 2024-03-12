use MwTech;

declare @matka as varchar(20) = '10075153V';

select x.idx01, x.ToolQuantity, COUNT(*) as ile
from (
SELECT pr.idx01,ifs.ToolId, ifs.ToolQuantity
  FROM [MwTech].[dbo].[IfsRoutes] as ifs
  inner join dbo.Products as pr
  on ifs.PartNo = pr.ProductNumber
  inner join dbo.ProductCategories as ca
  on ca.id = pr.ProductCategoryId
  where pr.Idx01 is not null and pr.Idx02 IS NOT NULL and ifs.ToolId is not null
  and pr.IsActive = 1
  and ca.RouteSource = 1
  group by ifs.ToolId ,pr.Idx01, ifs.ToolQuantity
  ) as x
  group by x.idx01, x.ToolQuantity
  having COUNT(*) > 1


 


  select *
  from (
  SELECT pr.idx01, pr.ProductNumber, isnull(ifs.ToolId,'null') as ToolId
  FROM [MwTech].[dbo].[IfsRoutes] as ifs
  inner join dbo.Products as pr
  on ifs.PartNo = pr.ProductNumber
  where pr.Idx01 = @matka
    and pr.IsActive = 1
  )
  as x
  group by x.idx01, x.ProductNumber, x.ToolId
  order by x.idx01, x.ProductNumber


  select x.idx01, x.ToolId, count(*) as ile
  from (
  SELECT pr.idx01, pr.ProductNumber, isnull(ifs.ToolId,'null') as ToolId
  FROM [MwTech].[dbo].[IfsRoutes] as ifs
  inner join dbo.Products as pr
  on ifs.PartNo = pr.ProductNumber
  where pr.Idx01 = @matka
    and pr.IsActive = 1
  group by pr.idx01, pr.ProductNumber, isnull(ifs.ToolId,'null')
  )
  as x
  group by x.idx01, x.ToolId
  order by x.idx01

