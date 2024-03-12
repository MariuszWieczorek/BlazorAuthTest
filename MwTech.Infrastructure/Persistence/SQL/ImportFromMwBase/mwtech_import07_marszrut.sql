-- IMPORT MARSZRUT z MwBase

delete from [MwTech].[dbo].[ManufactoringRoutes]
DBCC CHECKIDENT('[MwTech].[dbo].[ManufactoringRoutes]', RESEED, 1)

declare @okres as int = (select pozid from mwbase.fk.danefk_okresy as o where czy_domyslny = 1);


insert into [MwTech].[dbo].[ManufactoringRoutes]
  (
       [ProductVersionId]
      ,[OperationId]
      ,[ResourceId]
	  ,[Time]
      ,[OrdinalNumber]
      ,[Value]
  )
  (

-- KAPA
SELECT zz.productVersionId, zz.operationId, zz.resourceID, 0, 1, zz.wartosc
FROM (
SELECT 
  (select ww.id
  from [MwTech].[dbo].[ProductVersions] as ww
  inner join [MwTech].[dbo].[Products] as pp
  on ww.productId = pp.id and ww.DefaultVersion = 1
  where pp.MwbaseMatid = n.matid) as productVersionId,
  (select id 
  from [MwTech].[dbo].[Operations] as oo
  where oo.operationNumber = 'OKA_WYTL_KAPY') as operationId,
  (select id 
  from [MwTech].[dbo].[Resources] as rr
  where rr.resourceNumber = 'OKA_WYTL') as resourceId,
  n.wartosc
  FROM [mwbase].[fk].[danefk_materialy_normatywy] as n
  inner join mwbase.gm.materialy as m
  on m.matid = n.matid
  inner join [mwbase].[fk].[danefk_wskazniki] as w
  on w.pozid = n.wskaznik
  WHERE okres = @okres
  and w.symbol = 'WYDAJNOSC_KG_NA_8RBH_KAPA'
  and m.grupa3 = 6
  ) as zz
  where zz.productVersionId is not null
  and zz.operationId is not null
  and zz.resourceID is not null

-- BIEZNIK BOK
UNION ALL
SELECT zz.productVersionId, zz.operationId, zz.resourceID, 0, 1, zz.wartosc
FROM (
SELECT 
  (select ww.id
  from [MwTech].[dbo].[ProductVersions] as ww
  inner join [MwTech].[dbo].[Products] as pp
  on ww.productId = pp.id and ww.DefaultVersion = 1
  where pp.MwbaseMatid = n.matid) as productVersionId,
  (select id 
  from [MwTech].[dbo].[Operations] as oo
  where oo.operationNumber = 'OBI_WYTL_BIEZNIK_BOK') as operationId,
  (select id 
  from [MwTech].[dbo].[Resources] as rr
  where rr.resourceNumber = 'ZO_LWB') as resourceId,
  n.wartosc
  FROM [mwbase].[fk].[danefk_materialy_normatywy] as n
  inner join mwbase.gm.materialy as m
  on m.matid = n.matid
  inner join [mwbase].[fk].[danefk_wskazniki] as w
  on w.pozid = n.wskaznik
  WHERE okres = @okres
  and w.symbol = 'WYDAJNOSC_SZT_NA_8RBH_WY_BIEZNIK'
  and m.grupa3 = (select pozid from mwbase.gm.materialy_grupa3 where mnemonik = 'BIEZNIK_BOK')
  ) as zz
  where zz.productVersionId is not null
  and zz.operationId is not null
  and zz.resourceID is not null

-- BIEZNIK CZOLO
UNION ALL
SELECT zz.productVersionId, zz.operationId, zz.resourceID, 0, 1, zz.wartosc
FROM (
SELECT 
  (select ww.id
  from [MwTech].[dbo].[ProductVersions] as ww
  inner join [MwTech].[dbo].[Products] as pp
  on ww.productId = pp.id and ww.DefaultVersion = 1
  where pp.MwbaseMatid = n.matid) as productVersionId,
  (select id 
  from [MwTech].[dbo].[Operations] as oo
  where oo.operationNumber = 'OBI_WYTL_BIEZNIK_CZOLO') as operationId,
  (select id 
  from [MwTech].[dbo].[Resources] as rr
  where rr.resourceNumber = 'ZO_LWB') as resourceId,
  n.wartosc
  FROM [mwbase].[fk].[danefk_materialy_normatywy] as n
  inner join mwbase.gm.materialy as m
  on m.matid = n.matid
  inner join [mwbase].[fk].[danefk_wskazniki] as w
  on w.pozid = n.wskaznik
  WHERE okres = @okres
  and w.symbol = 'WYDAJNOSC_SZT_NA_8RBH_WY_BIEZNIK'
  and m.grupa3 = (select pozid from mwbase.gm.materialy_grupa3 where mnemonik = 'BIEZNIK_CZOLO')
  ) as zz
  where zz.productVersionId is not null
  and zz.operationId is not null
  and zz.resourceID is not null


-- DRUTÓWKA
UNION ALL
SELECT zz.productVersionId, zz.operationId, zz.resourceID, 0, 1, zz.wartosc
FROM (
SELECT 
  (select ww.id
  from [MwTech].[dbo].[ProductVersions] as ww
  inner join [MwTech].[dbo].[Products] as pp
  on ww.productId = pp.id and ww.DefaultVersion = 1
  where pp.MwbaseMatid = n.matid) as productVersionId,
  (select id 
  from [MwTech].[dbo].[Operations] as oo
  where oo.operationNumber = 'ODR_DRUTOWKA_PROD') as operationId,
  (select id 
  from [MwTech].[dbo].[Resources] as rr
  where rr.resourceNumber = 'ZO_MASZYNA_DRUTOWKA') as resourceId,
  n.wartosc
  FROM [mwbase].[fk].[danefk_materialy_normatywy] as n
  inner join mwbase.gm.materialy as m
  on m.matid = n.matid
  inner join [mwbase].[fk].[danefk_wskazniki] as w
  on w.pozid = n.wskaznik
  WHERE okres = @okres
  and w.symbol = 'WYDAJNOSC_8RBH_DRUTOWKA'
  and m.grupa3 = (select pozid from mwbase.gm.materialy_grupa3 where mnemonik = 'DRUTOWKA')
  ) as zz
  where zz.productVersionId is not null
  and zz.operationId is not null
  and zz.resourceID is not null

  -- DRUTÓWKA
UNION ALL
SELECT zz.productVersionId, zz.operationId, zz.resourceID, 0, 2, zz.wartosc
FROM (
SELECT 
  (select ww.id
  from [MwTech].[dbo].[ProductVersions] as ww
  inner join [MwTech].[dbo].[Products] as pp
  on ww.productId = pp.id and ww.DefaultVersion = 1
  where pp.MwbaseMatid = n.matid) as productVersionId,
  (select id 
  from [MwTech].[dbo].[Operations] as oo
  where oo.operationNumber = 'ODR_DRUTOWKA_OWIJKA') as operationId,
  (select id 
  from [MwTech].[dbo].[Resources] as rr
  where rr.resourceNumber = 'ZO_OSOBA') as resourceId,
  n.wartosc
  FROM [mwbase].[fk].[danefk_materialy_normatywy] as n
  inner join mwbase.gm.materialy as m
  on m.matid = n.matid
  inner join [mwbase].[fk].[danefk_wskazniki] as w
  on w.pozid = n.wskaznik
  WHERE okres = @okres
  and w.symbol = 'WYDAJNOSC_8RBH_OWIJKA'
  and m.grupa3 = (select pozid from mwbase.gm.materialy_grupa3 where mnemonik = 'DRUTOWKA')
  ) as zz
  where zz.productVersionId is not null
  and zz.operationId is not null
  and zz.resourceID is not null

-- APEX
UNION ALL
SELECT zz.productVersionId, zz.operationId, zz.resourceID, 0, 2, zz.wartosc
FROM (
SELECT 
  (select ww.id
  from [MwTech].[dbo].[ProductVersions] as ww
  inner join [MwTech].[dbo].[Products] as pp
  on ww.productId = pp.id and ww.DefaultVersion = 1
  where pp.MwbaseMatid = n.matid) as productVersionId,
  (select id 
  from [MwTech].[dbo].[Operations] as oo
  where oo.operationNumber = 'OAP_WYTL_APEXU') as operationId,
  (select id 
  from [MwTech].[dbo].[Resources] as rr
  where rr.resourceNumber = 'ZO_WYTL_APEX') as resourceId,
  n.wartosc
  FROM [mwbase].[fk].[danefk_materialy_normatywy] as n
  inner join mwbase.gm.materialy as m
  on m.matid = n.matid
  inner join [mwbase].[fk].[danefk_wskazniki] as w
  on w.pozid = n.wskaznik
  WHERE okres = @okres
  and w.symbol = 'WYDAJNOSC_KG_NA_8RBH_WY_APEX'
  and m.grupa3 = (select pozid from mwbase.gm.materialy_grupa3 where mnemonik = 'APEX')
  ) as zz
  where zz.productVersionId is not null
  and zz.operationId is not null
  and zz.resourceID is not null


-- CIĘCIE KORDU GUMOWANEGO
UNION ALL
SELECT zz.productVersionId, zz.operationId, zz.resourceID, 0, 2, zz.wartosc
FROM (
SELECT 
  (select ww.id
  from [MwTech].[dbo].[ProductVersions] as ww
  inner join [MwTech].[dbo].[Products] as pp
  on ww.productId = pp.id and ww.DefaultVersion = 1
  where pp.MwbaseMatid = n.matid) as productVersionId,
  (select id 
  from [MwTech].[dbo].[Operations] as oo
  where oo.operationNumber = 'OKG_CIECIE_KORDU') as operationId,
  (select id 
  from [MwTech].[dbo].[Resources] as rr
  where rr.resourceNumber = 'ZO_MASZYNA_PLASKA') as resourceId,
  n.wartosc
  FROM [mwbase].[fk].[danefk_materialy_normatywy] as n
  inner join mwbase.gm.materialy as m
  on m.matid = n.matid
  inner join [mwbase].[fk].[danefk_wskazniki] as w
  on w.pozid = n.wskaznik
  WHERE okres = @okres
  and w.symbol = 'WYDAJNOSC_M2_NA_8RBH_CIECIE_KORDU'
  and m.grupa3 = (select pozid from mwbase.gm.materialy_grupa3 where mnemonik = 'KORD_GUMOWANY_CIETY')
  ) as zz
  where zz.productVersionId is not null
  and zz.operationId is not null
  and zz.resourceID is not null


-- OPONA SUROWA
UNION ALL
SELECT zz.productVersionId, zz.operationId, zz.resourceID, 0, 2, zz.wartosc
FROM (
SELECT 
  (select ww.id
  from [MwTech].[dbo].[ProductVersions] as ww
  inner join [MwTech].[dbo].[Products] as pp
  on ww.productId = pp.id and ww.DefaultVersion = 1
  where pp.MwbaseMatid = n.matid) as productVersionId,
  (select id 
  from [MwTech].[dbo].[Operations] as oo
  where oo.operationNumber = 'OSU_KONFEKCJA') as operationId,
  (select id 
  from [MwTech].[dbo].[Resources] as rr
  where rr.resourceNumber = 'ZO_MASZYNA_KONFEKCYJNA') as resourceId,
  n.wartosc
  FROM [mwbase].[fk].[danefk_materialy_normatywy] as n
  inner join mwbase.gm.materialy as m
  on m.matid = n.matid
  inner join [mwbase].[fk].[danefk_wskazniki] as w
  on w.pozid = n.wskaznik
  WHERE okres = @okres
  and w.symbol = 'WYDAJNOSC_8RBH_KONFEKCJA'
  and m.grupa3 = (select pozid from mwbase.gm.materialy_grupa3 where mnemonik = 'OPONA_SUROW')
  ) as zz
  where zz.productVersionId is not null
  and zz.operationId is not null
  and zz.resourceID is not null


-- OPONA SUROWA
UNION ALL
SELECT zz.productVersionId, zz.operationId, zz.resourceID, 0, 2, zz.wartosc
FROM (
SELECT 
  (select ww.id
  from [MwTech].[dbo].[ProductVersions] as ww
  inner join [MwTech].[dbo].[Products] as pp
  on ww.productId = pp.id and ww.DefaultVersion = 1
  where pp.MwbaseMatid = n.matid) as productVersionId,
  (select id 
  from [MwTech].[dbo].[Operations] as oo
  where oo.operationNumber = 'OSU_MALOWANIE') as operationId,
  (select id 
  from [MwTech].[dbo].[Resources] as rr
  where rr.resourceNumber = 'ZO_OSOBA') as resourceId,
  n.wartosc
  FROM [mwbase].[fk].[danefk_materialy_normatywy] as n
  inner join mwbase.gm.materialy as m
  on m.matid = n.matid
  inner join [mwbase].[fk].[danefk_wskazniki] as w
  on w.pozid = n.wskaznik
  WHERE okres = @okres
  and w.symbol = 'CZAS_MALOWANIE_OPONY'
  and m.grupa3 = (select pozid from mwbase.gm.materialy_grupa3 where mnemonik = 'OPONA_SUROW')
  ) as zz
  where zz.productVersionId is not null
  and zz.operationId is not null
  and zz.resourceID is not null


-- OPONA WULKANIZOWANA
UNION ALL
SELECT zz.productVersionId, zz.operationId, zz.resourceID, zz.wartosc, 1, zz.wartosc
FROM (
SELECT 
  (select ww.id
  from [MwTech].[dbo].[ProductVersions] as ww
  inner join [MwTech].[dbo].[Products] as pp
  on ww.productId = pp.id and ww.DefaultVersion = 1
  where pp.MwbaseMatid = m.matid) as productVersionId,
  (select id 
  from [MwTech].[dbo].[Operations] as oo
  where oo.operationNumber = 'OWU_WULKANNIZACJA') as operationId,
  (select id 
  from [MwTech].[dbo].[Resources] as rr
  where rr.resourceNumber = 'ZO_OPONY_PRASA_WULK') as resourceId,
  w.czasWulkanizacji + w.czasZamykOtwier + w.czasPrzeladunku  as wartosc
  FROM [mwbase].[prdkabat].[opony_wulk] as i
		  INNER JOIN [mwbase].[prdkabat].[opony_wulk_wersje] as w
		  ON w.[oponaWulk] = i.[pozid]
		  INNER JOIN [mwbase].[gm].[materialy] as m
		  ON m.[matid] = i.matid
  ) as zz
  where zz.productVersionId is not null
  and zz.operationId is not null
  and zz.resourceID is not null

  -- OPONA WULKANIZOWANA
UNION ALL
SELECT zz.productVersionId, zz.operationId, zz.resourceID, zz.wartosc, 2, zz.wartosc
FROM (
SELECT 
  (select ww.id
  from [MwTech].[dbo].[ProductVersions] as ww
  inner join [MwTech].[dbo].[Products] as pp
  on ww.productId = pp.id and ww.DefaultVersion = 1
  where pp.MwbaseMatid = m.matid) as productVersionId,
  (select id 
  from [MwTech].[dbo].[Operations] as oo
  where oo.operationNumber = 'OWU_KJ') as operationId,
  (select id 
  from [MwTech].[dbo].[Resources] as rr
  where rr.resourceNumber = 'ZO_OSOBA') as resourceId,
  w.czasKj  as wartosc
  FROM [mwbase].[prdkabat].[opony_wulk] as i
		  INNER JOIN [mwbase].[prdkabat].[opony_wulk_wersje] as w
		  ON w.[oponaWulk] = i.[pozid]
		  INNER JOIN [mwbase].[gm].[materialy] as m
		  ON m.[matid] = i.matid
  ) as zz
  where zz.productVersionId is not null
  and zz.operationId is not null
  and zz.resourceID is not null

  )

  -- OSU-GT-169-28-14-1-L



