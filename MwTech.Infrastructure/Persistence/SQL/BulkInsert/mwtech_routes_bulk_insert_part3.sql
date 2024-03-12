/* import bomów na podstawie dbo.import_boms czêœæ 3
   dodanie do wersji produktu bomów 
   uwaga na domyœlne wersji i warianty
   */

use MwTech;

select 
	   v.id as [RouteVersionId]
	  ,o.id as OperationId
	  ,r.Id as ResourceId
	  ,w.Id as WorkCenterId
	  ,i.WorkCenterNumber
	  ,co.Id as ChangeOverResourceId
	  ---
	  ,i.OrdinalNumber as OrdinalNumber
	  ---
      ,i.OperationLabourConsumption
      ,i.OperationMachineConsumption
 	  ,i.ResourceQty
	  ---
      ,i.ChangeOverLabourConsumption
      ,i.ChangeOverMachineConsumption
	  ,i.ChangeOverNumberOfEmployee
	  ---
      ,i.MoveTime
      ,i.Overlap
	  ---
	  ,'i20230111b' as description
	  ,s.ProductNumber
	  ---
		from dbo.import_routes as i
		left join dbo.Products as s
		on s.ProductNumber = i. productNumber
		left join dbo.Operations as o
		on o.OperationNumber = i.OperationNumber
		left join dbo.Resources as r
		on r.ResourceNumber = i.ResourceNumber
		left join dbo.Resources as w
		on w.ResourceNumber = i.WorkcenterNumber
		left join dbo.Resources as co
		on co.ResourceNumber = i.ChangeOverResourceNumber
		LEFT join dbo.RouteVersions as v
--	    on v.ProductId = s.id and v.DefaultVersion = 1
		on v.ProductId = s.id and v.AlternativeNo = i.alternative
--		where i.alternative = '*'
		where i.alternative != '*'



USE [MwTech]
GO

-- dodano 2022.10.17


INSERT INTO [dbo].[ManufactoringRoutes]
           (
		    [RouteVersionId]
		   ,[OperationId]
           ,[ResourceId]
           ,[WorkCenterId]
		   ,[ChangeOverResourceId]
		   ---
		   ,[OrdinalNumber]
		   ---
           ,[OperationLabourConsumption]
           ,[OperationMachineConsumption]
           ,[ResourceQty]
		   ---
           ,[ChangeOverLabourConsumption]
           ,[ChangeOverMachineConsumption]
           ,[ChangeOverNumberOfEmployee]
		   --
           
           
           ,[MoveTime]
           ,[Overlap]
		   --
		   ,[Description]
		   )
 
           (
		   select 
	   v.id as RouteVersionId
	  ,o.id as OperationId
	  ,r.Id as ResourceId
	  ,w.Id as WorkCenterId
	  ,co.Id as ChangeOverResourceId
	  ---
	  ,i.OrdinalNumber as OrdinalNumber
	  ---
      ,i.OperationLabourConsumption
      ,i.OperationMachineConsumption
 	  ,i.ResourceQty
	  ---
      ,i.ChangeOverLabourConsumption
      ,i.ChangeOverMachineConsumption
	  ,i.ChangeOverNumberOfEmployee
	  ---
      ,i.MoveTime
      ,i.Overlap
	  ---
	  ,'i20230111b' as description
	  ---
		from dbo.import_routes as i
		left join dbo.Products as s
		on s.ProductNumber = i. productNumber
		left join dbo.Operations as o
		on o.OperationNumber = i.OperationNumber
		left join dbo.Resources as r
		on r.ResourceNumber = i.ResourceNumber
		left join dbo.Resources as w
		on w.ResourceNumber = i.WorkcenterNumber
		left join dbo.Resources as co
		on co.ResourceNumber = i.ChangeOverResourceNumber
		LEFT join dbo.RouteVersions as v
--	    on v.ProductId = s.id and v.DefaultVersion = 1
		on v.ProductId = s.id and v.AlternativeNo = i.alternative
--		where i.alternative = '*'
		where i.alternative != '*'
	   )



