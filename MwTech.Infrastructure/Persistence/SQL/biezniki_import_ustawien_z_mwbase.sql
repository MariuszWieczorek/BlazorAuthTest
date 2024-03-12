SELECT 
 37 as ProductSettingVersionId
,(select id from MwTech.dbo.Settings as x where x.SettingNumber = s.symbolParam) as SettingId
,u.wartoscMin as MinValue
,u.wartoscCel as Value
,u.wartoscMax as MaxValue
,u.tekst as Text
,u.opis as Description   
,s.symbolParam
FROM [mwbase].[prdkabat].[typ_maszyny_wyrob_ustawienia_pozycje] as u
inner join [prdkabat].typ_maszyny_wyrob_ustawienia_naglowki as n
on n.pozid = u.naglowekId
inner join [prdkabat].[maszyny_szablony_parametrow_pozycje] as s
on s.pozId = u.parametrId
where s.szablonId = 2
and naglowekId = 281

/*
insert into MwTech.dbo.ProductSettingVersionPositions 
(
       [ProductSettingVersionId]
      ,[SettingId]
      ,[MinValue]
	  ,[Value]
      ,[MaxValue]
	  ,[Text]
	  ,[Description]
	  ,IsActive
)
(
SELECT 
 37 as ProductSettingVersionId
,(select id from MwTech.dbo.Settings as x where x.SettingNumber = s.symbolParam) as SettingId
,u.wartoscMin as MinValue
,u.wartoscCel as Value
,u.wartoscMax as MaxValue
,u.tekst as Text
,u.opis as Description   
,1 as IsActive
FROM [mwbase].[prdkabat].[typ_maszyny_wyrob_ustawienia_pozycje] as u
inner join [prdkabat].typ_maszyny_wyrob_ustawienia_naglowki as n
on n.pozid = u.naglowekId
inner join [prdkabat].[maszyny_szablony_parametrow_pozycje] as s
on s.pozId = u.parametrId
where s.szablonId = 2
and naglowekId = 281
)

*/


