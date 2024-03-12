-- Import Wersji Ustawień Maszyn dla Produktów z MwBase
-- Nagłówki i Pozycje
-- Importujemy nagłówki, jeżeli jeszcze ich nie ma w MwTech
-- Rozpoznajemy po MwBaseId
-- Podobnie Importujemy Pozycje
-- jeżeli jeszcze ich nie ma w MwTech
-- Również rozpoznajemy po MwBaseId
-- Nie importujemy pozycji, dla których nie powiódł się import nagłówków

Update z
SET z.AlternativeNo = x.rownr
FROM [MwTech].[dbo].[ProductSettingVersions] as z
inner join 
(
select h.ProductId, h.WorkCenterId
,CAST(ROW_NUMBER() over(PARTITION BY h.ProductId order by h.ProductId, h.WorkCenterId) as numeric(10) ) as rownr
FROM [MwTech].[dbo].[ProductSettingVersions] as h
group by h.ProductId, h.WorkCenterId
)
as x
on x.ProductId = z.ProductId and x.WorkCenterId = z.WorkCenterId


-- nagłówki Prasa Wulkanizacyjna -----------------------------------------------------------------------------------------------------

  insert into [MwTech].[dbo].[ProductSettingVersions]
  (
	   AlternativeNo
      ,ProductSettingVersionNumber
      ,Name
	  ,MwBaseName
	  ,DefaultVersion

      ,[ProductId]
      ,[MachineCategoryId]
      ,[MachineId]
	  ,[WorkCenterId]
      ,[Description]

      ,[IsAccepted01]
      ,[Accepted01ByUserId]
      ,[Accepted01Date]

      ,[IsAccepted02]
      ,[Accepted02ByUserId]
      ,[Accepted02Date]

      ,[IsAccepted03]
      ,[Accepted03ByUserId]
      ,[Accepted03Date]


      ,[MwbaseId]
  )
  (

-- 



SELECT 
1 as wariant,
x.wersjaRecepty,
x.nazwaRecepty,
x.nazwaRecepty,
1 as DefaultV,
--

( select id from mwtech.dbo.Products as p where p.ProductNumber = x.indeksHandlowy) as ProductId,
( select id from mwtech.dbo.MachineCategories as m where m.MachineCategoryNumber = 'PRASA_OPONY') as MachineCategoryId,
( select id from mwtech.dbo.Machines as m where trim(m.MachineNumber) = trim(x.symbol2)) as MachineId,
( select id from mwtech.dbo.Resources as m where trim(m.ResourceNumber) = trim(x.symbol2)) as WorkCenterId,
x.opis as description,

--
x.czyZatw1,
case when x.userZatw1 = 1  then '7f2bacf6-564a-4272-a4bb-f76832476024'
     when x.userZatw1 = 3  then '7f194520-56b2-4684-b304-4fc98884c35b'
	 when x.userZatw1 = 4  then '1e136dc3-afb5-40b7-90e8-e0826ca2b0dd'
	 when x.userZatw1 = 9  then '0f7c375a-7990-4df6-bdd9-80e2148b8b83'
	 when x.userZatw1 = 10 then '9c3f2472-379b-48ad-b690-b8afaedd4262'
	 when x.userZatw1 = 16 then 'bc51a27e-1fa6-44b6-b490-5547af06fd93'
	 when x.userZatw1 = 52 then 'ac35510e-27fd-45bf-b2c5-3c807883a8cf'
     end as userZatw1,
x.czasZatw1,
--     
x.czyZatw2,
case when x.userZatw2 = 1  then '7f2bacf6-564a-4272-a4bb-f76832476024'
     when x.userZatw2 = 3  then '7f194520-56b2-4684-b304-4fc98884c35b'
	 when x.userZatw2 = 4  then '1e136dc3-afb5-40b7-90e8-e0826ca2b0dd'
	 when x.userZatw2 = 9  then '0f7c375a-7990-4df6-bdd9-80e2148b8b83'
	 when x.userZatw2 = 10 then '9c3f2472-379b-48ad-b690-b8afaedd4262'
	 when x.userZatw2 = 16 then 'bc51a27e-1fa6-44b6-b490-5547af06fd93'
	 when x.userZatw2 = 52 then 'ac35510e-27fd-45bf-b2c5-3c807883a8cf'
     end as userZatw2,
x.czasZatw2,
--

x.czyZatw3,
case when x.userZatw3 = 1  then '7f2bacf6-564a-4272-a4bb-f76832476024'
     when x.userZatw3 = 3  then '7f194520-56b2-4684-b304-4fc98884c35b'
	 when x.userZatw3 = 4  then '1e136dc3-afb5-40b7-90e8-e0826ca2b0dd'
	 when x.userZatw3 = 9  then '0f7c375a-7990-4df6-bdd9-80e2148b8b83'
	 when x.userZatw3 = 10 then '9c3f2472-379b-48ad-b690-b8afaedd4262'
	 when x.userZatw3 = 16 then 'bc51a27e-1fa6-44b6-b490-5547af06fd93'
	 when x.userZatw3 = 52 then 'ac35510e-27fd-45bf-b2c5-3c807883a8cf'
     end as userZatw3,
x.czasZatw3,

-- x.indeks,
-- x.tkwid,
-- x.indeksHandlowy,
-- x.symbol2 as Machine,
-- ( select TechCardNumber from mwtech.dbo.Products as p where p.ProductNumber = x.indeksHandlowy) as TechCardNumber,
x.pozid as mwbaseid




FROM

(SELECT m.nazwa,m.symbol2,m.typMaszyny 
	  ,o.indeksHandlowy
	  ,o.indeks
	  ,o.tkwId
	  ,wcm.matid
	  ,wcm.wyrobId
	  ,sn.[pozId]
      ,sn.[receptaNadId]
      ,sn.[wcmId]
      ,sn.[domyslna]
      ,sn.[wersjaRecepty]
--    ,sn.[nazwaRecepty]

	  ,CAST('' 
			+ CAST(wcm.wyrobId as varchar(10))
			+ '.'
			+ CAST(mnn.wersja as varchar(10))
			+ '.'
			+ CAST(sn.wersjaRecepty as varchar(10))
			+ '-'
			+ TRIM(m.symbol2) as varchar(50)
			) as nazwaRecepty 

      ,sn.[opis]

      ,sn.[active]
      ,sn.[czyZatw1]
      ,sn.[czasZatw1]
      ,sn.[userZatw1]
      ,sn.[czyZatw2]
      ,sn.[czasZatw2]
      ,sn.[userZatw2]
      ,sn.[userZapis]
      ,sn.[czasZapisu]
      ,sn.[czyZatw3]
      ,sn.[czasZatw3]
      ,sn.[userZatw3]
  FROM [mwbase].[prdkabat].[wyrob_czynnosc_maszyna_recepty_naglowki] as sn
  inner join [mwbase].[prdkabat].[wyrob_czynnosc_maszyna] as wcm
  on wcm.pozId = sn.wcmId
  inner join [mwbase].[prdkabat].[maszyny] as m
  on m.pozId = wcm.maszynaId
  inner join [mwbase].[prdkabat].[opony] as o
  on o.pozid = wcm.wyrobId
  inner join [mwbase].[prdkabat].[typ_maszyny_wyrob_ustawienia_naglowki] as mnn
  on mnn.pozId = sn.receptaNadId
  ) as x
  
  WHERE ( select id from mwtech.dbo.Products as p where p.ProductNumber = x.indeksHandlowy) is not null 
  AND not exists
  (select * from [MwTech].[dbo].[ProductSettingVersions] as xw 
  where xw.mwbaseid = x.pozid)
)  




  
  -- )
--  order by x.indeksHandlowy, x.symbol2, x.wersjaRecepty
-- Koniec nagłówków dla Pras
insert into [MwTech].[dbo].[ProductSettingVersionPositions]
(
       [ProductSettingVersionId]
      ,[SettingId]
      ,[Text]
      ,[Value]
      ,[Description]
      ,[IsActive]
      ,[MaxValue]
      ,[MinValue]
      ,[ModifiedByUserId]
      ,[ModifiedDate]
	  ,MwBaseId
)
(
select x.ProductSettingVersionId,
x.settingId,
x.text,
x.value, 
x.description,
1 as active,
null as MaxValue,
null as MinValue,
x.ModifiedByUserId,
x.ModifiedDate,
x.pozId
from
(
SELECT 
       ( select xw.id from [MwTech].[dbo].[ProductSettingVersions] as xw where xw.MwbaseId = rp.naglowekId) as ProductSettingVersionId
      ,(select id from [MwTech].[dbo].[Settings] where settingNumber = (select symbolParam FROM [mwbase].[prdkabat].[maszyny_szablony_parametrow_pozycje] as sz where sz.pozid = rp.parametrId)) as settingId
      ,rp.wartosc as value
      ,rp.opis as description
      ,rp.tekst as text
      ,1 as IsActive
	  ,rp.czasZapisu as ModifiedDate

	  ,case when rp.userZapis = 1  then '7f2bacf6-564a-4272-a4bb-f76832476024'
		 when rp.userZapis = 3  then '7f194520-56b2-4684-b304-4fc98884c35b'
		 when rp.userZapis = 4  then '1e136dc3-afb5-40b7-90e8-e0826ca2b0dd'
		 when rp.userZapis = 9  then '0f7c375a-7990-4df6-bdd9-80e2148b8b83'
		 when rp.userZapis = 10 then '9c3f2472-379b-48ad-b690-b8afaedd4262'
		 when rp.userZapis = 16 then 'bc51a27e-1fa6-44b6-b490-5547af06fd93'
		 when rp.userZapis = 52 then 'ac35510e-27fd-45bf-b2c5-3c807883a8cf'
		 end as ModifiedByUserId
		 ,pozid
FROM [mwbase].[prdkabat].[wyrob_czynnosc_maszyna_recepty_pozycje] AS rp
) as x
where x.ProductSettingVersionId is not null
AND not exists
  (select * from [MwTech].[dbo].[ProductSettingVersionPositions] as xw 
  where xw.mwbaseid = x.pozid)
)


-- zapewnia, że istnieje nagłówek
-- where exists ( select xw.id from [MwTech].[dbo].[ProductSettingVersions] as xw where xw.MwbaseId = rp.naglowekId)
