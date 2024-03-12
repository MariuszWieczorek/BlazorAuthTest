-- Pozycje Marszrut --
-- Uwaga nie zmieniaæ klauzuli ORDER BY !

DECLARE @PageNo			 INT = 1;
DECLARE @PageSize		 INT = 5000;
DECLARE @NoOfRowsToSkip  INT = (@PageNo - 1) * @PageSize;
DECLARE @NoOfRowsToFetch INT =  @PageSize;

SELECT CONTRACT
      ,CategoryNumber
      ,PART_NO
      ,ALTERNATIVE_NO
      ,ALTERNATIVE_DESCRIPTION
      ,VersionNo
      ,AlternativeNo
      ,IsActive
      ,IsDefaultVersion
      ,RouteCategory
      ,OPERATION_NO
      ,OPERATION_DESCRIPTION
      ,WORK_CENTER_NO
      ,MACH_SETUP_TIME
      ,LABOR_SETUP_TIME
      ,SETUP_LABOR_CLASS_NO
      ,SETUP_CREW_SIZE
      ,MACH_RUN_FACTOR
      ,LABOR_RUN_FACTOR
      ,RUN_TIME_CODE
      ,LABOR_CLASS_NO
      ,CREW_SIZE
      ,MOVE_TIME
      ,OVERLAP
      ,OVERLAP_UNIT
      ,PARALLEL_OPERATION
      ,BOM_TYPE
      ,BOM_TYPE_DB
      ,ProductCategoryName
      ,OperationName
      ,Client
      ,Idx01
      ,Idx02
	  ,RoutePositionId
	  ,@PageNo as PageNo
      ,ExportDate
  FROM [MwTech].[dbo].[mwtech_route_ifs]
  WHERE 1 = 1
--  AND PART_NO
--   IN ('SBA.D10/240'
--      ,'SBA.D10/250'
--      ,'SBA.D11/240'
--    )
  ORDER BY CategoryNumber,PART_NO,VersionNo,AlternativeNo,OPERATION_NO,RoutePositionId
  OFFSET @NoOfRowsToSkip ROWS
  FETCH NEXT @NoOfRowsToFetch ROWS ONLY
  


