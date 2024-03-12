/****** Script for SelectTopNRows command from SSMS  ******/
declare @operation as varchar(20);
declare @consumption as decimal(10,6);
declare @employee as decimal(10,6);
set @operation = 'DWU_WULKANIZACJA';
set @consumption = 0.5;
set @employee = 1;




update r
set r.ChangeOverResourceId = 411,
r.ChangeOverNumberOfEmployee = @employee,
r.ChangeOverLabourConsumption = @consumption,
r.ChangeOverMachineConsumption = @consumption
  FROM [MwTech].[dbo].[ManufactoringRoutes] as r
  where OperationId = (select Id from [MwTech].[dbo].[Operations] where OperationNumber = @operation)

  select r.ResourceId,
	r.ChangeOverResourceId,
	r.ChangeOverNumberOfEmployee,
	r.ChangeOverLabourConsumption,
	r.ChangeOverMachineConsumption
  FROM [MwTech].[dbo].[ManufactoringRoutes] as r
  where OperationId = (select Id from [MwTech].[dbo].[Operations] where OperationNumber = @operation)