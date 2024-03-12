/****** Script for SelectTopNRows command from SSMS  ******/

SELECT 
       a.indeks
      ,[symbolParam]
      ,[nazwaParam]
      ,[wartoscCel]
      ,[tekst]
	  ,b.Value
	  ,b.Text
  FROM [mwbase].[prdkabat].[view_kordy_kalander_zatwierdzone_domyslne] as a
  inner join [MwTech].[dbo].[mwtech_scada_settings_positions] as b
  on a.nazwaParam = b.SettingName and a.indeks = b.ProductNumber
  where a.wartoscCel != b.Value or b.Value is null

  
SELECT *
  FROM  [MwTech].[dbo].[mwtech_scada_settings_positions] as b
  where Value is null