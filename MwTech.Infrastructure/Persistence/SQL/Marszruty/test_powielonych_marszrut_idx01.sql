  use MwTech;
  
  SELECT idx01, [CategoryNumber], part_no, alternative_no, versionno, operation_no 
  , COUNT(*) as ile
  FROM dbo.mwtech_route_ifs_idx01
  -- where operation_no = 80
  group by idx01, [CategoryNumber], part_no, alternative_no, versionno, operation_no
    having COUNT(*) > 1
	order by [CategoryNumber], idx01


	  SELECT idx02, [CategoryNumber], part_no, alternative_no, versionno, operation_no 
  , COUNT(*) as ile
  FROM dbo.mwtech_route_ifs_idx02
  -- where operation_no = 80
  group by idx02, [CategoryNumber], part_no, alternative_no, versionno, operation_no
    having COUNT(*) > 1
	order by [CategoryNumber], idx02
  

