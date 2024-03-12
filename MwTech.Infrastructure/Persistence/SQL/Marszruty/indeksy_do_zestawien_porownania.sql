/* Indeksy do zestawieñ */

USE [MwTech]
GO
CREATE NONCLUSTERED INDEX IX_Products_IsActive_IsTest
ON [dbo].[Products] ([IsActive],[IsTest])
INCLUDE ([ProductNumber],[ProductCategoryId])
GO

USE [MwTech]
GO
CREATE NONCLUSTERED INDEX iX_IfsRoutes_PartNo_AlternativeNo
ON [dbo].[IfsRoutes] ([PartNo],[AlternativeNo])

GO




