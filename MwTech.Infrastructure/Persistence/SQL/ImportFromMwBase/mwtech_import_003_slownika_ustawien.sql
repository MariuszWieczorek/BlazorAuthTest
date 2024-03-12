/****** Script for SelectTopNRows command from SSMS  ******/
-- Import szablonów ustawień
-- Rezygnujemy z wielu szablonów dla jednej kategorii maszyn
-- Kategoria maszyny jest sama w sobie szablonem
-- JM dociągniemy później

delete from [MwTech].[dbo].[Settings]
DBCC CHECKIDENT('[MwTech].[dbo].[Settings]', RESEED, 1)



  insert into [MwTech].[dbo].[Settings]
  (
       [SettingNumber]
      ,[Name]
      ,[OrdinalNumber]
      ,[Description]
      ,[SettingCategoryId]
      ,[Text]
      ,[MinValue]
      ,[Value]
      ,[MaxValue]
      ,[IsEditable]
      ,[IsActive]
      ,[IsNumeric]
      ,[AlwaysOnPrintout]
      ,[HideOnPrintout]
  )
  (
 -- wszystkie parametry
select 
p.[symbolParam],
p.[nazwaParam],
p.[lp],
p.[opis],
(select id from [MwTech].[dbo].[SettingCategories] as x where x.[SettingCategoryNumber] = g.symbol),
'',
p.[wartoscMin],
p.[wartoscCel],
p.[wartoscMax],
p.[edycja],
p.[active],
1,
p.[zawszeNaWydruku],
p.[ukryjNaWydruku]
from [mwbase].[prdkabat].[maszyny_szablony_parametrow_pozycje] as p
inner join [mwbase].[prdkabat].[maszyny_grupy_parametrow] as g
on g.pozid = p.idGrupy
where 1 = 1
and not exists(select * from [MwTech].[dbo].[Settings] as x where SettingNumber = p.[symbolParam])




)
 



