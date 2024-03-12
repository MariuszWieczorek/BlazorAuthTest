use MwTech;

declare @waz as varchar(20) = '10075153TR13';
/*
IF OBJECT_ID('dbo.temp','U') IS NOT NULL
	DROP TABLE dbo.temp;

  create table dbo.temp
  (
	matka varchar(30),
	tool varchar(30)
  )
  
  insert into dbo.temp (matka,tool) values ('10075153TR13','FODE-10075153')
  insert into dbo.temp (matka,tool) values ('10075153TR15','FODE-10075153')
  insert into dbo.temp (matka,tool) values ('10075153TR15C','FODE-10075153')
  insert into dbo.temp (matka,tool) values ('10075153TR15D','FODE-10075153')
  insert into dbo.temp (matka,tool) values ('10075153TR218','FODE-10075153')
  insert into dbo.temp (matka,tool) values ('10075153TR218C','FODE-10075153')
  insert into dbo.temp (matka,tool) values ('10075153TR75','FODE-10075153')
  insert into dbo.temp (matka,tool) values ('10075153TR78','FODE-10075153')
  insert into dbo.temp (matka,tool) values ('10075153V','FODE-10075153')
  insert into dbo.temp (matka,tool) values ('1258018V','FODE-1258018')
  insert into dbo.temp (matka,tool) values ('1505517TR15','FODE-1505517')
  insert into dbo.temp (matka,tool) values ('1505517TR218','FODE-1505517')
  insert into dbo.temp (matka,tool) values ('17518515V','FODE-17518515')
  insert into dbo.temp (matka,tool) values ('1904517TR15','FODE-1904517')
  insert into dbo.temp (matka,tool) values ('1904517TR218','FODE-1904517')
  insert into dbo.temp (matka,tool) values ('19520514V','FODE-19520514')
  insert into dbo.temp (matka,tool) values ('30018GP4','FODE-30018')
  insert into dbo.temp (matka,tool) values ('30018GP4K','FODE-30018')
  insert into dbo.temp (matka,tool) values ('30019GP4','FODE-30019')
  insert into dbo.temp (matka,tool) values ('30019GP4K','FODE-30019')
  insert into dbo.temp (matka,tool) values ('50055060225PK145','FODE-50055060225')
  insert into dbo.temp (matka,tool) values ('50055060225TR218','FODE-50055060225')
  insert into dbo.temp (matka,tool) values ('50055060225TR218C','FODE-50055060225')
  insert into dbo.temp (matka,tool) values ('50055060225VFI70','FODE-50055060225')
  insert into dbo.temp (matka,tool) values ('70050265TR218','FODE-70050265')
  insert into dbo.temp (matka,tool) values ('7107042TR218','FODE-7107042')
  insert into dbo.temp (matka,tool) values ('75016VD','FODE-75016')
  */


-- SELECT pr.idx01, pr.ProductNumber, isnull(ifs.ToolId,'null') as ToolId, t.tool
  /*
  update ifs set ToolId = t.tool 
  FROM [MwTech].[dbo].[IfsRoutes] as ifs
  inner join dbo.Products as pr
  on ifs.PartNo = pr.ProductNumber
  inner join dbo.temp as t
  on t.matka = pr.Idx01
  where 1 = 1
	 and ifs.ToolId != t.tool 
  -- and pr.Idx01 = @matka
     and pr.IsActive = 1
	 and toolid is not null
*/

SELECT x.idx02,x.ToolId,COUNT(*)
FROM(
SELECT pr.idx02,ifs.ToolId
  FROM [MwTech].[dbo].[IfsRoutes] as ifs
  inner join dbo.Products as pr
  on ifs.PartNo = pr.ProductNumber
  inner join dbo.ProductCategories as ca
  on ca.id = pr.ProductCategoryId
  where 1 = 1
  AND pr.Idx01 IS NULL
  and pr.Idx02 IS NOT NULL
  and ifs.ToolId IS NOT NULL
  and pr.IsActive = 1
  and ca.RouteSource = 2
  group by ifs.ToolId ,pr.Idx02
  ) as x
  group by x.ToolId ,x.Idx02
  having COUNT(*) > 1

--select ca.CategoryNumber, pr.ProductNumber, r.RoutingToolId, r.ToolQuantity, i.ToolId, r.id as PositionId, (select Id from dbo.RoutingTools where ToolNumber = i.toolid) as tool, i.ToolQuantity
-- update r SET RoutingToolId =  (select Id from dbo.RoutingTools where ToolNumber = i.toolid), ToolQuantity = i.ToolQuantity
update r SET RoutingToolId =  (select Id from dbo.RoutingTools where ToolNumber = 'SZWD-225/Z'), ToolQuantity = i.ToolQuantity
from dbo.Products as pr
inner join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
inner join dbo.RouteVersions as v
on v.ProductId = pr.id
inner join dbo.ManufactoringRoutes as r
on r.RouteVersionId = v.id
inner join 
(
SELECT pr.idx02,ifs.ToolId, ifs.ToolQuantity, ifs.WorkCenterNo
  FROM [MwTech].[dbo].[IfsRoutes] as ifs
  inner join dbo.Products as pr
  on ifs.PartNo = pr.ProductNumber
  inner join dbo.ProductCategories as ca
  on ca.id = pr.ProductCategoryId
  where 1 = 1
  --AND pr.Idx01 IS NULL
  and pr.Idx02 IS NOT NULL
  and ifs.ToolId IS NOT NULL
  and pr.IsActive = 1
  and ca.RouteSource = 2
  group by ifs.ToolId ,pr.Idx02, ifs.ToolQuantity, ifs.WorkCenterNo
) as i
on i.Idx02 = pr.ProductNumber
where 1 = 1
AND v.ProductCategoryId = (select Id from dbo.ProductCategories where CategoryNumber = 'DWY')
AND r.WorkCenterId = (select Id from dbo.Resources where ResourceNumber = i.WorkCenterNo)
-- AND ca.RouteSource = 1 
-- and r.RoutingToolId is not null
-- and ca.CategoryNumber not in ( 'DWY','DWU' )
AND i.Idx02 = '0148'

SELECT pr.idx02,ifs.ToolId
  FROM [MwTech].[dbo].[IfsRoutes] as ifs
  inner join dbo.Products as pr
  on ifs.PartNo = pr.ProductNumber
  inner join dbo.ProductCategories as ca
  on ca.id = pr.ProductCategoryId
  where 1 = 1
 -- AND pr.Idx01 IS NULL
  and pr.Idx02 IS NOT NULL
  and ifs.ToolId IS NOT NULL
  and pr.IsActive = 1
  and ca.RouteSource = 2
  and pr.idx02 = '818'	
  group by ifs.ToolId ,pr.Idx02
  