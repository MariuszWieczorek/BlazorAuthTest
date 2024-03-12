-- Table-Valued Function (TVF) 
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

CREATE OR ALTER FUNCTION mwtech_bom_cte (@SetId INT, @SetVersionId INT = null)
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
	--
	,0 AS Level
	,CAST(ROW_NUMBER() over(order by boms.SetId) as numeric(10) )as rownr

	-- zestaw
	,setProduct.Id as SetProductId
	
	,setProductVersion.Id as SetProductVersionId
	,setProduct.ProductNumber as SetProductNumber
	,setProductVersion.ProductQty as SetProductQty
	,setProduct.UnitId as SetUnitId
	,setUnit.Name as SetUnit
	,setProductVersion.ProductWeight as SetProductWeight
	,1 as SetOrdinalNo

	-- składnik
	,boms.OrdinalNumber as PartOrdinalNo
	,boms.OnProductionOrder as PartOnProductionOrder
	,boms.DoNotIncludeInWeight as PartDoesNotIncludeInWeight
	,partProduct.Id as PartProductId
	
	,partProductVersion.Id as PartProductVersionId
	,cast( coalesce(partProduct.ProductNumber,'')  as varchar(50)) as PartProductNumber
	,cast( coalesce(partProduct.Name,'')  as varchar(50)) as PartProductName
	,cast( coalesce(partProduct.ContentsOfRubber,0) as decimal(12,5)) as PartContentsOfRubber
	,cast( coalesce(partProduct.Density,0) as decimal(12,5)) as PartDensity
	,cast( coalesce(partProduct.ScalesId,0) as int) as PartScalesId
	,boms.PartQty as PartProductQty
	

	-- obliczenia ostatecznej ilości składnika
	-- ilość składnika jest zdefiniowana do ilości zestawu
	-- dzielimy ilość składnika przez ilość zestawów aby uzyskać ilość składnika dla jednostkowego zestawu
	-- uzyskaną ilość jednostkową mnożymy przez ilość produktu, który jest zestawem poziom wyżej w drzewie 
	-- pierwszy składnik dla ilości jednostkowej 
	-- CAST( round((boms.PartQty / setProductVersion.ProductQty) * 1 ,6) as numeric(14,6)) as FinalPartProductQty
	   
	-- pierwszy składnik dla ilości zdefiniowanej w wersji zestawu
	-- - boms.PartQty * 0.01 * boms.Scrap

	 ,CAST( round((boms.PartQty  ) ,6) as numeric(14,6)) as FinalPartProductQty
	 ,partProduct.UnitId as PartUnitId
	 ,partUnit.Name as PartUnit
	 ,partProductVersion.ProductWeight as PartProductWeight

-- 	 ,CAST( round(
--	 (boms.PartQty * iif(partUnit.Weight=1,1,partProductVersion.ProductWeight))
--	 ,6) as numeric(14,6)) as FinalPartProductWeight
-- - boms.PartQty * 0.01 * boms.Scrap
	 ,CAST( round(
	   ((boms.PartQty  ) * iif(partUnit.Weight=1,1,partProductVersion.ProductWeight))  
	 ,6) as numeric(14,6)) as FinalPartProductWeight

--	 ,CAST(trim(CAST(setProduct.Id as varchar(100)))  AS varchar(500)) as SetGrp
--	 ,CAST(trim(CAST(setProduct.Id as varchar(100))) + '&' + trim(CAST(partProduct.Id as varchar(100)))  AS varchar(500)) as PartGrp

 	,CAST(trim(CAST(0 as varchar(100)))  AS varchar(500)) as SetGrp
	,CAST(trim(CAST(0 as varchar(100))) + '__' + trim(CAST(boms.Id as varchar(100)))  AS varchar(500)) as PartGrp
	
	,ca.CategoryNumber as PartCategoryNumber
	 ,ca1.CategoryNumber as SetCategoryNumber

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

		inner join dbo.ProductCategories as ca
		on ca.Id = partProduct.ProductCategoryId

		inner join dbo.ProductCategories as ca1
		on ca1.Id = setProduct.ProductCategoryId

 		WHERE boms.SetId = @SetId 
		and boms.SetVersionId = IIF(@SetVersionId IS NOT NULL,@SetVersionId, (select Id from dbo.ProductVersions as x where x.productId = @SetId and x.DefaultVersion = 1))

UNION ALL 

SELECT 
	-- 
   	 c.Id as ParentId
	-- ,boms.Id as Id
	,c.Id + boms.Id  as Id
	-- 
    ,c.Level + 1  as level
	,CAST(ROW_NUMBER() over(order by boms.SetId) as numeric(10) ) as rownr
	
	-- zestaw
	,setProduct.Id as SetProductId
	
	-- ,CAST( trim(c.SetGrp) + '&' + trim(CAST(c.SetOrdinalNo as varchar(500))) + '#' + trim(CAST(setProduct.Id as varchar(500)))  AS varchar(500)) as SetGrp
	,setProductVersion.Id as SetProductVersionId
	,setProduct.ProductNumber as SetProductNumber
	,setProductVersion.ProductQty as SetProductQty
	,setProduct.UnitId as SetUnitId
	,setUnit.Name as SetUnit
	,setProductVersion.ProductWeight as SetProductWeight
	,c.PartOrdinalNo as SetOrdinalNo

	-- składnik
	,boms.OrdinalNumber as PartOrdinalNo
	,boms.OnProductionOrder as PartOnProductionOrder
	,boms.DoNotIncludeInWeight as PartDoesNotIncludeInWeight
	,partProduct.Id as PartProductId
	
	-- ,CAST( trim(c.PartGrp) + '&' + trim(CAST(c.PartOrdinalNo as varchar(500))) + '#' + trim(CAST(partProduct.Id as varchar(500)))  AS varchar(500)) as PartGrp
	,partProductVersion.Id as PartProductVersionId
	,cast( coalesce(partProduct.ProductNumber,'')  as varchar(50)) as PartProductNumber
	,cast( coalesce(partProduct.Name,'')  as varchar(50)) as PartProductName
	,cast( coalesce(partProduct.ContentsOfRubber,0) as decimal(12,5)) as PartContentsOfRubber
	,cast( coalesce(partProduct.Density,0) as decimal(12,5)) as PartDensity
	,cast( coalesce(partProduct.ScalesId,0) as int) as PartScalesId
	,boms.PartQty as PartProductQty

	-- obliczenia ostatecznej ilości składnika
	-- ilość składnika jest zdefiniowana do ilości zestawu
	-- dzielimy ilość składnika przez ilość zestawów aby uzyskać ilość składnika dla jednostkowego zestawu
	-- uzyskaną ilość jednostkową mnożymy przez ilość produktu, który jest zestawem poziom wyżej w drzewie 

	-- - boms.PartQty * 0.01 * boms.Scrap

	,CAST( round(((boms.PartQty  ) / setProductVersion.ProductQty) * c.FinalPartProductQty,6) as numeric(14,6)) as FinalPartProductQty
	,partProduct.UnitId as PartUnitId
    ,partUnit.Name as PartUnit
	,partProductVersion.ProductWeight as PartProductWeight

--	,CAST( round(
--	 ((boms.PartQty / setProductVersion.ProductQty) * c.FinalPartProductQty * iif(partUnit.Weight=1,1,partProductVersion.ProductWeight))
--	,6) as numeric(14,6)) as FinalPartProductWeight
		
    -- (boms.PartQty - boms.PartQty * 0.01 * boms.Scrap )

	,CAST( round(
	 (( (boms.PartQty ) / setProductVersion.ProductQty) * c.FinalPartProductQty * iif(partUnit.Weight=1,1,partProductVersion.ProductWeight))
	,6) as numeric(14,6)) as FinalPartProductWeight

--	,CAST( trim(c.SetGrp) + '&' +  trim(CAST(setProduct.Id as varchar(500)))  AS varchar(500)) as SetGrp
--	,CAST( trim(c.PartGrp) + '&' + trim(CAST(partProduct.Id as varchar(500)))  AS varchar(500)) as PartGrp

	,CAST( trim(c.PartGrp)   AS varchar(500)) as SetGrp
	,CAST( trim(c.PartGrp) + '__' + trim(CAST(boms.Id as varchar(500)))  AS varchar(500)) as PartGrp

	,ca.CategoryNumber as PartCategoryNumber
	,ca1.CategoryNumber as SetCategoryNumber

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

		inner join dbo.ProductCategories as ca
		on ca.Id = partProduct.ProductCategoryId

		inner join dbo.ProductCategories as ca1
		on ca1.Id = setProduct.ProductCategoryId

		inner join CTE as c	
		on  boms.SetId = c.partProductId
		-- dodana wersja do warunku, bo powielały się pozycje przy oponie wulkanizowanej
		and boms.SetVersionId = c.partProductVersionId
		

  )


 select c.*
 ,(select count(*) as ile from CTE as x where x.setProductId = c.partProductId) as HowManyParts
  from CTE as c 
 
)