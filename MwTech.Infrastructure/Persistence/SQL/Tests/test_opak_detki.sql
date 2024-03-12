  SELECT top 150
      pr.ProductNumber, COUNT(*) as ile
  FROM [MwTech].[dbo].[Products] as pr
  inner join dbo.ProductVersions as v
  on v.ProductId = pr.Id
  Where v.DefaultVersion = 1
  group by pr.ProductNumber
  having COUNT(*) < 1
  order by ProductNumber

  /*
  select sets.ProductNumber as secik
  , part.ProductNumber as part
  , b.PartQty 
  from dbo.boms as b
  inner join 
  (SELECT 
      pr.id
  FROM [MwTech].[dbo].[Products] as pr
  inner join dbo.ProductVersions as v
  on v.ProductId = pr.Id
  Where v.DefaultVersion = 1
  group by pr.Id
  having COUNT(*) > 1
  ) as x
  on x.Id = b.SetId
 inner join dbo.Products as part
 on part.Id = b.PartId
 inner join dbo.Products as sets
 on sets.Id = b.SetId
 where part.ProductNumber = 'OPK-KARTON-01'
 */

  
