/* import bomów na podstawie dbo.import_boms czêœæ 3
   dodanie do wersji produktu bomów */

use MwTech;

select * from (
select 
 s.ProductNumber as setProductNo
,s.id as setProductId
,v.Id as productVersionId
--
,p.ProductNumber as partProductNo
,p.id as partProductId
--
,i.No
,i.partQty
,i.excess
--
,iif(i.onProdOrder = 'T',1,0) as onProdOrder
--
,'i20221020a' as description
from dbo.import_boms as i
left join dbo.Products as s
on s.ProductNumber = i.productNo
left join dbo.Products as p
on p.ProductNumber = i.partNo
LEFT join dbo.ProductVersions as v
on v.ProductId = s.id and v.DefaultVersion = 1
group by 
 v.id
,s.ProductNumber
,s.id
,i.alternative
,i.alternativeName
,i.No
,p.ProductNumber
,p.id
,i.partQty
,i.excess
,i.onProdOrder
) as x



USE [MwTech]
GO


INSERT INTO [dbo].[Boms]
           (
		    [SetId]
		   ,[SetVersionId]
			--
		   ,[OrdinalNumber]
           ,[PartId]
		    --
           ,[PartQty]
           ,[Excess]
		   ,[OnProductionOrder]
		   --
           ,[Description]
	       )
 
           (
		   select 
				 s.id as setProductId
				,v.Id as productVersionId
				--
				,i.No
				,p.id as partProductId
				--
				,i.partQty
				,coalesce(i.excess,0)
				,iif(i.onProdOrder = 'T',1,0) as onProdOrder
				--
				,'i20221020a' as description
				--
				from dbo.import_boms as i
				left join dbo.Products as s
				on s.ProductNumber = i.productNo
				left join dbo.Products as p
				on p.ProductNumber = i.partNo
				LEFT join dbo.ProductVersions as v
				on v.ProductId = s.id and v.DefaultVersion = 1
				group by 
				 v.id
				,s.ProductNumber
				,s.id
				,i.alternative
				,i.alternativeName
				,i.No
				,p.ProductNumber
				,p.id
				,i.partQty
				,i.excess
				,i.onProdOrder
		 )


