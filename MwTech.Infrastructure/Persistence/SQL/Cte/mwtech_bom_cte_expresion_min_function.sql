﻿-- Table-Valued Function (TVF) 
-- Drzewko technologiczne w MwTech
-- Zestawem jest produkt we wskazanej wersji
-- Składnikiem jest produkt w wersji domyślnej
-- UWAGA
-- Aby pobrały się wszystkie składniki 
-- Każdy produkt musi mieć conajmniej jedną wersję
-- Jedna z tych wersji musi być ustawiona jako domyslna

-- funkcję tabelaryczną wywołujemy jak poniżej
-- select * from dbo.mwtech_bom_cte(@SetId,@SetVersionId)
-- OPTION (MAXRECURSION 1000); -- bez ograniczenia poziomu rekursji

-- zmiany 2023.05.11 - dodanie kolumna Id i ParentId

CREATE OR ALTER FUNCTION mwtech_bom_cte_min (@SetId INT, @SetVersionId INT = null)
RETURNS TABLE
AS
RETURN
(

with CTE AS
(
SELECT 
    --
	 0 as ParentId
	,boms.Id as Id
	-- zestaw
	,setProduct.Id as SetProductId
	,setProductVersion.Id as SetProductVersionId
	,setProductVersion.ProductQty as SetProductQty
	,setProductVersion.ProductWeight as SetProductWeight

	-- składnik
	,boms.PartId  as PartProductId
	,partProductVersion.Id as PartProductVersionId
	-- warunki
	,boms.OnProductionOrder as PartOnProductionOrder
	,boms.DoNotIncludeInWeight as PartDoesNotIncludeInWeight
	-- ilosc
	,boms.PartQty as PartProductQty
	


	  ,CAST( round((boms.PartQty  ) ,6) as numeric(14,6)) as FinalPartProductQty

	 ,CAST( round(
	   ((boms.PartQty  ) * iif(partUnit.Weight=1,1,partProductVersion.ProductWeight))  
	 ,6) as numeric(14,6)) as FinalPartProductWeight

 FROM dbo.boms as boms
		inner join dbo.Products as setProduct
		on setProduct.Id = boms.SetId
		inner join dbo.ProductVersions as setProductVersion
		on setProductVersion.Id = boms.SetVersionId
		inner join dbo.Units as setUnit
		on setUnit.Id = setProduct.UnitId
		
		inner join dbo.Products as partProduct
		on partProduct.Id = boms.PartId
		inner join dbo.ProductVersions as partProductVersion
		on partProductVersion.ProductId = partProduct.Id and partProductVersion.DefaultVersion = 1
		inner join dbo.Units as partUnit
		on partUnit.Id = partProduct.UnitId

 		WHERE 1 = 1
		AND boms.SetId = @SetId 
		AND boms.SetVersionId = IIF(@SetVersionId IS NOT NULL,@SetVersionId, (select Id from dbo.ProductVersions as x where x.productId = @SetId and x.DefaultVersion = 1))

UNION ALL 

SELECT 
 c.Id as ParentId
	-- ,boms.Id as Id
	,c.Id + boms.Id  as Id
	
	-- zestaw
	,setProduct.Id as SetProductId
	
	-- ,CAST( trim(c.SetGrp) + '&' + trim(CAST(c.SetOrdinalNo as varchar(500))) + '#' + trim(CAST(setProduct.Id as varchar(500)))  AS varchar(500)) as SetGrp
	,setProductVersion.Id as SetProductVersionId
	,setProductVersion.ProductQty as SetProductQty
	,setProductVersion.ProductWeight as SetProductWeight

	-- składnik
	,boms.PartId  as PartProductId
	,partProductVersion.Id as PartProductVersionId
	-- warunki
	,boms.OnProductionOrder as PartOnProductionOrder
	,boms.DoNotIncludeInWeight as PartDoesNotIncludeInWeight
	-- ilosc
	,boms.PartQty as PartProductQty
	

	,CAST( round(((boms.PartQty  ) / setProductVersion.ProductQty) * c.FinalPartProductQty,6) as numeric(14,6)) as FinalPartProductQty

	,CAST( round(
	 (( (boms.PartQty ) / setProductVersion.ProductQty) * c.FinalPartProductQty * iif(partUnit.Weight=1,1,partProductVersion.ProductWeight))
	,6) as numeric(14,6)) as FinalPartProductWeight


	  FROM dbo.boms as boms
		inner join dbo.Products as setProduct
		on setProduct.Id = boms.SetId
		inner join dbo.ProductVersions as setProductVersion
		on setProductVersion.Id = boms.SetVersionId
		inner join dbo.Units as setUnit
		on setUnit.Id = setProduct.UnitId

		inner join dbo.Products as partProduct
		on partProduct.Id = boms.PartId
		inner join dbo.ProductVersions as partProductVersion
		on partProductVersion.ProductId = partProduct.Id and partProductVersion.DefaultVersion = 1
		inner join dbo.Units as partUnit
		on partUnit.Id = partProduct.UnitId

		inner join CTE as c	
		on  boms.SetId = c.partProductId
		and boms.SetVersionId = c.partProductVersionId

  )


 select c.*
 ,(select count(*) as ile from CTE as x where x.setProductId = c.partProductId) as HowManyParts
  from CTE as c 
 
)