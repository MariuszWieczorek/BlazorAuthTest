/* (01) import produktów z MWBase */
/* mwtech_import01_produktow */


delete from [MwTech].[dbo].[ManufactoringRoutes]
DBCC CHECKIDENT('[MwTech].[dbo].[ManufactoringRoutes]', RESEED, 1)

delete from [MwTech].[dbo].[Boms]
DBCC CHECKIDENT('[MwTech].[dbo].[Boms]', RESEED, 1)

delete from [MwTech].[dbo].[ProductVersions]
DBCC CHECKIDENT('[MwTech].[dbo].[ProductVersions]', RESEED, 1)


delete from [MwTech].[dbo].[ProductSettingVersionPositions]
DBCC CHECKIDENT('[MwTech].[dbo].[ProductSettingVersionPositions]', RESEED, 1)

delete from [MwTech].[dbo].[ProductSettingVersions]
DBCC CHECKIDENT('[MwTech].[dbo].[ProductSettingVersions]', RESEED, 1)

delete from [MwTech].[dbo].[ProductCosts]
DBCC CHECKIDENT('[MwTech].[dbo].[ProductCosts]', RESEED, 1)

delete from [MwTech].[dbo].[Products]
DBCC CHECKIDENT('[MwTech].[dbo].[Products]', RESEED, 1)


DECLARE @jm_kg int = (select id from  [MwTech].[dbo].[Units] as j  where j.name = 'kg')
DECLARE @jm_szt int = (select id from  [MwTech].[dbo].[Units] as j  where j.name = 'szt')


  insert into [MwTech].[dbo].[Products]
  (
  ProductNumber,
  Name,
  MwbaseMatid,
  UnitId,
  ProductCategoryId,
  CreatedDate,
  CreatedByUserId
  )
  (
  -- bieżniki czoło 4 -> 8
select 
m.INDEKS,
m.nazwa,
m.matid,
@jm_szt as UnitId,
(SELECT id FROM [MwTech].[dbo].[ProductCategories] as g WHERE Name = 'Bieżnik Czoło') as ProductCategoryId,
getdate() as createdDate,
'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
from mwbase.gm.materialy as m
where grupa3 = 4
and not exists(select * from [MwTech].[dbo].[Products] as x where mwbasematid = m.matid)

-- bieżniki bok 5 -> 7
union all
select 
m.INDEKS,
m.nazwa,
m.matid,
@jm_szt as UnitId,
(SELECT id FROM [MwTech].[dbo].[ProductCategories] as g WHERE Name = 'Bieżnik Bok') as ProductCategoryId,
getdate() as createdDate,
'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
from mwbase.gm.materialy as m
where grupa3 = 5
and not exists(select * from [MwTech].[dbo].[Products] as x where mwbasematid = m.matid)

-- kapa opony 6 -> 9
union all
select 
m.INDEKS,
m.nazwa,
m.matid,
@jm_kg as UnitId,
(SELECT id FROM [MwTech].[dbo].[ProductCategories] as g WHERE Name = 'Kapa Opony') as ProductCategoryId,
getdate() as createdDate,
'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
from mwbase.gm.materialy as m
where grupa3 = 6
and not exists(select * from [MwTech].[dbo].[Products] as x where mwbasematid = m.matid)

-- drutówka 7 -> 10
union all
select 
m.INDEKS,
m.nazwa,
m.matid,
@jm_szt as UnitId,
(SELECT id FROM [MwTech].[dbo].[ProductCategories] as g WHERE Name = 'Drutówka') as ProductCategoryId,
getdate() as createdDate,
'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
from mwbase.gm.materialy as m
where grupa3 = 7
and not exists(select * from [MwTech].[dbo].[Products] as x where mwbasematid = m.matid)

-- opona wulk 1 -> 11
union all
select 
m.INDEKS,
m.nazwa,
m.matid,
@jm_szt as UnitId,
(SELECT id FROM [MwTech].[dbo].[ProductCategories] as g WHERE Name = 'Opona Wulkanizowana') as ProductCategoryId,
getdate() as createdDate,
'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
from mwbase.gm.materialy as m
where grupa3 = 1
and not exists(select * from [MwTech].[dbo].[Products] as x where mwbasematid = m.matid)

-- opona sur 3 -> 12
union all
select 
m.INDEKS,
m.nazwa,
m.matid,
@jm_szt as UnitId,
(SELECT id FROM [MwTech].[dbo].[ProductCategories] as g WHERE Name = 'Opona Surowa') as ProductCategoryId,
getdate() as createdDate,
'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
from mwbase.gm.materialy as m
where grupa3 = 3
and not exists(select * from [MwTech].[dbo].[Products] as x where mwbasematid = m.matid)

-- apex 8 -> 13
union all
select 
m.INDEKS,
m.nazwa,
m.matid,
@jm_kg as UnitId,
(SELECT id FROM [MwTech].[dbo].[ProductCategories] as g WHERE Name = 'Apex') as ProductCategoryId,
getdate() as createdDate,
'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
from mwbase.gm.materialy as m
where grupa3 = 8
and not exists(select * from [MwTech].[dbo].[Products] as x where mwbasematid = m.matid)

-- apex + drutówka 9 -> 14
union all
select 
m.INDEKS,
m.nazwa,
m.matid,
@jm_szt as UnitId,
(SELECT id FROM [MwTech].[dbo].[ProductCategories] as g WHERE Name = 'Drutówka z Apexem') as ProductCategoryId,
getdate() as createdDate,
'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
from mwbase.gm.materialy as m
where grupa3 = 9
and not exists(select * from [MwTech].[dbo].[Products] as x where mwbasematid = m.matid)

-- kord cięty 10 -> 15
union all
select 
m.INDEKS,
m.nazwa,
m.matid,
@jm_kg as UnitId,
(SELECT id FROM [MwTech].[dbo].[ProductCategories] as g WHERE Name = 'Kord Cięty') as ProductCategoryId,
getdate() as createdDate,
'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
from mwbase.gm.materialy as m
where grupa3 = 10
and not exists(select * from [MwTech].[dbo].[Products] as x where mwbasematid = m.matid)

-- kord gumowany 11 -> 16
union all
select 
m.INDEKS,
m.nazwa,
m.matid,
@jm_kg as UnitId,
(SELECT id FROM [MwTech].[dbo].[ProductCategories] as g WHERE Name = 'Kord Gumowany') as ProductCategoryId,
getdate() as createdDate,
'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
from mwbase.gm.materialy as m
where grupa3 = 11
and not exists(select * from [MwTech].[dbo].[Products] as x where mwbasematid = m.matid)

-- kord surowy 12 -> 17
union all
select 
m.INDEKS,
m.nazwa,
m.matid,
@jm_kg as UnitId,
(SELECT id FROM [MwTech].[dbo].[ProductCategories] as g WHERE Name = 'Kord Surowy') as ProductCategoryId,
getdate() as createdDate,
'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
from mwbase.gm.materialy as m
where grupa3 = 12
and not exists(select * from [MwTech].[dbo].[Products] as x where mwbasematid = m.matid)


-- mieszanka 14 -> 2
union all
select 
m.INDEKS,
m.nazwa,
m.matid,
@jm_kg as UnitId,
(SELECT id FROM [MwTech].[dbo].[ProductCategories] as g WHERE Name = 'Mieszanka') as ProductCategoryId,
getdate() as createdDate,
'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
from mwbase.gm.materialy as m
where grupa3 = 14
and not exists(select * from [MwTech].[dbo].[Products] as x where mwbasematid = m.matid)

-- drut 15 -> 18
union all
select 
m.INDEKS,
m.nazwa,
m.matid,
@jm_kg as UnitId,
(SELECT id FROM [MwTech].[dbo].[ProductCategories] as g WHERE Name = 'Drut') as ProductCategoryId,
getdate() as createdDate,
'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
from mwbase.gm.materialy as m
where grupa3 = 15
and not exists(select * from [MwTech].[dbo].[Products] as x where mwbasematid = m.matid)

-- preparaty 21 -> 19
union all
select 
m.INDEKS,
m.nazwa,
m.matid,
@jm_kg as UnitId,
(SELECT id FROM [MwTech].[dbo].[ProductCategories] as g WHERE Name = 'Preparaty') as ProductCategoryId,
getdate() as createdDate,
'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
from mwbase.gm.materialy as m
where grupa3 = 21
and not exists(select * from [MwTech].[dbo].[Products] as x where mwbasematid = m.matid)

-- tkanina ochronna 22 -> 20
union all
select 
m.INDEKS,
m.nazwa,
m.matid,
@jm_kg as UnitId,
(SELECT id FROM [MwTech].[dbo].[ProductCategories] as g WHERE Name = 'Tkanina Ochronna') as ProductCategoryId,
getdate() as createdDate,
'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
from mwbase.gm.materialy as m
where grupa3 = 22
and not exists(select * from [MwTech].[dbo].[Products] as x where mwbasematid = m.matid)
 )
 

--  update [MwTech].[dbo].[Products] set UnitId = 3 where ProductCategoryId = 9
--  update [MwTech].[dbo].[Products] set UnitId = 3 where ProductCategoryId = 13
--  update [MwTech].[dbo].[Products] set UnitId = 3 where ProductCategoryId = 15
--  update [MwTech].[dbo].[Products] set UnitId = 3 where ProductCategoryId = 16
--  update [MwTech].[dbo].[Products] set UnitId = 3 where ProductCategoryId = 17
--  update [MwTech].[dbo].[Products] set UnitId = 3 where ProductCategoryId = 18
--  update [MwTech].[dbo].[Products] set UnitId = 3 where ProductCategoryId = 19
--  update [MwTech].[dbo].[Products] set UnitId = 3 where ProductCategoryId = 20


