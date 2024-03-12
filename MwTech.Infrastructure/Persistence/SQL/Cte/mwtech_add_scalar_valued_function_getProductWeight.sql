-- ================================================
-- Template generated from Template Explorer using:
-- Create Scalar Function (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE OR ALTER FUNCTION getProductWeight(@productId as INT)
RETURNS DECIMAL(10,2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @productWeight DECIMAL(10,2)
	DECLARE @productVersionId INT = 0
	SET @productVersionId = (SELECT TOP 1 v.id from dbo.ProductVersions as v where v.ProductId = @productId and v.DefaultVersion = 1)

	-- Add the T-SQL statements to compute the return value here
	-- SELECT <@ResultVar, sysname, @Result> = <@Param1, sysname, @p1>
	
	SET @productWeight =
	(SELECT SUM(FinalPartProductWeight) FROM dbo.mwtech_bom_cte_min(@productId,@productVersionId) as t
	where HowManyParts = 0 and PartDoesNotIncludeInWeight = 0)

	-- Return the result of the function
	RETURN @productWeight

END
GO

