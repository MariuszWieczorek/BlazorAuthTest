/****** Script for SelectTopNRows command from SSMS  ******/


delete from [MwTech].[dbo].[SettingCategories]
DBCC CHECKIDENT('[MwTech].[dbo].[SettingCategories]', RESEED, 1)



  insert into [MwTech].[dbo].[SettingCategories]
  (
       [SettingCategoryNumber]
      ,[Name]
      ,[OrdinalNumber]
      ,[Description]
      ,[Color]
      ,[MachineCategoryId]
  )
  (
 -- prasy
select
m.symbol,
m.nazwa,
m.lp,
m.opis,
m.color,
case 
when m.maszynaTyp = 1 then 1
when m.maszynaTyp = 2 then 4
when m.maszynaTyp = 4 then 5
when m.maszynaTyp = 5 then 3 
else 0 end as dddd 
from  [mwbase].[prdkabat].[maszyny_grupy_parametrow] as m
inner join  [mwbase].[prdkabat].[maszyna_typ] as c 
on  c.pozid = m.maszynaTyp
where 1=1
and not exists(select * from [MwTech].[dbo].[SettingCategories] as x where [SettingCategoryNumber] = m.symbol)

)
 



