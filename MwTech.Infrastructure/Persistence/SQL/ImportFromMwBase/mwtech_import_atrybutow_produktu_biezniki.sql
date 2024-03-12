-- IMPORT ATRYBUTÓW


-- KAPA [gram]

-- --------------------------------------------------------------------------------------------------
-- APEX [gram]

-- --------------------------------------------------------------------------------------------------
-- DRUTÓWKA [szt]

-- --------------------------------------------------------------------------------------------------
-- OPONA SUROWA [szt]




delete from [MwTech].[dbo].[ProductProperties]
DBCC CHECKIDENT('[MwTech].[dbo].[ProductProperties]', RESEED, 1)



insert into [MwTech].[dbo].[ProductProperties]
  (
	   [ProductId]
      ,[PropertyId]
      ,[MinValue]
	  ,[Value]
	  ,[MaxValue]
      ,[Text]
  )
  (


-- -------------------------------------------------------------------------------------------------
-- BIEŻNIK CZOŁO [szt]
-- -------------------------------------------------------------------------------------------------

select 
tp.Id as ProductId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obc_weight') as popertyId,
w.waga_min as MinValue,
w.waga_cel as CelValue,
w.waga_max as MaxValue,
'' as text

FROM [mwbase].[prdkabat].[biezniki] as i
	  INNER JOIN [mwbase].[prdkabat].[biezniki_wersje] as w
	  ON w.[bieznik] = i.[pozid] and i.wersja = w.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  where i.Typ = 0	
	 
	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obc_weight'))



-- -------------------------------------------------------------------------------------------------------------------
union all

select 
tp.Id as ProductId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obc_length') as popertyId,
w.dlugosc_min as MinValue,
w.dlugosc_cel as CelValue,
w.dlugosc_max as MaxValue,
'' as text

FROM [mwbase].[prdkabat].[biezniki] as i
	  INNER JOIN [mwbase].[prdkabat].[biezniki_wersje] as w
	  ON w.[bieznik] = i.[pozid]  and i.wersja = w.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  where i.Typ = 0	
	
	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obc_length'))
	  

-- -------------------------------------------------------------------------------------------------------------------
union all

select 
tp.Id as ProductId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obc_width') as popertyId,
w.szer_min as MinValue,
w.szer_cel as CelValue,
w.szer_max as MaxValue,
'' as text

FROM [mwbase].[prdkabat].[biezniki] as i
	  INNER JOIN [mwbase].[prdkabat].[biezniki_wersje] as w
	  ON w.[bieznik] = i.[pozid]  and i.wersja = w.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  where i.Typ = 0	
		
	  	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obc_width'))


-- -------------------------------------------------------------------------------------------------------------------

union all

select 
tp.Id as ProductId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obc_cross_section_area') as popertyId,
null as MinValue,
w.pole_przekroju as CelValue,
null as MaxValue,
'' as text

FROM [mwbase].[prdkabat].[biezniki] as i
	  INNER JOIN [mwbase].[prdkabat].[biezniki_wersje] as w
	  ON w.[bieznik] = i.[pozid]  and i.wersja = w.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  where i.Typ = 0	
		
	  	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obc_cross_section_area'))

	  -- -------------------------------------------------------------------------------------------------------------------

union all

select 
tp.Id as ProductId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obc_listwa') as popertyId,
null as MinValue,
null as CelValue,
null as MaxValue,
l.symbol as text

FROM [mwbase].[prdkabat].[biezniki] as i
	  INNER JOIN [mwbase].[prdkabat].[biezniki_wersje] as w
	  ON w.[bieznik] = i.[pozid]  and i.wersja = w.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
	  left join [mwbase].[prdkabat].[listwy] as l
	  on l.POZID = w.listwa

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  

	  where i.Typ = 0	
		
	  	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obc_listwa'))
-- -------------------------------------------------------------------------------------------------------------------

	  -- -------------------------------------------------------------------------------------------------------------------

union all

select 
tp.Id as ProductId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obc_type') as popertyId,
null as MinValue,
null as CelValue,
null as MaxValue,
bt.symbol as text

FROM [mwbase].[prdkabat].[biezniki] as i
	  INNER JOIN [mwbase].[prdkabat].[biezniki_wersje] as w
	  ON w.[bieznik] = i.[pozid]  and i.wersja = w.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
	  left join [mwbase].[prdkabat].bieznik_typ as bt
	  on bt.POZID = i.bieznik_Typ

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  

	  where i.Typ = 0	
		
	  	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obc_type'))

-- -------------------------------------------------------------------------------------------------
-- BIEŻNIK BOK [szt]
-- --------------------------------------------------------------------------------------------------

union all 
select 
tp.Id as ProductId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obb_weight') as popertyId,
w.waga_min as MinValue,
w.waga_cel as CelValue,
w.waga_max as MaxValue,
'' as text

FROM [mwbase].[prdkabat].[biezniki] as i
	  INNER JOIN [mwbase].[prdkabat].[biezniki_wersje] as w
	  ON w.[bieznik] = i.[pozid]  and i.wersja = w.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  where i.Typ = 1	
		
	  	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obb_weight'))

-- -------------------------------------------------------------------------------------------------------------------
union all

select 
tp.Id as ProductId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obb_length') as popertyId,
w.dlugosc_min as MinValue,
w.dlugosc_cel as CelValue,
w.dlugosc_max as MaxValue,
'' as text

FROM [mwbase].[prdkabat].[biezniki] as i
	  INNER JOIN [mwbase].[prdkabat].[biezniki_wersje] as w
	  ON w.[bieznik] = i.[pozid]  and i.wersja = w.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  where i.Typ = 1	
		
	  	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obb_length'))

-- -------------------------------------------------------------------------------------------------------------------
union all

select 
tp.Id as ProductId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obb_width') as popertyId,
w.szer_min as MinValue,
w.szer_cel as CelValue,
w.szer_max as MaxValue,
'' as text

FROM [mwbase].[prdkabat].[biezniki] as i
	  INNER JOIN [mwbase].[prdkabat].[biezniki_wersje] as w
	  ON w.[bieznik] = i.[pozid]  and i.wersja = w.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  where i.Typ = 1	
		
	  	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obb_width'))


-- -------------------------------------------------------------------------------------------------------------------

union all

select 
tp.Id as ProductId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obb_cross_section_area') as popertyId,
null as MinValue,
w.pole_przekroju as CelValue,
null as MaxValue,
'' as text

FROM [mwbase].[prdkabat].[biezniki] as i
	  INNER JOIN [mwbase].[prdkabat].[biezniki_wersje] as w
	  ON w.[bieznik] = i.[pozid]  and i.wersja = w.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  where i.Typ = 1	
		
	  	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obb_cross_section_area'))

	  -- -------------------------------------------------------------------------------------------------------------------

union all

select 
tp.Id as ProductId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obb_listwa') as popertyId,
null as MinValue,
null as CelValue,
null as MaxValue,
l.symbol as text

FROM [mwbase].[prdkabat].[biezniki] as i
	  INNER JOIN [mwbase].[prdkabat].[biezniki_wersje] as w
	  ON w.[bieznik] = i.[pozid]  and i.wersja = w.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
	  left join [mwbase].[prdkabat].[listwy] as l
	  on l.POZID = w.listwa

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  

	  where i.Typ = 1	
		
	  	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obb_listwa'))
-- -------------------------------------------------------------------------------------------------------------------

	  -- -------------------------------------------------------------------------------------------------------------------

union all

select 
tp.Id as ProductId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obb_type') as popertyId,
null as MinValue,
null as CelValue,
null as MaxValue,
bt.symbol as text

FROM [mwbase].[prdkabat].[biezniki] as i
	  INNER JOIN [mwbase].[prdkabat].[biezniki_wersje] as w
	  ON w.[bieznik] = i.[pozid]  and i.wersja = w.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
	  left join [mwbase].[prdkabat].bieznik_typ as bt
	  on bt.POZID = i.bieznik_Typ

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  

	  where i.Typ = 1	
		
	  	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'obb_type'))

-- -------------------------------------------------------------------------------------------------
-- KAPA [szt]
-- -------------------------------------------------------------------------------------------------

union all
select 
tp.Id as ProductId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oka_height') as popertyId,
w.gruboscMin as MinValue,
w.gruboscCel as CelValue,
w.gruboscMax as MaxValue,
'' as text

FROM [mwbase].[prdkabat].[kapy] as i
	  INNER JOIN [mwbase].[prdkabat].[kapy_wersje] as w
	  ON w.[kapa] = i.[pozid]  and i.wersja = w.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  where 1 = 1
		
	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oka_height'))

-- -------------------------------------------------------------------------------------------------------------------

union all
select 
tp.Id as ProductId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oka_width') as popertyId,
w.szerokoscMin as MinValue,
w.szerokoscCel as CelValue,
w.szerokoscMax as MaxValue,
'' as text

FROM [mwbase].[prdkabat].[kapy] as i
	  INNER JOIN [mwbase].[prdkabat].[kapy_wersje] as w
	  ON w.[kapa] = i.[pozid]  and i.wersja = w.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  where 1 = 1
		
	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oka_width'))

-- -------------------------------------------------------------------------------------------------------------------

-- -------------------------------------------------------------------------------------------------
-- APEX [szt]
-- -------------------------------------------------------------------------------------------------

union all
select 
tp.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oap_dim_a ') as popertyId,
null as MinValue,  
i.wymiarA as CelValue,
null as MaxValue,
'' as text

FROM [mwbase].[prdkabat].[apexy] as i
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  
	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oap_dim_a'))
-- -------------------------------------------------------------------------------------------------

union all
select 
tp.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oap_dim_b ') as popertyId,
null as MinValue,  
i.wymiarB as CelValue,
null as MaxValue,
'' as text

FROM [mwbase].[prdkabat].[apexy] as i
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  
	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oap_dim_b'))


-- -------------------------------------------------------------------------------------------------
union all
select 
tp.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oap_dim_c ') as popertyId,
null as MinValue,  
i.wymiarC as CelValue,
null as MaxValue,
'' as text

FROM [mwbase].[prdkabat].[apexy] as i
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  
	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'oap_dim_c'))
----

-- -------------------------------------------------------------------------------------------------
-- KAPA [szt]
-- -------------------------------------------------------------------------------------------------

union all
select 
tp.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'odr_number_of_wires') as popertyId,
null as MinValue,
w.iloscDrutow as CelValue,
null as MaxValue,
'' as text

FROM [mwbase].[prdkabat].[drutowki] as i
	  INNER JOIN [mwbase].[prdkabat].[drutowki_wersje] as w
	  ON w.[drutowka] = i.[pozid]  and i.wersja = w.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  
	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'odr_number_of_wires'))

-- -------------------------------------------------------------------------------------------------


union all
select 
tp.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'odr_number_of_wire_coils') as popertyId,
null as MinValue,
w.iloscZwojow as CelValue,
null as MaxValue,
'' as text

FROM [mwbase].[prdkabat].[drutowki] as i
	  INNER JOIN [mwbase].[prdkabat].[drutowki_wersje] as w
	  ON w.[drutowka] = i.[pozid]  and i.wersja = w.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  
	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'odr_number_of_wire_coils'))

 -- -------------------------------------------------------------------------------------------------
	  
union all
select 
tp.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'odr_circ') as popertyId,
null as MinValue,
w.obwodCel as CelValue,
null as MaxValue,
'' as text

FROM [mwbase].[prdkabat].[drutowki] as i
	  INNER JOIN [mwbase].[prdkabat].[drutowki_wersje] as w
	  ON w.[drutowka] = i.[pozid]  and i.wersja = w.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  
	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'odr_circ'))

	   -- -------------------------------------------------------------------------------------------------
	  
union all
select 
tp.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'odr_over_lap') as popertyId,
w.obwodMin as MinValue,
w.obwodCel as CelValue,
w.obwodMax as MaxValue,
'' as text

FROM [mwbase].[prdkabat].[drutowki] as i
	  INNER JOIN [mwbase].[prdkabat].[drutowki_wersje] as w
	  ON w.[drutowka] = i.[pozid]  and i.wersja = w.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  
	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'odr_over_lap'))

-- -------------------------------------------------------------------------------------------------------------------

union all
select 
tp.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'osu_closed_drum_circumference') as popertyId,
null as MinValue,
w.obwodBebnaKonf as CelValue,
null as MaxValue,
'' as text

FROM [mwbase].[prdkabat].[opony] as i
	  INNER JOIN [mwbase].[prdkabat].[opony_wersje] as w
	  ON w.[opona] = i.[pozid]   and i.wersja = w.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  where 1 = 1
		
	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'osu_closed_drum_circumference'))

-- -------------------------------------------------------------------------------------------------------------------

union all
select 
tp.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'osu_opened_drum_circumference') as popertyId,
null as MinValue,
w.obwodBebnaKonf2 as CelValue,
null as MaxValue,
'' as text

FROM [mwbase].[prdkabat].[opony] as i
	  INNER JOIN [mwbase].[prdkabat].[opony_wersje] as w
	  ON w.[opona] = i.[pozid]   and i.wersja = w.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  where 1 = 1
		
	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'osu_opened_drum_circumference'))


-- -------------------------------------------------------------------------------------------------------------------

union all
select 
tp.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'osu_drum_width') as popertyId,
null as MinValue,
w.szerokoscBebnaKonf as CelValue,
null as MaxValue,
'' as text

FROM [mwbase].[prdkabat].[opony] as i
	  INNER JOIN [mwbase].[prdkabat].[opony_wersje] as w
	  ON w.[opona] = i.[pozid]   and i.wersja = w.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  where 1 = 1
		
	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'osu_drum_width'))

-- -------------------------------------------------------------------------------------------------------------------

union all
select 
tp.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'osu_diameter_in_inch') as popertyId,
null as MinValue,
i.cal as CelValue,
null as MaxValue,
'' as text

FROM [mwbase].[prdkabat].[opony] as i
	  INNER JOIN [mwbase].[prdkabat].[opony_wersje] as w
	  ON w.[opona] = i.[pozid]   and i.wersja = w.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  where 1 = 1
		
	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'osu_diameter_in_inch'))

-- -------------------------------------------------------------------------------------------------------------------

union all
select 
tp.Id as ProductVersionId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'osu_nosnosc_pr') as popertyId,
null as MinValue,
i.[nosnoscPr] as CelValue,
null as MaxValue,
'' as text

FROM [mwbase].[prdkabat].[opony] as i
	  INNER JOIN [mwbase].[prdkabat].[opony_wersje] as w
	  ON w.[opona] = i.[pozid]   and i.wersja = w.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  where 1 = 1
		
	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'osu_nosnosc_pr'))

-- -------------------------------------------------------------------------------------------------
-- koniec
  )





