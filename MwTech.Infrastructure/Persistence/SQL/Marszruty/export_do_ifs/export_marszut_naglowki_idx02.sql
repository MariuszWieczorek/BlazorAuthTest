-- Pozycje Marszrut Dla Idx02 --
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
  PART_NO
, ALTERNATIVE_NO
, ALTERNATIVE_DESCRIPTION
, BOM_TYPE, BOM_TYPE_DB
, IIF(ALTERNATIVE_DESCRIPTION = 'WAZ01','KT2',CONTRACT) as CONTRACT
, ROUTING_REVISION
FROM dbo.mwtech_route_ifs_idx02_headers
WHERE 1 = 1
  ORDER BY ProductCategory,PART_NO,versionNumber,alternativeNo,RouteVersionId
  OFFSET @NoOfRowsToSkip ROWS
  FETCH NEXT @NoOfRowsToFetch ROWS ONLY
