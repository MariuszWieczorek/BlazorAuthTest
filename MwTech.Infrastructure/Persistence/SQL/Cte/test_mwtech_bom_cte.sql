use mwtech;
DECLARE @SetId as INT;
DECLARE @SetVersionId as INT;
SET @SetId = 103420;
SET @SetVersionId = 111196;
select * from dbo.mwtech_bom_cte(@SetId,@SetVersionId)