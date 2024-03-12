/* import marszrut na podstawie dbo.import_routes czêœæ 2
   zak³adanie rekordów z wersj¹ marszruty  */

use MwTech;

	    select 
		     iif(i.alternative='*',0,i.alternative) as alt
			,i.WorkcenterNumber as altName
			--
			,i.productNumber
			,s.id as ProductId
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
			,'i20221020a' as Description
			from dbo.import_routes as i
			left join dbo.Products as s
			on s.ProductNumber = i. productNumber
			left join dbo.Operations as o
			on o.OperationNumber = i.OperationNumber
			left join dbo.Resources as r
			on r.ResourceNumber = i.ResourceNumber
			left join dbo.Resources as w
			on w.ResourceNumber = i.WorkcenterNumber
			left join dbo.Resources as co
			on co.ResourceNumber = i.ChangeOverResourceNumber
--			where s.productNumber is null
			group by 
			i.umiejscowienie
		   ,s.ProductNumber
		   ,i.productNumber
		   ,s.Id
		   ,i.alternative
		   ,i.WorkcenterNumber
		   

INSERT INTO [dbo].[RouteVersions]
           (
            [VersionNumber]
	   	   ,[AlternativeNo]
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
		   -- 
		   ,IsAccepted01
		   ,IsAccepted02
		   )
		   (
	    select 
			 1 as VersionNumber
		    ,iif(i.alternative='*',0,i.alternative) as alt
			,i.WorkcenterNumber as altName
			--
			,s.id as ProductId
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
			,'i20230111b' as Description
			--
			,0 as IsAccepted01
		    ,0 as IsAccepted02
			from dbo.import_routes as i
			left join dbo.Products as s
			on s.ProductNumber = i. productNumber
			left join dbo.Operations as o
			on o.OperationNumber = i.OperationNumber
			left join dbo.Resources as r
			on r.ResourceNumber = i.ResourceNumber
			left join dbo.Resources as w
			on w.ResourceNumber = i.WorkcenterNumber
			left join dbo.Resources as co
			on co.ResourceNumber = i.ChangeOverResourceNumber
			group by 
			i.umiejscowienie
		   ,s.ProductNumber
		   ,s.Id
		   ,i.alternative
		   ,i.WorkcenterNumber
		   )








