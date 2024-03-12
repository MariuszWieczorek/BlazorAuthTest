/****** Script for SelectTopNRows command from SSMS  ******/

-- delete r
select *
from dbo.ManufactoringRoutes as r
inner join 
(
SELECT max(Id) as maxid, COUNT(*) as ile
      ,[OperationId]
      ,[ResourceId]
      ,[OrdinalNumber]
      ,[ResourceQty]
      ,[WorkCenterId]
      ,[OperationLabourConsumption]
      ,[OperationMachineConsumption]
      ,[ChangeOverLabourConsumption]
      ,[ChangeOverMachineConsumption]
      ,[ChangeOverNumberOfEmployee]
      ,[ChangeOverResourceId]
      ,[RouteVersionId]
      ,[MoveTime]
      ,[Overlap]
      ,[Description]
      ,[ProductCategoryId]
  FROM [MwTech].[dbo].[ManufactoringRoutes]
  group by 
       [OperationId]
      ,[ResourceId]
      ,[OrdinalNumber]
      ,[ResourceQty]
      ,[WorkCenterId]
      ,[OperationLabourConsumption]
      ,[OperationMachineConsumption]
      ,[ChangeOverLabourConsumption]
      ,[ChangeOverMachineConsumption]
      ,[ChangeOverNumberOfEmployee]
      ,[ChangeOverResourceId]
      ,[RouteVersionId]
      ,[MoveTime]
      ,[Overlap]
      ,[Description]
      ,[ProductCategoryId]
	  having COUNT(*) > 1
	  ) as x
	  on x.maxid = r.id