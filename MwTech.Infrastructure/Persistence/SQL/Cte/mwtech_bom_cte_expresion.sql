-- Drzewko technologiczne w MwTech
-- Zestawem jest produkt we wskazanej wersji
-- Składnikiem jest produkt w wersji domyślnej
-- Aby pobrały się wszystkie składniki 
-- Każdy produkt musi mieć conajmniej jedną wersję
-- Jedna z tych wersji musi być ustawiona jako domyslna

use mwtech;

declare @SetId INT;
declare @SetVersionId INT;

SET @SetId = (select ID from dbo.Products where ProductNumber = 'OSUGT100020161T');
SET @SetVersionId = null;


with CTE AS
(
SELECT 
	-- Id & ParentId
   	 0 as ParentId
--	,boms.Id as Id
	,Cast(round(rand() * 100000,0) + boms.Id  as int) as Id
	--
	,0 AS Level
	,CAST(ROW_NUMBER() over(order by boms.SetId) as numeric(10) )as rownr

	-- zestaw
	,setProduct.Id as SetProductId
	,CAST(trim(CAST(setProduct.Id as varchar(100)))  AS varchar(500)) as SetGrp
	,setProductVersion.Id as SetProductVersionId
	,setProduct.ProductNumber as SetProductNumber
	,setProductVersion.ProductQty as SetProductQty
	,setProduct.UnitId as SetUnitId
	,setUnit.Name as SetUnit
	,setProductVersion.ProductWeight as SetProductWeight

	-- składnik
	,boms.OrdinalNumber as PartOrdinalNo
	,boms.OnProductionOrder as PartOnProductionOrder
	,boms.DoNotIncludeInWeight as PartDoesNotIncludeInWeight
	,partProduct.Id as PartProductId
	,CAST(trim(CAST(setProduct.Id as varchar(100))) + '&' + trim(CAST(partProduct.Id as varchar(100)))  AS varchar(500)) as PartGrp
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

 		WHERE boms.SetId = @SetId 
		and boms.SetVersionId = IIF(@SetVersionId IS NOT NULL,@SetVersionId, (select Id from dbo.ProductVersions as x where x.productId = @SetId and x.DefaultVersion = 1))

UNION ALL 

SELECT 
   	 c.Id as ParentId
	-- ,boms.Id as Id
	   ,Cast(round(rand() * 100000,0) + boms.Id  as int) as Id
	-- 
    ,c.Level + 1  as level
	,CAST(ROW_NUMBER() over(order by boms.SetId) as numeric(10) ) as rownr
	
	-- zestaw
	,setProduct.Id as SetProductId
	,CAST( trim(c.SetGrp) + '&' + trim(CAST(setProduct.Id as varchar(500)))  AS varchar(500)) as SetGrp
	,setProductVersion.Id as SetProductVersionId
	,setProduct.ProductNumber as SetProductNumber
	,setProductVersion.ProductQty as SetProductQty
	,setProduct.UnitId as SetUnitId
	,setUnit.Name as SetUnit
	,setProductVersion.ProductWeight as SetProductWeight

	-- składnik
	,boms.OrdinalNumber as PartOrdinalNo
	,boms.OnProductionOrder as PartOnProductionOrder
	,boms.DoNotIncludeInWeight as PartDoesNotIncludeInWeight
	,partProduct.Id as PartProductId
	,CAST( trim(c.PartGrp) + '&' + trim(CAST(partProduct.Id as varchar(500)))  AS varchar(500)) as PartGrp
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
		on  c.partProductId = boms.SetId
		and c.partProductVersionId = boms.SetVersionId
--		and c.PartOrdinalNo = boms.OrdinalNumber
		

  )


 select round(rand() * 100000,0)  as x, c.*
 ,(select count(*) as ile from CTE as x where x.setProductId = c.partProductId) as HowManyParts
  from CTE as c 
  where c.SetProductNumber = 'OSUGT100020161T'

