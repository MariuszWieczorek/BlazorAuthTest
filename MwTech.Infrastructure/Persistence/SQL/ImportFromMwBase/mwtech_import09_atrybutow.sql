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




delete from [MwTech].[dbo].[ProductProperties]
DBCC CHECKIDENT('[MwTech].[dbo].[ProductProperties]', RESEED, 1)



insert into [MwTech].[dbo].[ProductProperties]
  (
	   [ProductId]
      ,[PropertyId]
      ,[Value]
      ,[Text]
  )
  (

select 
tp.Id as ProductId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'type_approval') as popertyId,
0  as value,
i.numerHomologacji as text

FROM [mwbase].[prdkabat].[opony_wulk] as i
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  where 1 = 1
		
	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  inner join [MwTech].[dbo].[Products] as p
	  on p.id = bb.ProductId
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'type_approval'))

-- -------------------------------------------------------------------------------------------------------------------

union all
select 
tp.Id as ProductId,
(select id from [MwTech].[dbo].[Properties] where propertyNumber = 'owu_tkw') as popertyId,
i.tkwid  as value,
'' as text

FROM [mwbase].[prdkabat].[opony_wulk] as i
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid

  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  where 1 = 1
		
	  and not exists
	  (select * from [MwTech].[dbo].[ProductProperties] as bb 
	  inner join [MwTech].[dbo].[Products] as p
	  on p.id = bb.ProductId
	  where bb.ProductId = tp.id
	  and bb.PropertyId = (select id from [MwTech].[dbo].[Properties] where propertyNumber = 'owu_tkw'))

-- -------------------------------------------------------------------------------------------------------------------


-- -------------------------------------------------------------------------------------------------------------------
-- koniec
  )





