/* 2023.10.19 */
/* Porównanie Marszrut wszystko porócz dêtek */


use MWTech;

with cte as
(
select *
 
 /*
 update rrr
  set ChangeOverLabourConsumption = IfsLaborSetupTime,
  ChangeOverMachineConsumption = IfsMachSetupTime,
  ChangeoverResourceId = (select id from dbo.Resources as wo where wo.ResourceNumber = x.IfsSetupLaborClassNo ),
  ChangeOverNumberOfEmployee = isnull(x.IfsSetupCrewSize,0),
  MoveTime = IfsMoveTime,
  Overlap = IfsOverlap,
  
  OperationLabourConsumption = x.IfsLaborRunFactor,
  OperationMachineConsumption  = x.IfsMachRunFactor,
  ResourceQty = x.IfsCrewSize,
  WorkCenterId = (select id from dbo.Resources as wo where wo.ResourceNumber = x.IfsWorkCenterNo ),
  ResourceId = (select id from dbo.Resources as wo where wo.ResourceNumber = x.IfsLaborClassNo )
  */
FROM (
SELECT 
 ca.CategoryNumber
,IFS.PartNo as IfsPartNo
,MWT.PART_NO as MwtPartNo
--
,IFS.RevisionNo as IfsRevisionNo
,MWT.ROUTING_REVISION as MwtRevisionNo
--
,IFS.AlternativeNo as IfsAlternativeNo
,MWT.ALTERNATIVE_NO as MwtAlternativeNo
--
,IFS.OperationNo as IfsOperationNo
,MWT.OPERATION_NO as MwtOperationNo
--
,IFS.OperationDescription as IfsOperationDescription
,MWT.OPERATION_DESCRIPTION as MwtOperationDescription
--
,IFS.WorkCenterNo as IfsWorkCenterNo
,MWT.WORK_CENTER_NO as MwtWorkCenterNo
--
,IFS.LaborClassNo as IfsLaborClassNo
,MWT.LABOR_CLASS_NO as MwtLaborClassNo
--
,IFS.LaborRunFactor as IfsLaborRunFactor
,MWT.LABOR_RUN_FACTOR as MwtLaborRunFactor
--
,IFS.MachRunFactor as IfsMachRunFactor
,MWT.MACH_RUN_FACTOR as MwtMachRunFactor
--
,IFS.CrewSize as IfsCrewSize
,MWT.CREW_SIZE as MwtCrewSize
--
,IFS.SetupLaborClassNo as IfsSetupLaborClassNo
,MWT.SETUP_LABOR_CLASS_NO as MwtSetupLaborClassNo
--
,IFS.SetupCrewSize as IfsSetupCrewSize
,MWT.SETUP_CREW_SIZE as MwtSetupCrewSize
--
,IFS.MachSetupTime as IfsMachSetupTime
,MWT.MACH_SETUP_TIME as MwtMachSetupTime
--
,IFS.LaborSetupTime as IfsLaborSetupTime
,MWT.LABOR_SETUP_TIME as MwtLaborSetupTime
--
,IFS.MoveTime as IfsMoveTime
,MWT.MOVE_TIME as MwtMoveTime
--
,IFS.Overlap as IfsOverlap
,MWT.OVERLAP as MwtOverlap
--
,IFS.RunTimeCode as IfsRunTimeCode
,MWT.RUN_TIME_CODE as MwtRunTimeCode
--
,MWT.ALTERNATIVE_DESCRIPTION as MWT_ALTERNATIVE_DESCRIPTION
--
,MWT.RoutePositionId
--
,IIF( IFS.OperationDescription = MWT.OPERATION_DESCRIPTION,1,0) as OPERATION_DESCRIPTION_TEST
,IIF( IFS.WorkCenterNo = MWT.WORK_CENTER_NO,1,0) as WorkCenterNoTest
,IIF( IFS.LaborClassNo = MWT.LABOR_CLASS_NO,1,0) as LaborClassNoTest
,IIF( IFS.CrewSize = MWT.CREW_SIZE,1,0) as CrewSizeTest
,IIF( IFS.MachRunFactor = ROUND(MWT.MACH_RUN_FACTOR,5),1,0) as MachRunFactorTest
,IIF( IFS.LaborRunFactor = ROUND(MWT.LABOR_RUN_FACTOR,5),1,0) as LaborRunFactorTest
--
,IIF( ISNULL(IFS.SetupCrewSize,0) = ISNULL(MWT.SETUP_CREW_SIZE,0) ,1,0) as SetupCrewSizeTest
,IIF( ISNULL(IFS.SetupLaborClassNo,'') = ISNULL(MWT.SETUP_LABOR_CLASS_NO,'') ,1,0) as SetupLaborClassNoTest
,IIF( ISNULL(IFS.MachSetupTime,0) = ISNULL(MWT.MACH_SETUP_TIME,0),1,0) as MachSetupTimeTest
,IIF( ISNULL(IFS.LaborSetupTime,0) = ISNULL(MWT.LABOR_SETUP_TIME,0) ,1,0) as LaborSetupTimeTest
,IIF( ISNULL(IFS.Overlap,0) = ISNULL(MWT.OVERLAP,0) ,1,0) as OverlapTest
,IIF( ISNULL(IFS.MoveTime,0) = ISNULL(MWT.MOVE_TIME,0) ,1,0) as MoveTimeTest
--
FROM dbo.IfsRoutes as IFS
left join dbo.Products as pr
on pr.ProductNumber = IFS.PartNo
left join dbo.ProductCategories as ca
on ca.Id = pr.ProductCategoryId
left join dbo.MWTech_route_IFS as MWT
ON  MWT.PART_NO = IFS.PartNo
AND MWT.ROUTING_REVISION = IFS.RevisionNo
AND MWT.ALTERNATIVE_NO = IFS.AlternativeNo 
AND MWT.OPERATION_NO = IFS.OperationNo
WHERE 1 = 1 
AND (ISNUMERIC(ifs.AlternativeNo ) = 1 OR ifs.AlternativeNo = '*')
AND (ISNUMERIC(ifs.RevisionNo ) = 1 OR ifs.RevisionNo = '*')
AND ca.RouteSource = 0
AND ifs.PartNo not in (select ProductNumber from dbo.IFS_black_list)
) AS x
-- INNER JOIN dbo.ManufactoringRoutes as rrr
-- on rrr.id = x.RoutePositionId
WHERE 1 = 1
-- AND x.RoutePositionId IS NOT NULL
AND
(	
	 x.WorkCenterNoTest = 0
 OR  x.LaborClassNoTest = 0
 OR  x.CrewSizeTest = 0
 OR  x.MachRunFactorTest = 0
 OR  x.LaborRunFactorTest = 0
 
 OR  x.SetupCrewSizeTest = 0
 OR  x.SetupLaborClassNoTest = 0
 OR  x.OverlapTest = 0
 OR  x.MoveTimeTest = 0
 OR  x.MachSetupTimeTest = 0
 OR  x.LaborSetupTimeTest = 0
 
)

--   and x.RoutePositionId in 
-- (
-- )
-- ORDER BY x.IFS_part_no,x.IFS_ALTERNATIVE_NO

--and ( CategoryNumber like 'BOL%')
-- and IfsPartNo = 'PGK210/A262'

)

select CategoryNumber, IfsOperationNo,IfsOperationDescription, IfsSetupLaborClassNo, IfsSetupCrewSize, count(*) as items from cte
group by CategoryNumber, IfsOperationNo,IfsOperationDescription, IfsSetupLaborClassNo, IfsSetupCrewSize
order by CategoryNumber, IfsOperationNo
