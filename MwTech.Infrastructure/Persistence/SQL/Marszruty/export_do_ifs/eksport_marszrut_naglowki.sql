-- nag³ówki Marszrut --
-- Uwaga nie zmieniaæ klauzuli ORDER BY !

DECLARE @PageNo			 INT = 1;
DECLARE @PageSize		 INT = 5000;
DECLARE @NoOfRowsToSkip  INT = (@PageNo - 1) * @PageSize;
DECLARE @NoOfRowsToFetch INT =  @PageSize;

SELECT 
       PART_NO
      ,ALTERNATIVE_NO
      ,ALTERNATIVE_DESCRIPTION
      ,BOM_TYPE_DB
      ,CONTRACT
      ,ROUTING_REVISION
	  ,[RouteVersionId]
	  ,@PageNo as PageNo
      ,[ExportDate]
  FROM [MwTech].[dbo].[mwtech_route_ifs_headers]
  ORDER BY [ProductCategory],[PART_NO],[VersionNo],[AlternativeNo],[RouteVersionId]
  OFFSET @NoOfRowsToSkip ROWS
  FETCH NEXT @NoOfRowsToFetch ROWS ONLY


