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
   	 0 as ParentId
	,boms.Id as Id

	,CAST(trim(CAST(0 as varchar(100)))  AS varchar(500)) as SetGrp
	,CAST(trim(CAST(0 as varchar(100))) + '__' + trim(CAST(boms.Id as varchar(100)))  AS varchar(500)) as PartGrp

	,0 AS Level
	,CAST(ROW_NUMBER() over(order by boms.SetId) as numeric(10) )as rownr

	-- zestaw
	,setProduct.Id as SetProductId
	,setProductVersion.Id as SetProductVersionId
	
	,setProduct.ProductNumber as SetProductNumber
	,setProductVersion.ProductQty as SetProductQty
	,setProductVersion.ProductWeight as SetProductWeight

	-- składnik
	,partProduct.Id as PartProductId
	,partProductVersion.Id as PartProductVersionId
	,boms.OrdinalNumber as PartOrdinalNo

	,cast( coalesce(partProduct.ProductNumber,'')  as varchar(50)) as PartProductNumber

	-- ilości
	,boms.PartQty as PartProductQty
	,CAST( round((boms.PartQty  ) ,6) as numeric(14,6)) as FinalPartProductQty

	--



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

 		WHERE boms.SetId = @SetId 
		and boms.SetVersionId = IIF(@SetVersionId IS NOT NULL,@SetVersionId, (select Id from dbo.ProductVersions as x where x.productId = @SetId and x.DefaultVersion = 1))

UNION ALL 

SELECT 
-- 	 c.Id as ParentId
--	,boms.Id as Id

	 c.Id as ParentId
	,c.Id + boms.Id  as Id

	,CAST( trim(c.PartGrp)   AS varchar(500)) as SetGrp
	,CAST( trim(c.PartGrp) + '__' + trim(CAST(boms.Id as varchar(500)))  AS varchar(500)) as PartGrp

    ,c.Level + 1  as level
	,CAST(ROW_NUMBER() over(order by boms.SetId) as numeric(10) ) as rownr
	
	-- zestaw
	,setProduct.Id as SetProductId
	,setProductVersion.Id as SetProductVersionId

	,setProduct.ProductNumber as SetProductNumber
	,setProductVersion.ProductQty as SetProductQty
	,setProductVersion.ProductWeight as SetProductWeight

	-- składnik
	,partProduct.Id as PartProductId
	,partProductVersion.Id as PartProductVersionId
	,boms.OrdinalNumber as PartOrdinalNo
	,cast( coalesce(partProduct.ProductNumber,'')  as varchar(50)) as PartProductNumber

	-- ilości
	,boms.PartQty as PartProductQty
	,CAST( round(((boms.PartQty  ) / setProductVersion.ProductQty) * c.FinalPartProductQty,6) as numeric(14,6)) as FinalPartProductQty
	


	--	
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

		inner join CTE as c	
		on  c.partProductId = boms.SetId
		and c.partProductVersionId = boms.SetVersionId
  )


 select  c.*
 ,(select count(*) as ile from CTE as x where x.setProductId = c.partProductId) as HowManyParts
 ,CAST(ROW_NUMBER() over(order by c.Id) as numeric(10) )as rownrr
  from CTE as c 
  order by level,rownr,SetProductNumber,PartProductNumber
  -- where c.SetProductNumber = 'OSUGT100020161T'

