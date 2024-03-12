/* (03) Import Wersji Produktów z MwBase */
/* mwtech_import03_bomow */

-- -------------------------------------------------------------------------------------------------
-- BIEŻNIK [szt]
-- OK
-- waga mieszanki bieznikowej [gram]					- wyliczona masa na 1 szt. bieżnika
-- waga mieszanki podkładowej [gram] (opcjonalnie)		- wyliczona masa na 1 szt. bieżnika


-- --------------------------------------------------------------------------------------------------
-- KAPA [gram]
-- OK
-- waga mieszanki na warstwę wewnętrzną opony [gram]	-  1 [gram] 

--	dodatkowo:
--  podajemy ilość mieszanki na metr bieżący [gram/mb]
--	wagę kapy wyliczamy dla konkretnej opony

-- --------------------------------------------------------------------------------------------------
-- APEX [gram]
-- OK
-- waga mieszanki na warstwę wewnętrzną opony [gram]	-  1 [gram] 

--	dodatkowo:
--  podajemy ilość mieszanki na metr bieżący [gram/mb]
--	wagę apexu wyliczamy dla konkretnej opony


-- --------------------------------------------------------------------------------------------------
-- DRUTÓWKA [szt]
-- OK
-- waga drutu	[gram]		-- wyliczamy
--							    [srednica_obliczeniowa] = [obwod]/PI + 1,2 x [ilość zwojów]
--							    [dlugosc_drutu] = ([srednica_obliczeniowa] x PI x [ilosc_drutow]  
--								+ [dlugosc_zakladki] x [ilość drutów]) x [ilość zwojów] 
--								[ciezar_drutu] [gram/metr] czytamy z bazy
--								[waga_drutu] = [dlugosc_drutu] x [ciezar_drutu]

-- ilość mieszanki	[gram]	-- wyliczamy proporcjonalnie do długości drutu	
--								[dlugosc_drutu] x 0,0010318 x 0,8						 
-- owijka					    [gram] kordu



-- --------------------------------------------------------------------------------------------------
-- OPONA SUROWA [szt]
-- BIEŻNIK_CZOŁO [szt]			-- 1 [szt]
-- BIEŻNIK_BOK [szt]			-- 2 [szt] (opcjonalnie)
-- DRUTÓWKA [szt]				-- 2 [szt]
-- APEX [gram]					-- wyliczamy 
--								-- lnWagaWylicz = ROUND(lnObwodBebna * 2 * 0.001 * iloscMieszankiNaMb,2)


DECLARE @gram_to_kg numeric(10,5) = 0.001  

delete from [MwTech].[dbo].[Boms]
DBCC CHECKIDENT('[MwTech].[dbo].[Boms]', RESEED, 1)



insert into [MwTech].[dbo].[Boms]
  (
       [SetId]
	  ,[SetVersionId]
      ,[PartId]
      ,[PartQty]
	  ,[PartLength]
	  ,[OrdinalNumber]
  )
  (

-- bieżniki - mieszanka podstawowa 
select 
tp.Id as SetId,
tw.Id as SetVersionId,
(select id from [MwTech].[dbo].[Products] as x where x.MwbaseMatid = ( select m.matid from mwbase.prdkabat.mieszanki as m where m.pozid = w.mieszanka)) as partId,
w.iloscMieszanki * @gram_to_kg as partQty,
0,
0

FROM [mwbase].[prdkabat].[biezniki] as i
	  INNER JOIN [mwbase].[prdkabat].[biezniki_wersje] as w
	  ON w.[bieznik] = i.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

	  where tp.ProductCategoryId IN (7,8)
	  and w.iloscMieszanki > 0
	  
	  and not exists
	  (select * from [MwTech].[dbo].[Boms] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.SetId
	  where bb.SetId = tw.id
	  and vv.MwbaseId = w.pozid
	  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x where x.MwbaseMatid = ( select m.matid from mwbase.prdkabat.mieszanki as m where m.pozid = w.mieszanka))
	  )

-- bieżniki - mieszanka kapa
union all 
select 
tp.Id as SetId,
tw.Id as SetVersionId,
(select id from [MwTech].[dbo].[Products] as x where x.MwbaseMatid = ( select m.matid from mwbase.prdkabat.mieszanki as m where m.pozid = w.mieszankaKapa)) as partId,
w.iloscMieszankiKapa * @gram_to_kg as partQty,
0,
0

FROM [mwbase].[prdkabat].[biezniki] as i
	  INNER JOIN [mwbase].[prdkabat].[biezniki_wersje] as w
	  ON w.[bieznik] = i.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

	  where tp.ProductCategoryId IN (7,8)
	  and w.iloscMieszankiKapa > 0
	  and not exists
	  (select * from [MwTech].[dbo].[Boms] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.SetId
	  where bb.SetId = tw.id
	  and vv.MwbaseId = w.pozid
	  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
	  where x.MwbaseMatid = ( select m.matid from mwbase.prdkabat.mieszanki as m where m.pozid = w.mieszankaKapa))
	  )


-- Kapa - mieszanka
-- 1 gram kapy == 1 gram mieszanki
union all 
select 
tp.Id as SetId,
tw.Id as SetVersionId,
(select id from [MwTech].[dbo].[Products] as x where x.MwbaseMatid = ( select m.matid from mwbase.prdkabat.mieszanki as m where m.pozid = w.mieszanka)) as partId,
1 as partQty,
0,
0

FROM [mwbase].[prdkabat].[kapy] as i
	  INNER JOIN [mwbase].[prdkabat].[kapy_wersje] as w
	  ON w.[kapa] = i.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

	  where tp.ProductCategoryId = 9
	  
	  and w.mieszanka is not null
	  and w.mieszanka > 0

	  and not exists
	  (select * from [MwTech].[dbo].[Boms] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.SetId
	  where bb.SetId = tw.id
	  and vv.MwbaseId = w.pozid
	  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
	  where x.MwbaseMatid = ( select m.matid from mwbase.prdkabat.mieszanki as m where m.pozid = w.mieszanka))
	  )


-- Apex - mieszanka
-- 1 gram Apexu == 1 gram mieszanki
union all 
select 
tp.Id as SetId,
tw.Id as SetVersionId,
(select id from [MwTech].[dbo].[Products] as x where x.MwbaseMatid = ( select m.matid from mwbase.prdkabat.mieszanki as m where m.pozid = i.mieszanka)) as partId,
1 as partQty,
0,
0

FROM [mwbase].[prdkabat].[apexy] as i
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = 1

	  where tp.ProductCategoryId = 13
	  
	  and i.mieszanka is not null
	  and i.mieszanka > 0

	  and not exists
	  (select * from [MwTech].[dbo].[Boms] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.SetId
	  where bb.SetId = tw.id
	  and vv.MwbaseId = i.pozid
	  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
	  where x.MwbaseMatid = ( select m.matid from mwbase.prdkabat.mieszanki as m where m.pozid = i.mieszanka))
	  )

-- Kord Gumowany - Mieszanka

union all 
select 
tp.Id as SetId,
tw.Id as SetVersionId,
(select id from [MwTech].[dbo].[Products] as x where x.MwbaseMatid = ( select m.matid from mwbase.prdkabat.mieszanki as m where m.pozid = i.mieszanka)) as partId,
i.iloscMieszankiNaKg * @gram_to_kg as partQty,
0,
0

FROM [mwbase].[prdkabat].[kordy_gumowane] as i
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = 1

	  where tp.ProductCategoryId = 16
	  
	  and i.mieszanka is not null
	  and i.mieszanka > 0

	  and not exists
	  (select * from [MwTech].[dbo].[Boms] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.SetId
	  where bb.SetId = tw.id
	  and vv.MwbaseId = i.pozid
	  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
	  where x.MwbaseMatid = ( select m.matid from mwbase.prdkabat.mieszanki as m where m.pozid = i.mieszanka))
	  )

-- Kord Gumowany - Kord Surowy

union all 
select 
tp.Id as SetId,
tw.Id as SetVersionId,
(select id from [MwTech].[dbo].[Products] as x where x.MwbaseMatid = ( select m.matid from mwbase.prdkabat.kordy_surowe as m where m.pozid = i.kordSurowy)) as partId,
i.iloscKorduNaKg * @gram_to_kg as partQty,
0,
0

FROM [mwbase].[prdkabat].[kordy_gumowane] as i
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = 1

	  where tp.ProductCategoryId = 16
	  
	  and i.kordSurowy is not null
	  and i.kordSurowy > 0

	  and not exists
	  (select * from [MwTech].[dbo].[Boms] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.SetId
	  where bb.SetId = tw.id
	  and vv.MwbaseId = i.pozid
	  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
	  where x.MwbaseMatid = ( select m.matid from mwbase.prdkabat.kordy_surowe as m where m.pozid = i.kordSurowy))
	  )

-- Tkanina Ochronna - Kord Gumowany

union all 
select 
tp.Id as SetId,
tw.Id as SetVersionId,
(select id from [MwTech].[dbo].[Products] as x where x.MwbaseMatid = ( select m.matid from mwbase.prdkabat.kordy_gumowane as m where m.pozid = i.kordGumowany)) as partId,
1 as partQty,
0,
0

FROM [mwbase].[prdkabat].[tkaniny_ochronne] as i
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = 1

	  where tp.ProductCategoryId = 20
	  
	  and i.kordGumowany is not null
	  and i.kordGumowany > 0

	  and not exists
	  (select * from [MwTech].[dbo].[Boms] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.SetId
	  where bb.SetId = tw.id
	  and vv.MwbaseId = i.pozid
	  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
	  where x.MwbaseMatid = ( select m.matid from mwbase.prdkabat.kordy_gumowane as m where m.pozid = i.kordGumowany))
	  )

-- drutówki - mieszanka

	union all 

	select 
	tp.Id as SetId,
	tw.Id as SetVersionId,
	(select id from [MwTech].[dbo].[Products] as x
	where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.mieszanki as xb where xb.pozid = w.mieszanka)) as partId,
	w.iloscMieszanki * @gram_to_kg as partQty,
	0,
	0

	FROM [mwbase].[prdkabat].[drutowki] as i
		  INNER JOIN [mwbase].[prdkabat].[drutowki_wersje] as w
		  ON w.[drutowka] = i.[pozid]
		  INNER JOIN [mwbase].[gm].[materialy] as m
		  ON m.[matid] = i.matid
  		  inner join [MwTech].[dbo].[Products] as tp
		  on tp.MwbaseMatid = i.matId

		  inner join [MwTech].[dbo].[ProductVersions] as tw
		  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

		  where tp.ProductCategoryId = 10
		  and w.mieszanka is not null

		  and not exists
		  (select * from [MwTech].[dbo].[Boms] as bb 
		  inner join [MwTech].[dbo].[ProductVersions] as vv
		  on vv.id = bb.SetId
		  where bb.SetId = tw.id
		  and vv.MwbaseId = w.pozid
		  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
		  where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.mieszanki as xb where xb.pozid = w.mieszanka))
		  )


	-- drutówki - drut

	union all 

	select 
	tp.Id as SetId,
	tw.Id as SetVersionId,
	(select id from [MwTech].[dbo].[Products] as x
	where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.druty as xb where xb.pozid = w.drut)) as partId,
	w.wagaDrutu * @gram_to_kg as partQty,
	0,
	0
	
	FROM [mwbase].[prdkabat].[drutowki] as i
		  INNER JOIN [mwbase].[prdkabat].[drutowki_wersje] as w
		  ON w.[drutowka] = i.[pozid]
		  INNER JOIN [mwbase].[gm].[materialy] as m
		  ON m.[matid] = i.matid
  		  inner join [MwTech].[dbo].[Products] as tp
		  on tp.MwbaseMatid = i.matId

		  inner join [MwTech].[dbo].[ProductVersions] as tw
		  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

		  where tp.ProductCategoryId = 10
		  and w.drut is not null

		  and not exists
		  (select * from [MwTech].[dbo].[Boms] as bb 
		  inner join [MwTech].[dbo].[ProductVersions] as vv
		  on vv.id = bb.SetId
		  where bb.SetId = tw.id
		  and vv.MwbaseId = w.pozid
		  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
		  where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.druty as xb where xb.pozid = w.drut))
		  )


		  -- drutówki - owijka

	union all 

	select 
	tp.Id as SetId,
	tw.Id as SetVersionId,
	(select id from [MwTech].[dbo].[Products] as x
	where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.kordy_surowe as xb where xb.pozid = w.owijka)) as partId,
	w.owijkaWaga * @gram_to_kg as partQty,
	0,
	0
	
	FROM [mwbase].[prdkabat].[drutowki] as i
		  INNER JOIN [mwbase].[prdkabat].[drutowki_wersje] as w
		  ON w.[drutowka] = i.[pozid]
		  INNER JOIN [mwbase].[gm].[materialy] as m
		  ON m.[matid] = i.matid
  		  inner join [MwTech].[dbo].[Products] as tp
		  on tp.MwbaseMatid = i.matId

		  inner join [MwTech].[dbo].[ProductVersions] as tw
		  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

		  where tp.ProductCategoryId = 10
		  and w.owijka is not null

		  and not exists
		  (select * from [MwTech].[dbo].[Boms] as bb 
		  inner join [MwTech].[dbo].[ProductVersions] as vv
		  on vv.id = bb.SetId
		  where bb.SetId = tw.id
		  and vv.MwbaseId = w.pozid
		  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
		  where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.kordy_surowe as xb where xb.pozid = w.owijka))
		  )

-- drutówka + apex , drutówka

	union all 

	select 
	tp.Id as SetId,
	tw.Id as SetVersionId,
	(select id from [MwTech].[dbo].[Products] as x
	where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.drutowki as xb where xb.pozid = i.drutowka)) as partId,
	1 as partQty,
	0,
	0
	
	FROM [mwbase].[prdkabat].[apexy_drutowki] as i
		  INNER JOIN [mwbase].[gm].[materialy] as m
		  ON m.[matid] = i.matid
  		  inner join [MwTech].[dbo].[Products] as tp
		  on tp.MwbaseMatid = i.matId

		  inner join [MwTech].[dbo].[ProductVersions] as tw
		  on tw.ProductId = tp.Id and tw.VersionNumber = 1

		  where tp.ProductCategoryId = 14
		  and i.drutowka is not null

		  
		  and not exists
		  (select * from [MwTech].[dbo].[Boms] as bb 
		  inner join [MwTech].[dbo].[ProductVersions] as vv
		  on vv.id = bb.SetId
		  where bb.SetId = tw.id
		  and vv.MwbaseId = i.pozid
		  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
		  where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.drutowki as xb where xb.pozid = i.drutowka))
		  )

-- drutówka + apex , apex
	
	union all 

	select 
	tp.Id as SetId,
	tw.Id as SetVersionId,
	(select id from [MwTech].[dbo].[Products] as x
	where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.apexy as xb where xb.pozid = i.apex)) as partId,
    i.apexIloscGram * @gram_to_kg,
	0,
	0
	
	FROM [mwbase].[prdkabat].[apexy_drutowki] as i
		  INNER JOIN [mwbase].[gm].[materialy] as m
		  ON m.[matid] = i.matid
  		
		  left join mwbase.prdkabat.apexy as ap
		  ON ap.pozid = i.apex
		  
		  inner join [MwTech].[dbo].[Products] as tp
		  on tp.MwbaseMatid = i.matId

		  inner join [MwTech].[dbo].[ProductVersions] as tw
		  on tw.ProductId = tp.Id and tw.VersionNumber = 1

		  where tp.ProductCategoryId = 14
		  and i.apex is not null

		  and not exists
		  (select * from [MwTech].[dbo].[Boms] as bb 
		  inner join [MwTech].[dbo].[ProductVersions] as vv
		  on vv.id = bb.SetId
		  where bb.SetId = tw.id
		  and vv.MwbaseId = i.pozid
		  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
		  where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.apexy as xb where xb.pozid = i.apex))
		  )


    -- opony, bieżnik czoło

	union all 

	select 
	tp.Id as SetId,
	tw.Id as SetVersionId,
	(select id from [MwTech].[dbo].[Products] as x
	where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.biezniki as xb where xb.pozid = w.bieznik)) as partId,
	1 as partQty,
	0,
	0
	
	FROM [mwbase].[prdkabat].[opony] as i
		  INNER JOIN [mwbase].[prdkabat].[opony_wersje] as w
		  ON w.[opona] = i.[pozid]
		  INNER JOIN [mwbase].[gm].[materialy] as m
		  ON m.[matid] = i.matid
  		  inner join [MwTech].[dbo].[Products] as tp
		  on tp.MwbaseMatid = i.matId

		  inner join [MwTech].[dbo].[ProductVersions] as tw
		  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

		  where tp.ProductCategoryId = 12
		  and w.bieznik is not null
		  and not exists
		  (select * from [MwTech].[dbo].[Boms] as bb 
		  inner join [MwTech].[dbo].[ProductVersions] as vv
		  on vv.id = bb.SetId
		  where bb.SetId = tw.id
		  and vv.MwbaseId = w.pozid
		  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
		  where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.biezniki as xb where xb.pozid = w.bieznik))
		  )

	-- opony, bieżnik bok

	union all 

	select 
	tp.Id as SetId,
	tw.Id as SetVersionId,
	(select id from [MwTech].[dbo].[Products] as x
	where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.biezniki as xb where xb.pozid = w.bok)) as partId,
	2 as partQty,
	0,
	0
	
	FROM [mwbase].[prdkabat].[opony] as i
		  INNER JOIN [mwbase].[prdkabat].[opony_wersje] as w
		  ON w.[opona] = i.[pozid]
		  INNER JOIN [mwbase].[gm].[materialy] as m
		  ON m.[matid] = i.matid
  		  inner join [MwTech].[dbo].[Products] as tp
		  on tp.MwbaseMatid = i.matId

		  inner join [MwTech].[dbo].[ProductVersions] as tw
		  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

		  where tp.ProductCategoryId = 12
		  and w.bok is not null
		  and not exists
		  (select * from [MwTech].[dbo].[Boms] as bb 
		  inner join [MwTech].[dbo].[ProductVersions] as vv
		  on vv.id = bb.SetId
		  where bb.SetId = tw.id
		  and vv.MwbaseId = w.pozid
		  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
		  where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.biezniki as xb where xb.pozid = w.bok))
		  )



-- opona surowa, kapa

	union all 

	select 
	tp.Id as SetId,
	tw.Id as SetVersionId,
	(select id from [MwTech].[dbo].[Products] as x
	where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.kapy as xb where xb.pozid = w.kapa)) as partId,
    CAST(coalesce( ROUND(((w.kapaDlugosc *  kw.szerokoscCel *  kw.gruboscCel)/1000) * km.gestosc,2) ,0) as numeric(10,2)) * @gram_to_kg  as kapa_waga_gram,
	w.kapaDlugosc as partLength,
	0
	
	FROM [mwbase].[prdkabat].[opony] as i
		  INNER JOIN [mwbase].[prdkabat].[opony_wersje] as w
		  ON w.[opona] = i.[pozid]
		  INNER JOIN [mwbase].[gm].[materialy] as m
		  ON m.[matid] = i.matid
  		  
		left join mwbase.prdkabat.kapy as k
		ON k.pozid = w.kapa
		left join mwbase.prdkabat.kapy_wersje as kw
		ON kw.kapa = k.pozid AND kw.pozid = k.wersja
		left join mwbase.prdkabat.mieszanki as km
		ON kw.mieszanka = km.pozid
		  
		  inner join [MwTech].[dbo].[Products] as tp
		  on tp.MwbaseMatid = i.matId

		  inner join [MwTech].[dbo].[ProductVersions] as tw
		  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

		  where tp.ProductCategoryId = 12
		  and w.kapa is not null

		  and not exists
		  (select * from [MwTech].[dbo].[Boms] as bb 
		  inner join [MwTech].[dbo].[ProductVersions] as vv
		  on vv.id = bb.SetId
		  where bb.SetId = tw.id
		  and vv.MwbaseId = w.pozid
		  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
		  where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.kapy as xb where xb.pozid = w.kapa))
		  )

	-- opona surowa, kapa - doklejka

	union all 

	select 
	tp.Id as SetId,
	tw.Id as SetVersionId,
	(select id from [MwTech].[dbo].[Products] as x
	where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.kapy as xb where xb.pozid = w.kapaDok)) as partId,
    CAST(coalesce( ROUND(((w.kapaDokDlugosc * w.kapaDokSzt *  kw.szerokoscCel *  kw.gruboscCel)/1000) * km.gestosc,2) ,0) as numeric(10,2)) * @gram_to_kg as kapa_waga_gram,
	w.kapaDokDlugosc * w.kapaDokSzt as partLength,
	0
	
	FROM [mwbase].[prdkabat].[opony] as i
		  INNER JOIN [mwbase].[prdkabat].[opony_wersje] as w
		  ON w.[opona] = i.[pozid]
		  INNER JOIN [mwbase].[gm].[materialy] as m
		  ON m.[matid] = i.matid
  		  
		left join mwbase.prdkabat.kapy as k
		ON k.pozid = w.kapaDok
		left join mwbase.prdkabat.kapy_wersje as kw
		ON kw.kapa = k.pozid AND kw.pozid = k.wersja
		left join mwbase.prdkabat.mieszanki as km
		ON kw.mieszanka = km.pozid
		  
		  inner join [MwTech].[dbo].[Products] as tp
		  on tp.MwbaseMatid = i.matId

		  inner join [MwTech].[dbo].[ProductVersions] as tw
		  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

		  where tp.ProductCategoryId = 12
		  and w.kapaDok is not null
		  and w.kapaDokDlugosc > 0
		  and w.kapaDokSzt > 0

		  and not exists
		  (select * from [MwTech].[dbo].[Boms] as bb 
		  inner join [MwTech].[dbo].[ProductVersions] as vv
		  on vv.id = bb.SetId
		  where bb.SetId = tw.id
		  and vv.MwbaseId = w.pozid
		  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
		  where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.kapy as xb where xb.pozid = w.kapadok))
		  )

-- opona surowa, kapa - 2

	union all 

	select 
	tp.Id as SetId,
	tw.Id as SetVersionId,
	(select id from [MwTech].[dbo].[Products] as x
	where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.kapy as xb where xb.pozid = w.kapa2)) as partId,
    CAST(coalesce( ROUND(((w.kapa2Dlugosc * w.kapa2Szt *  kw.szerokoscCel *  kw.gruboscCel)/1000) * km.gestosc,2) ,0) as numeric(10,2)) * @gram_to_kg as kapa_waga_gram,
	w.kapa2Dlugosc * w.kapa2Szt as partLength,
	0
	
	FROM [mwbase].[prdkabat].[opony] as i
		  INNER JOIN [mwbase].[prdkabat].[opony_wersje] as w
		  ON w.[opona] = i.[pozid]
		  INNER JOIN [mwbase].[gm].[materialy] as m
		  ON m.[matid] = i.matid
  		  
		left join mwbase.prdkabat.kapy as k
		ON k.pozid = w.kapa2
		left join mwbase.prdkabat.kapy_wersje as kw
		ON kw.kapa = k.pozid AND kw.pozid = k.wersja
		left join mwbase.prdkabat.mieszanki as km
		ON kw.mieszanka = km.pozid
		  
		  inner join [MwTech].[dbo].[Products] as tp
		  on tp.MwbaseMatid = i.matId

		  inner join [MwTech].[dbo].[ProductVersions] as tw
		  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

		  where tp.ProductCategoryId = 12
		  and w.kapa2 is not null
		  and w.kapa2 > 0
		  and w.kapa2Szt > 0

		  and not exists
		  (select * from [MwTech].[dbo].[Boms] as bb 
		  inner join [MwTech].[dbo].[ProductVersions] as vv
		  on vv.id = bb.SetId
		  where bb.SetId = tw.id
		  and vv.MwbaseId = w.pozid
		  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
		  where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.kapy as xb where xb.pozid = w.kapa2))
		  )


 -- opona surowa, drutówka --- tylko gdy nie drutówka+apex

	union all 

	select 
	tp.Id as SetId,
	tw.Id as SetVersionId,
	(select id from [MwTech].[dbo].[Products] as x
	where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.drutowki as xb where xb.pozid = w.drutowka)) as partId,
	2 as partQty,
	0,
	0
	
	FROM [mwbase].[prdkabat].[opony] as i
		  INNER JOIN [mwbase].[prdkabat].[opony_wersje] as w
		  ON w.[opona] = i.[pozid]
		  INNER JOIN [mwbase].[gm].[materialy] as m
		  ON m.[matid] = i.matid
  		  inner join [MwTech].[dbo].[Products] as tp
		  on tp.MwbaseMatid = i.matId

		  inner join [MwTech].[dbo].[ProductVersions] as tw
		  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

		  where tp.ProductCategoryId = 12
		  and w.drutowka is not null
		  and w.apexDrutowka is null
		  
		  and not exists
		  (select * from [MwTech].[dbo].[Boms] as bb 
		  inner join [MwTech].[dbo].[ProductVersions] as vv
		  on vv.id = bb.SetId
		  where bb.SetId = tw.id
		  and vv.MwbaseId = w.pozid
		  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
		  where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.drutowki as xb where xb.pozid = w.drutowka))
		  )

-- opona surowa, apex --- tylko gdy nie drutówka+apex
	union all 

	select 
	tp.Id as SetId,
	tw.Id as SetVersionId,
	(select id from [MwTech].[dbo].[Products] as x
	where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.apexy as xb where xb.pozid = w.apex)) as partId,
--  CAST(coalesce( ROUND(ap.iloscMieszankiNaMb * 2 * 0.001 * w.obwodBebnaKonf,2),0) as numeric(10,2)) as apexIloscGramWylicz,
	w.apexIloscGram * @gram_to_kg as apexIloscGram,
	w.obwodBebnaKonf as partLength,
	0
	
	FROM [mwbase].[prdkabat].[opony] as i
		  INNER JOIN [mwbase].[prdkabat].[opony_wersje] as w
		  ON w.[opona] = i.[pozid]
		  INNER JOIN [mwbase].[gm].[materialy] as m
		  ON m.[matid] = i.matid
  		
		  left join mwbase.prdkabat.apexy as ap
		  ON ap.pozid = w.apex
		  
		  inner join [MwTech].[dbo].[Products] as tp
		  on tp.MwbaseMatid = i.matId

		  inner join [MwTech].[dbo].[ProductVersions] as tw
		  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

		  where tp.ProductCategoryId = 12
		  and w.apex is not null
		  and w.apexDrutowka is null

		  and not exists
		  (select * from [MwTech].[dbo].[Boms] as bb 
		  inner join [MwTech].[dbo].[ProductVersions] as vv
		  on vv.id = bb.SetId
		  where bb.SetId = tw.id
		  and vv.MwbaseId = w.pozid
		  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
		  where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.apexy as xb where xb.pozid = w.apex))
		  )

-- opona surowa, apex+drutówka
	union all 

	select 
	tp.Id as SetId,
	tw.Id as SetVersionId,
	(select id from [MwTech].[dbo].[Products] as x
	where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.apexy_drutowki as xb where xb.pozid = w.apexDrutowka)) as partId,
--  CAST(coalesce( ROUND(ap.iloscMieszankiNaMb * 2 * 0.001 * w.obwodBebnaKonf,2),0) as numeric(10,2)) as apexIloscGramWylicz,
	2 as Ilosc,
	w.obwodBebnaKonf as partLength,
	0
	
	FROM [mwbase].[prdkabat].[opony] as i
		  INNER JOIN [mwbase].[prdkabat].[opony_wersje] as w
		  ON w.[opona] = i.[pozid]
		  INNER JOIN [mwbase].[gm].[materialy] as m
		  ON m.[matid] = i.matid
  		
		  left join mwbase.prdkabat.apexy_drutowki as ap
		  ON ap.pozid = w.apexDrutowka
		  
		  inner join [MwTech].[dbo].[Products] as tp
		  on tp.MwbaseMatid = i.matId

		  inner join [MwTech].[dbo].[ProductVersions] as tw
		  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

		  where tp.ProductCategoryId = 12
		  and w.apexDrutowka is not null

		  and not exists
		  (select * from [MwTech].[dbo].[Boms] as bb 
		  inner join [MwTech].[dbo].[ProductVersions] as vv
		  on vv.id = bb.SetId
		  where bb.SetId = tw.id
		  and vv.MwbaseId = w.pozid
		  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
		  where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.apexy_drutowki as xb where xb.pozid = w.apexDrutowka))
		  )

--   Opona Surowa - Tkanina ochronna
--	,CAST( ROUND( o.cal * 25.4 * PI() * CAST(coalesce(tko.szerokosc,0)  as INT) * coalesce(tkoKG.wagaCel,0) * 2 * 0.000001 ,2) as numeric(10,2)) as tkaninaOchronnaWagaGram

union all 

	select 
	tp.Id as SetId,
	tw.Id as SetVersionId,
	(select id from [MwTech].[dbo].[Products] as x
	where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.tkaniny_ochronne as xb where xb.pozid = w.tkaninaOchronna)) as partId,
    ROUND( i.cal * 25.4 * PI() * CAST(coalesce(tko.szerokosc,0)  as INT) * coalesce(tkoKG.wagaCel,0) * 2 * 0.000001 ,2) * @gram_to_kg,
	0,
	0
	
	FROM [mwbase].[prdkabat].[opony] as i
		  INNER JOIN [mwbase].[prdkabat].[opony_wersje] as w
		  ON w.[opona] = i.[pozid]
		  INNER JOIN [mwbase].[gm].[materialy] as m
		  ON m.[matid] = i.matid
  		
		  left join mwbase.prdkabat.tkaniny_ochronne as tko
		  ON tko.pozid = w.tkaninaOchronna
		  left join mwbase.prdkabat.kordy_gumowane as tkoKG
		  ON tkoKG.pozid = tko.kordGumowany

		  inner join [MwTech].[dbo].[Products] as tp
		  on tp.MwbaseMatid = i.matId

		  inner join [MwTech].[dbo].[ProductVersions] as tw
		  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

		  where tp.ProductCategoryId = 12
		  and w.tkaninaOchronna is not null


		  and not exists
		  (select * from [MwTech].[dbo].[Boms] as bb 
		  inner join [MwTech].[dbo].[ProductVersions] as vv
		  on vv.id = bb.SetId
		  where bb.SetId = tw.id
		  and vv.MwbaseId = w.pozid
		  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
		  where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.tkaniny_ochronne as xb where xb.pozid = w.tkaninaOchronna))
		  )


------------------------------------------------- opona surowa -> kord gumowany cięty -------------------------------------------------
	union all 

	select 
	tp.Id as SetId,
	tw.Id as SetVersionId,
	(select id from [MwTech].[dbo].[Products] as x
	where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.kordy_gumowane_ciete as xb where xb.pozid = owkc.kordCiety)) as partId,
    
	CAST(CASE owkc.warstwa
	when 0 then ( ((w.obwodBebnaKonf + 2 + 16) * kgc.szerokosc)/1000000) * owkc.ilosc * tkoKG.wagaCel
	ELSE (((w.obwodBebnaKonf + 2 + ((owkc.warstwa - 1) * 8)) * kgc.szerokosc)/1000000) * owkc.ilosc * tkoKG.wagaCel
	END as numeric(10,5)) * @gram_to_kg
	AS ile_gram_na_opone,
	0,
	owkc.warstwa

	
	FROM [mwbase].[prdkabat].[opony] as i
		  INNER JOIN [mwbase].[prdkabat].[opony_wersje] as w
		  ON w.[opona] = i.[pozid]
		  INNER JOIN [mwbase].[prdkabat].[opony_kordy_gumowane_ciete] as owkc
		  on owkc.opona = w.opona and owkc.wersja = w.pozid

		  INNER JOIN [mwbase].[gm].[materialy] as m
		  ON m.[matid] = i.matid
  		
		  left join mwbase.prdkabat.kordy_gumowane_ciete as kgc
		  ON kgc.pozid = owkc.kordCiety
		  left join mwbase.prdkabat.kordy_gumowane as tkoKG
		  ON tkoKG.pozid = kgc.kordGumowany

		  inner join [MwTech].[dbo].[Products] as tp
		  on tp.MwbaseMatid = i.matId

		  inner join [MwTech].[dbo].[ProductVersions] as tw
		  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

		  where tp.ProductCategoryId = 12

		
		  and not exists
		  (select * from [MwTech].[dbo].[Boms] as bb 
		  inner join [MwTech].[dbo].[ProductVersions] as vv
		  on vv.id = bb.SetId
		  where bb.SetId = tw.id
		  and vv.MwbaseId = w.pozid
		  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
		  where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.kordy_gumowane_ciete as xb where xb.pozid = owkc.kordCiety))
		  )


-- Opona Surowa - Preparaty
	union all 

	select 
	tp.Id as SetId,
	tw.Id as SetVersionId,
	(select id from [MwTech].[dbo].[Products] as x
	where x.MwbaseMatid = w.malowPrep1MatId) as partId,
    w.malowPrep1Ilosc * @gram_to_kg,
	0,
	0
	
	FROM [mwbase].[prdkabat].[opony] as i
		  INNER JOIN [mwbase].[prdkabat].[opony_wersje] as w
		  ON w.[opona] = i.[pozid]
		  INNER JOIN [mwbase].[gm].[materialy] as m
		  ON m.[matid] = i.matid
  				  		  
		  inner join [MwTech].[dbo].[Products] as tp
		  on tp.MwbaseMatid = i.matId

		  inner join [MwTech].[dbo].[ProductVersions] as tw
		  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

		  where tp.ProductCategoryId = 12
		  and w.malowPrep1MatId is not null
		  and w.malowPrep1Ilosc > 0

		  and not exists
		  (select * from [MwTech].[dbo].[Boms] as bb 
		  inner join [MwTech].[dbo].[ProductVersions] as vv
		  on vv.id = bb.SetId
		  where bb.SetId = tw.id
		  and vv.MwbaseId = w.pozid
		  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
		  where x.MwbaseMatid = w.malowPrep1MatId)
		  )


-- opona wulkanizowana -> opona surowa

	union all 

	select 
	tp.Id as SetId,
	tw.Id as SetVersionId,
	(select id from [MwTech].[dbo].[Products] as x
	where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.opony as xb where xb.pozid = w.oponaSurowa)) as partId,
    1,
	0,
	0
	
	FROM [mwbase].[prdkabat].[opony_wulk] as i
		  INNER JOIN [mwbase].[prdkabat].[opony_wulk_wersje] as w
		  ON w.[oponaWulk] = i.[pozid]
		  INNER JOIN [mwbase].[gm].[materialy] as m
		  ON m.[matid] = i.matid
  		  
		left join mwbase.prdkabat.opony as k
		ON k.pozid = w.oponaSurowa
		left join mwbase.prdkabat.opony_wersje as kw
		ON kw.opona = k.pozid AND kw.pozid = k.wersja

  
		  inner join [MwTech].[dbo].[Products] as tp
		  on tp.MwbaseMatid = i.matId

		  inner join [MwTech].[dbo].[ProductVersions] as tw
		  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

		  where tp.ProductCategoryId = 11
		  and w.oponaSurowa is not null
		  and w.oponaSurowa != 0

		  and not exists
		  (select * from [MwTech].[dbo].[Boms] as bb 
		  inner join [MwTech].[dbo].[ProductVersions] as vv
		  on vv.id = bb.SetId
		  where bb.SetId = tw.id
		  and vv.MwbaseId = w.pozid
		  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
		  where x.MwbaseMatid = ( select xb.matid from mwbase.prdkabat.opony as xb where xb.pozid = w.oponaSurowa))
		  )
--

-- Kord Gumowany Cięty - Kord Gumowany

union all 
select 
tp.Id as SetId,
tw.Id as SetVersionId,
(select id from [MwTech].[dbo].[Products] as x 
 where x.MwbaseMatid = ( select m.matid from mwbase.prdkabat.kordy_gumowane as m where m.pozid = i.kordGumowany)) as partId,
1 as partQty,
0,
0

FROM [mwbase].[prdkabat].[kordy_gumowane_ciete] as i
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = 1

	  where tp.ProductCategoryId = 15
	  
	  and i.kordGumowany is not null
	  and i.kordGumowany > 0

	  and not exists
	  (select * from [MwTech].[dbo].[Boms] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.SetId
	  where bb.SetId = tw.id
	  and vv.MwbaseId = i.pozid
	  and bb.PartId = (select id from [MwTech].[dbo].[Products] as x 
	  where x.MwbaseMatid = ( select m.matid from mwbase.prdkabat.kordy_gumowane as m where m.pozid = i.kordGumowany))
	  )



-- koniec
  )

  -- OSU-GT-169-28-14-1-L



