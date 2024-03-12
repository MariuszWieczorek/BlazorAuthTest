	declare @OperationNo varchar(50);
declare @OperationId int;
declare @ProductNo varchar(50);
declare @WorkCenterId int;
declare @WorkCenterNo varchar(10);
set @OperationNo= 'OWU-WULKANIZACJA';
set @OperationId = (select op.Id from [MwTech].[dbo].[Operations] as op where op.OperationNumber = @OperationNo) 
set @WorkCenterNo = 'POKON';
set @WorkCenterId = (select wc.Id from [MwTech].[dbo].Resources as wc where wc.ResourceNumber = @WorkCenterNo) ;

	select pr.ProductNumber
  FROM [MwTech].[dbo].[ManufactoringRoutes] as r
  inner join [MwTech].[dbo].[ProductVersions] as pv
  on pv.Id = r.ProductVersionId
  inner join [MwTech].[dbo].[Products] as pr
  on pr.Id = pv.ProductId
  where r.WorkCenterId = @WorkCenterId 
