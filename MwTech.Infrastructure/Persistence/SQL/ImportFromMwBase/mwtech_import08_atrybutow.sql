-- IMPORT ATRYBUTÓW

-- -------------------------------------------------------------------------------------------------
-- BIEŻNIK [szt]

-- --------------------------------------------------------------------------------------------------
-- KAPA [gram]

-- --------------------------------------------------------------------------------------------------
-- APEX [gram]

-- --------------------------------------------------------------------------------------------------
-- DRUTÓWKA [szt]

-- --------------------------------------------------------------------------------------------------
-- OPONA SUROWA [szt]




delete from [MwTech].[dbo].[ProductVersionProperties]
DBCC CHECKIDENT('[MwTech].[dbo].[ProductVersionProperties]', RESEED, 1)



insert into [MwTech].[dbo].[ProductVersionProperties]
  (
	   [ProductVersionId]
      ,[PropertyId]
      ,[Value]
      ,[Text]
  )
  (
-- drutówki
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'odr_number_of_wires') as popertyId,
w.iloscDrutow as value,
'' as text

FROM [mwbase].[prdkabat].[drutowki] as i
	  INNER JOIN [mwbase].[prdkabat].[drutowki_wersje] as w
	  ON w.[drutowka] = i.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

	  
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and vv.MwbaseId = w.pozid
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'odr_number_of_wires'))

-- -------------------------------------------------------------------------------------------------------------------
union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'odr_number_of_wire_coils') as popertyId,
w.iloscZwojow as value,
'' as text

FROM [mwbase].[prdkabat].[drutowki] as i
	  INNER JOIN [mwbase].[prdkabat].[drutowki_wersje] as w
	  ON w.[drutowka] = i.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

	  
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and vv.MwbaseId = w.pozid
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'odr_number_of_wire_coils'))


-- -------------------------------------------------------------------------------------------------------------------
union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'odr_circ') as popertyId,
w.obwodCel as value,
'' as text

FROM [mwbase].[prdkabat].[drutowki] as i
	  INNER JOIN [mwbase].[prdkabat].[drutowki_wersje] as w
	  ON w.[drutowka] = i.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

	  
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and vv.MwbaseId = w.pozid
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'odr_circ'))

-- -------------------------------------------------------------------------------------------------------------------
union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'odr_over_lap') as popertyId,
w.zakladka as value,
'' as text

FROM [mwbase].[prdkabat].[drutowki] as i
	  INNER JOIN [mwbase].[prdkabat].[drutowki_wersje] as w
	  ON w.[drutowka] = i.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

	  
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and vv.MwbaseId = w.pozid
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'odr_over_lap'))


-- -------------------------------------------------------------------------------------------------------------------
union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oap_dimension_a ') as popertyId,
i.wymiarA as value,
'' as text

FROM [mwbase].[prdkabat].[apexy] as i
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = 1

	  
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oap_dimension_a '))

-- -------------------------------------------------------------------------------------------------------------------
union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oap_dimension_b ') as popertyId,
i.wymiarB as value,
'' as text

FROM [mwbase].[prdkabat].[apexy] as i
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = 1

	  
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oap_dimension_b '))

-- -------------------------------------------------------------------------------------------------------------------
union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oap_dimension_c ') as popertyId,
i.wymiarC as value,
'' as text

FROM [mwbase].[prdkabat].[apexy] as i
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = 1

	  
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oap_dimension_c '))

-- -------------------------------------------------------------------------------------------------------------------
union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obi_c_weight') as popertyId,
w.waga_cel as value,
'' as text

FROM [mwbase].[prdkabat].[biezniki] as i
	  INNER JOIN [mwbase].[prdkabat].[biezniki_wersje] as w
	  ON w.[bieznik] = i.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

	  where i.Typ = 0	
		
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and vv.MwbaseId = w.pozid
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obi_c_weight'))

	  -- -------------------------------------------------------------------------------------------------------------------
union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obi_c_length') as popertyId,
w.dlugosc_cel as value,
'' as text

FROM [mwbase].[prdkabat].[biezniki] as i
	  INNER JOIN [mwbase].[prdkabat].[biezniki_wersje] as w
	  ON w.[bieznik] = i.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

	  where i.Typ = 0	
		
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and vv.MwbaseId = w.pozid
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obi_c_length'))

	  -- -------------------------------------------------------------------------------------------------------------------
union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obi_c_width') as popertyId,
w.szer_cel as value,
'' as text

FROM [mwbase].[prdkabat].[biezniki] as i
	  INNER JOIN [mwbase].[prdkabat].[biezniki_wersje] as w
	  ON w.[bieznik] = i.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

	  where i.Typ = 0	
		
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and vv.MwbaseId = w.pozid
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obi_c_width'))


	  -- -------------------------------------------------------------------------------------------------------------------
union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obi_c_cross_section_area') as popertyId,
w.pole_przekroju as value,
'' as text

FROM [mwbase].[prdkabat].[biezniki] as i
	  INNER JOIN [mwbase].[prdkabat].[biezniki_wersje] as w
	  ON w.[bieznik] = i.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

	  where i.Typ = 0	
		
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and vv.MwbaseId = w.pozid
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obi_c_cross_section_area'))


	  -- -------------------------------------------------------------------------------------------------------------------
union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obi_b_weight') as popertyId,
w.waga_cel as value,
'' as text

FROM [mwbase].[prdkabat].[biezniki] as i
	  INNER JOIN [mwbase].[prdkabat].[biezniki_wersje] as w
	  ON w.[bieznik] = i.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

	  where i.Typ = 1	
		
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and vv.MwbaseId = w.pozid
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obi_b_weight'))

	  -- -------------------------------------------------------------------------------------------------------------------
union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obi_b_length') as popertyId,
w.dlugosc_cel as value,
'' as text

FROM [mwbase].[prdkabat].[biezniki] as i
	  INNER JOIN [mwbase].[prdkabat].[biezniki_wersje] as w
	  ON w.[bieznik] = i.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

	  where i.Typ = 1	
		
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and vv.MwbaseId = w.pozid
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obi_b_length'))

	  -- -------------------------------------------------------------------------------------------------------------------
union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obi_b_width') as popertyId,
w.szer_cel as value,
'' as text

FROM [mwbase].[prdkabat].[biezniki] as i
	  INNER JOIN [mwbase].[prdkabat].[biezniki_wersje] as w
	  ON w.[bieznik] = i.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

	  where i.Typ = 1	
		
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and vv.MwbaseId = w.pozid
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obi_b_width'))


-- -------------------------------------------------------------------------------------------------------------------

union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oka_height') as popertyId,
w.gruboscCel as value,
'' as text

FROM [mwbase].[prdkabat].[kapy] as i
	  INNER JOIN [mwbase].[prdkabat].[kapy_wersje] as w
	  ON w.[kapa] = i.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

	  where 1 = 1
		
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and vv.MwbaseId = w.pozid
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oka_height'))

-- -------------------------------------------------------------------------------------------------------------------

union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oka_length') as popertyId,
w.dlugoscCel as value,
'' as text

FROM [mwbase].[prdkabat].[kapy] as i
	  INNER JOIN [mwbase].[prdkabat].[kapy_wersje] as w
	  ON w.[kapa] = i.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

	  where 1 = 1
		
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and vv.MwbaseId = w.pozid
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oka_length'))

-- -------------------------------------------------------------------------------------------------------------------

union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'osu_closed_drum_circumference') as popertyId,
w.obwodBebnaKonf as value,
'' as text

FROM [mwbase].[prdkabat].[opony] as i
	  INNER JOIN [mwbase].[prdkabat].[opony_wersje] as w
	  ON w.[opona] = i.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

	  where 1 = 1
		
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and vv.MwbaseId = w.pozid
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'osu_closed_drum_circumference'))


-- -------------------------------------------------------------------------------------------------------------------

union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'osu_opened_drum_circumference') as popertyId,
w.obwodBebnaKonf2 as value,
'' as text

FROM [mwbase].[prdkabat].[opony] as i
	  INNER JOIN [mwbase].[prdkabat].[opony_wersje] as w
	  ON w.[opona] = i.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = w.numerWersji

	  where 1 = 1
		
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and vv.MwbaseId = w.pozid
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'osu_opened_drum_circumference'))

-- -------------------------------------------------------------------------------------------------------------------
union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'mie_density') as popertyId,
i.gestosc as value,
'' as text

FROM [mwbase].[prdkabat].[mieszanki] as i
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = 1

	  
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'mie_density'))

-- -------------------------------------------------------------------------------------------------------------------

-- -------------------------------------------------------------------------------------------------------------------
union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'dr_weight_mb') as popertyId,
i.waga as value,
'' as text

FROM [mwbase].[prdkabat].[druty] as i
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = 1

	  
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'dr_weight_mb'))


-- -------------------------------------------------------------------------------------------------------------------
union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oks_weight_m2') as popertyId,
0 as value,
'' as text

FROM [mwbase].[prdkabat].[kordy_surowe] as i
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = 1

	  
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oks_weight_m2'))

-- -------------------------------------------------------------------------------------------------------------------

-- -------------------------------------------------------------------------------------------------------------------
union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oks_bail_width') as popertyId,
i.szerokosc as value,
'' as text

FROM [mwbase].[prdkabat].[kordy_surowe] as i
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = 1

	  
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oks_bail_width'))


-- -------------------------------------------------------------------------------------------------------------------
union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'okg_bail_width') as popertyId,
i.szerokosc as value,
'' as text

FROM [mwbase].[prdkabat].[kordy_gumowane] as i
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = 1

	  
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'okg_bail_width'))

-- -------------------------------------------------------------------------------------------------------------------
union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'okg_bail_width') as popertyId,
i.szerokosc as value,
'' as text

FROM [mwbase].[prdkabat].[kordy_gumowane] as i
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = 1

	  
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'okg_bail_width'))

-- okg_weight_m2
-- -------------------------------------------------------------------------------------------------------------------
union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'okg_weight_m2') as popertyId,
i.wagaCel as value,
'' as text

FROM [mwbase].[prdkabat].[kordy_gumowane] as i
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = 1

	  
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'okg_weight_m2'))

-- okg_rubber_thickness
-- -------------------------------------------------------------------------------------------------------------------
union all
select 
tw.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'okg_rubber_thickness') as popertyId,
i.gruboscGumowania as value,
'' as text

FROM [mwbase].[prdkabat].[kordy_gumowane] as i
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  inner join [MwTech].[dbo].[ProductVersions] as tw
	  on tw.ProductId = tp.Id and tw.VersionNumber = 1

	  
	  and not exists
	  (select * from [MwTech].[dbo].[ProductVersionProperties] as bb 
	  inner join [MwTech].[dbo].[ProductVersions] as vv
	  on vv.id = bb.ProductVersionId
	  where bb.ProductVersionId = tw.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'okg_rubber_thickness'))


-- -------------------------------------------------------------------------------------------------------------------
-- koniec
  )





