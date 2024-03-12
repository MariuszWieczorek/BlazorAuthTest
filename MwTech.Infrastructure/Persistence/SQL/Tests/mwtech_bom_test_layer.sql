/****** Script for SelectTopNRows command from SSMS  ******/
select x.setProduct, x.partProduct, x.Layer, COUNT(*) as ile
from 
(
SELECT se.ProductNumber as setProduct
	  ,pa.ProductNumber as partProduct	
	  ,b.Layer

--	  ,v.VersionNumber
  --    ,b.PartQty
--      ,b.OrdinalNumber
 --     ,[SetVersionId]
  --    ,[Excess]
   --   ,[OnProductionOrder]
  FROM [MwTech].[dbo].[Boms] as b
  inner join dbo.Products as se
  on se.Id = b.SetId
  inner join dbo.Products as pa
  on pa.Id = b.PartId
  inner join dbo.ProductVersions as v
  on v.Id = b.SetVersionId
    
  where 1 = 1
--  and b.OrdinalNumber = 0
--	and b.Layer = 0
  and v.ToIfs = 1
  and se.ProductNumber like 'OSU%'
  and pa.ProductNumber like 'OKG%'
  ) as x
  group by x.setProduct, x.partProduct, x.Layer
  order by x.setProduct, x.Layer, x.partProduct


select x.setProduct, x.partProduct, x.Layer, x.warstwa
from 
(
SELECT se.ProductNumber as setProduct
	  ,pa.ProductNumber as partProduct	
	  ,b.Layer
	  ,mbw.warstwa

--	  ,v.VersionNumber
  --    ,b.PartQty
--      ,b.OrdinalNumber
 --     ,[SetVersionId]
  --    ,[Excess]
   --   ,[OnProductionOrder]
  FROM [MwTech].[dbo].[Boms] as b
  inner join dbo.Products as se
  on se.Id = b.SetId
  inner join dbo.Products as pa
  on pa.Id = b.PartId
  inner join dbo.ProductVersions as v
  on v.Id = b.SetVersionId
  inner join mwbase.gm.materialy as mbm
  on mbm.INDEKS = se.OldProductNumber 
  inner join mwbase.prdkabat.opony as mbo
  on mbo.matId = mbm.MATID
  inner join [mwbase].[prdkabat].[opony_kordy_gumowane_ciete] as mbw
  on mbw.opona = mbo.pozid 
  and mbw.wersja = mbo.wersja
 
 inner join mwbase.prdkabat.kordy_gumowane_ciete as kc
 on kc.pozId = mbw.kordCiety and kc.indeks = pa.ProductNumber
  
  where 1 = 1
--  and b.OrdinalNumber = 0
--	and b.Layer = 0
	  and v.ToIfs = 1
   and se.ProductNumber like 'OSU%'
  and pa.ProductNumber like 'OKG%'
  ) as x
  group by x.setProduct, x.partProduct, x.Layer, x.warstwa
  order by x.setProduct, x.partProduct, x.Layer, x.warstwa

/*
  update b
  set Layer = mbw.warstwa
  FROM [MwTech].[dbo].[Boms] as b
  inner join dbo.Products as se
  on se.Id = b.SetId
  inner join dbo.Products as pa
  on pa.Id = b.PartId
  inner join dbo.ProductVersions as v
  on v.Id = b.SetVersionId
  inner join mwbase.gm.materialy as mbm
  on mbm.INDEKS = se.OldProductNumber 
  inner join mwbase.prdkabat.opony as mbo
  on mbo.matId = mbm.MATID
  inner join [mwbase].[prdkabat].[opony_kordy_gumowane_ciete] as mbw
  on mbw.opona = mbo.pozid 
  and mbw.wersja = mbo.wersja
 
 inner join mwbase.prdkabat.kordy_gumowane_ciete as kc
 on kc.pozId = mbw.kordCiety and kc.indeks = pa.ProductNumber
  
  where 1 = 1
--  and b.OrdinalNumber = 0
	and b.Layer = 0
	  and v.ToIfs = 1
   and se.ProductNumber like 'OSU%'
  and pa.ProductNumber like 'OKG%'
  */


/*
  update b
  set OrdinalNumber = 1
    FROM [MwTech].[dbo].[Boms] as b
  inner join dbo.Products as se
  on se.Id = b.SetId
  inner join dbo.Products as pa
  on pa.Id = b.PartId
  inner join dbo.ProductVersions as v
  on v.Id = b.SetVersionId
  where b.OrdinalNumber = 0
  and v.ToIfs = 1
  and se.ProductNumber like 'OKG%'
 -- and pa.ProductNumber like 'ODD001%'
 */