/****** Script for SelectTopNRows command from SSMS  ******/
/* Dodanie kartonu */

USE [MwTech]
GO

SELECT t.indeks1
      ,t.ilosc
	  ,t.po_ile
	  ,p.ProductNumber
	  ,p.id as ProductId
	  ,v.Id as ProducVersionId
	  ,94607 as KartonId
	  ,(select MAX(X.OrdinalNumber) + 1 from [dbo].[Boms] as x where SetId = p.id and SetVersionId = v.Id) as lp
FROM [MwTech].[dbo].[temp] as t
inner join dbo.Products as p
on p.OldProductNumber = t.indeks1
inner join dbo.ProductVersions as v
on v.ProductId = p.Id and v.DefaultVersion = 1

/*

INSERT INTO [dbo].[Boms]
           ([SetId]
           ,[PartId]
           ,[PartQty]
           ,[OrdinalNumber]
           ,[SetVersionId]
           ,[Excess]
           ,[OnProductionOrder]
           ,[Layer]
           ,[Description])
(
SELECT p.id as SetId
	  ,94607 as PartId
	  ,t.po_ile as PartQty
	  ,(select MAX(X.OrdinalNumber) + 1 from [dbo].[Boms] as x where SetId = p.id and SetVersionId = v.Id) as OrdinalNumber
	  ,v.Id as SetVersionId
      ,0 as Excess
	  ,0 as OnProductionOrder
	  ,0 as Layer
	  ,'czêœæ kartonu na jedn¹ dêtkê' as Description
FROM [MwTech].[dbo].[temp] as t
inner join dbo.Products as p
on p.OldProductNumber = t.indeks1
inner join dbo.ProductVersions as v
on v.ProductId = p.Id and v.DefaultVersion = 1
)

*/