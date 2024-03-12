/* import bomów na podstawie dbo.import_boms czêœæ 2
   zak³adanie rekordów z wersj¹ produktu  */

use MwTech;


select 
 s.ProductNumber as setProductNo
,s.id as setProductId
,cast(iif(i.alternative='*',0,i.alternative) as int)as alt
,i.alternativeName as altName
from dbo.import_boms as i
left join dbo.Products as s
on s.ProductNumber = i. productNo
left join dbo.Products as p
on p.ProductNumber = i. productNo
group by 
 i.umiejscowienie
,s.ProductNumber
,s.id
,i.alternative
,i.alternativeName




INSERT INTO [dbo].[ProductVersions]
           (
            [VersionNumber]
		   ,[Name]
		   --
		   ,[ProductId]
           ,[ProductQty]
           --
		   ,[IsActive]
           ,[ToIfs]
           ,[IfsDefaultVersion]
		   ,[DefaultVersion]
		   --
           ,[CreatedByUserId]
           ,[CreatedDate]
		   --
		   ,[Description]
		   )
		   (
		   select 
		     iif(i.alternative='*',0,i.alternative) as alt
			,i.alternativeName as altName
			--
			,s.id as setProductId
			,1 as [ProductQty]
			--
			,1 as [IsActive]
			,1 as [ToIfs]
			,iif(i.alternative='*',1,0) as [IfsDefaultVersion]
			,iif(i.alternative='*',1,0) as [DefaultVersion]
			--
			,'7f2bacf6-564a-4272-a4bb-f76832476024' as CreatedByUserId
			,GETDATE() as CreatedDate
			--
			,'i20221020a'
			from dbo.import_boms as i
			left join dbo.Products as s
			on s.ProductNumber = i. productNo
			group by 
			 s.id
			,i.alternative
			,i.alternativeName
		   )








