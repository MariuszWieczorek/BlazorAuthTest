-- Pozycje Marszrut Dla idx02 --
-- MwTech 2023.07.07
-- Marszruty Dla Dêtek pobrane z Marszrut Wê¿y
-- Uwaga nie zmieniaæ klauzuli ORDER BY !

DECLARE @PageNo			 INT = 1;
DECLARE @PageSize		 INT = 5000;
--
DECLARE @NoOfRowsToSkip  INT = (@PageNo - 1) * @PageSize;
DECLARE @NoOfRowsToFetch INT =  @PageSize;


USE MwTech;

SELECT
  IIF(WORK_CENTER_NO = 'WAZ01','KT2',CONTRACT) as CONTRACT
, PART_NO
, ALTERNATIVE_NO
, OPERATION_NO
, OPERATION_DESCRIPTION
, WORK_CENTER_NO
-- SETUP / przezbrojenie
, MACH_SETUP_TIME
, LABOR_SETUP_TIME
, SETUP_LABOR_CLASS_NO
, SETUP_CREW_SIZE
-- RUN / wykonanie
, MACH_RUN_FACTOR
, LABOR_RUN_FACTOR
, RUN_TIME_CODE
, LABOR_CLASS_NO
, CREW_SIZE
--
, MOVE_TIME
, OVERLAP
, OVERLAP_UNIT
, PARALLEL_OPERATION
, BOM_TYPE
, BOM_TYPE_DB
--
, ROUTING_REVISION
FROM dbo.mwtech_route_ifs_idx02
WHERE 1 = 1
  ORDER BY CategoryNumber,PART_NO,VersionNo,AlternativeNo,OPERATION_NO,RouteVersionId,RoutePositionId
  OFFSET @NoOfRowsToSkip ROWS
  FETCH NEXT @NoOfRowsToFetch ROWS ONLY
