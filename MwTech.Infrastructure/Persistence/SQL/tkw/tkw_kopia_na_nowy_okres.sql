use mwtech;
declare @OldPeriodId as int;
declare @NewPeriodId as int;

set @OldPeriodId = 27;
set @NewPeriodId = 28;


insert into dbo.ProductCosts
(
	   [AccountingPeriodId]
      ,[ProductId]
      ,[Cost]
      ,[IsCalculated]
      ,[CreatedByUserId]
      ,[CreatedDate]
      ,[ModifiedByUserId]
      ,[ModifiedDate]
      ,[CalculatedDate]
      ,[CurrencyId]
      ,[Description]
      ,[ImportedDate]
      ,[IsImported]
      ,[LabourCost]
      ,[MarkupCost]
      ,[MaterialCost]
      ,[ProductLabourCost]
      ,[EstimatedCost]
      ,[EstimatedLabourCost]
      ,[EstimatedMarkupCost]
      ,[EstimatedMaterialCost]
      ,[EstimatedProductLabourCost]
	  )

select 
	   @NewPeriodId as AccountingPeriodId
      ,[ProductId]
      ,[Cost]
      ,[IsCalculated]
      ,[CreatedByUserId]
      ,[CreatedDate]
      ,[ModifiedByUserId]
      ,GETDATE()
      ,GETDATE()
      ,[CurrencyId]
      ,[Description]
      ,[ImportedDate]
      ,[IsImported]
      ,[LabourCost]
      ,[MarkupCost]
      ,[MaterialCost]
      ,[ProductLabourCost]
      ,[EstimatedCost]
      ,[EstimatedLabourCost]
      ,[EstimatedMarkupCost]
      ,[EstimatedMaterialCost]
      ,[EstimatedProductLabourCost]
FROM dbo.ProductCosts as co
WHERE co.AccountingPeriodId = @OldPeriodId
