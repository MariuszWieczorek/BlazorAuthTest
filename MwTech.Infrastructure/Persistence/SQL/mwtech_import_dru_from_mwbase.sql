/****** Script for SelectTopNRows command from SSMS  ******/
 SELECT x.ProductNumber,
 x.nazwa as Parameter,
 'ODR' as Category,
 'ODR01' as Machine,
 'ODR01' as WorkCenter,
 1 as Alt,
 1 as ver,
 'we' as nazwa_recepty,
 ' ' as vmin,
 x.wartosc 
 FROM(
 SELECT 
	  pr.ProductNumber,
	  'odr_IloscDrutow' as nazwa, 	
	  dw.iloscDrutow  as wartosc
	  FROM mwtech.dbo.Products as pr
	  inner join MwTech.dbo.ProductCategories as ca
	  on ca.Id = pr.ProductCategoryId
	  inner join [mwbase].[prdkabat].[drutowki] as d
	  on   d.indeks = pr.ProductNumber
	  inner join [mwbase].[prdkabat].[drutowki_wersje] as dw
	  on   dw.pozid = d.wersja
	  where ca.CategoryNumber = 'ODR'
   union all
   SELECT 
	  pr.ProductNumber,
	  'odr_IloscZwojow' as nazwa, 	
	  dw.IloscZwojow as wartosc
	  FROM mwtech.dbo.Products as pr
	  inner join MwTech.dbo.ProductCategories as ca
	  on ca.Id = pr.ProductCategoryId
	  inner join [mwbase].[prdkabat].[drutowki] as d
	  on   d.indeks = pr.ProductNumber
	  inner join [mwbase].[prdkabat].[drutowki_wersje] as dw
	  on   dw.pozid = d.wersja
	  where ca.CategoryNumber = 'ODR'
   union all
   SELECT 
	  pr.ProductNumber,
	  'odr_Zakladka' as nazwa, 	
	  dw.zakladka  as wartosc
	  FROM mwtech.dbo.Products as pr
	  inner join MwTech.dbo.ProductCategories as ca
	  on ca.Id = pr.ProductCategoryId
	  inner join [mwbase].[prdkabat].[drutowki] as d
	  on   d.indeks = pr.ProductNumber
	  inner join [mwbase].[prdkabat].[drutowki_wersje] as dw
	  on   dw.pozid = d.wersja
	  where ca.CategoryNumber = 'ODR'
   union all
   SELECT 
	  pr.ProductNumber,
	  'odr_PredkoscNawijaniaDrutu' as nazwa, 	
	  dw.predkoscNawijaniaDrutu  as wartosc
	  FROM mwtech.dbo.Products as pr
	  inner join MwTech.dbo.ProductCategories as ca
	  on ca.Id = pr.ProductCategoryId
	  inner join [mwbase].[prdkabat].[drutowki] as d
	  on   d.indeks = pr.ProductNumber
	  inner join [mwbase].[prdkabat].[drutowki_wersje] as dw
	  on   dw.pozid = d.wersja
	  where ca.CategoryNumber = 'ODR'
   union all
   SELECT 
	  pr.ProductNumber,
	  'odr_CzasRozpedzaniaDrutowki' as nazwa, 	
	  dw.czasRozpedzania  as wartosc
	  FROM mwtech.dbo.Products as pr
	  inner join MwTech.dbo.ProductCategories as ca
	  on ca.Id = pr.ProductCategoryId
	  inner join [mwbase].[prdkabat].[drutowki] as d
	  on   d.indeks = pr.ProductNumber
	  inner join [mwbase].[prdkabat].[drutowki_wersje] as dw
	  on   dw.pozid = d.wersja
	  where ca.CategoryNumber = 'ODR'
   union all
   SELECT 
	  pr.ProductNumber,
	  'odr_CzasPrzytrzymaniaRzutuDrutowki' as nazwa, 	
	  dw.czasPrzytrzymania  as wartosc
	  FROM mwtech.dbo.Products as pr
	  inner join MwTech.dbo.ProductCategories as ca
	  on ca.Id = pr.ProductCategoryId
	  inner join [mwbase].[prdkabat].[drutowki] as d
	  on   d.indeks = pr.ProductNumber
	  inner join [mwbase].[prdkabat].[drutowki_wersje] as dw
	  on   dw.pozid = d.wersja
	  where ca.CategoryNumber = 'ODR'
   union all
   SELECT 
	  pr.ProductNumber,
	  'odr_CzasPuszczaniaDrutowki' as nazwa, 	
	  dw.czasPuszczania  as wartosc
	  FROM mwtech.dbo.Products as pr
	  inner join MwTech.dbo.ProductCategories as ca
	  on ca.Id = pr.ProductCategoryId
	  inner join [mwbase].[prdkabat].[drutowki] as d
	  on   d.indeks = pr.ProductNumber
	  inner join [mwbase].[prdkabat].[drutowki_wersje] as dw
	  on   dw.pozid = d.wersja
	  where ca.CategoryNumber = 'ODR'
   union all
   SELECT 
	  pr.ProductNumber,
	  'odr_PozycjaRzutuDrutowki' as nazwa, 	
	  dw.pozycjaRzutuDrutowki  as wartosc
	  FROM mwtech.dbo.Products as pr
	  inner join MwTech.dbo.ProductCategories as ca
	  on ca.Id = pr.ProductCategoryId
	  inner join [mwbase].[prdkabat].[drutowki] as d
	  on   d.indeks = pr.ProductNumber
	  inner join [mwbase].[prdkabat].[drutowki_wersje] as dw
	  on   dw.pozid = d.wersja
	  where ca.CategoryNumber = 'ODR'
   union all
   SELECT 
	  pr.ProductNumber,
	  'odr_PozycjaZatrzymywaniaZatrzasku' as nazwa, 	
	  dw.pozycjaZatrzymywania  as wartosc
	  FROM mwtech.dbo.Products as pr
	  inner join MwTech.dbo.ProductCategories as ca
	  on ca.Id = pr.ProductCategoryId
	  inner join [mwbase].[prdkabat].[drutowki] as d
	  on   d.indeks = pr.ProductNumber
	  inner join [mwbase].[prdkabat].[drutowki_wersje] as dw
	  on   dw.pozid = d.wersja
	  where ca.CategoryNumber = 'ODR'
   union all
   SELECT 
	  pr.ProductNumber,
	  'odr_PojemnoscWieszaka' as nazwa, 	
	  dw.pojemnoscWieszaka  as wartosc
	  FROM mwtech.dbo.Products as pr
	  inner join MwTech.dbo.ProductCategories as ca
	  on ca.Id = pr.ProductCategoryId
	  inner join [mwbase].[prdkabat].[drutowki] as d
	  on   d.indeks = pr.ProductNumber
	  inner join [mwbase].[prdkabat].[drutowki_wersje] as dw
	  on   dw.pozid = d.wersja
	  where ca.CategoryNumber = 'ODR'
   union all
   SELECT 
	  pr.ProductNumber,
	  'odr_RozmiarPierscienia' as nazwa, 	
	  dw.rozmiarPierscienia  as wartosc
	  FROM mwtech.dbo.Products as pr
	  inner join MwTech.dbo.ProductCategories as ca
	  on ca.Id = pr.ProductCategoryId
	  inner join [mwbase].[prdkabat].[drutowki] as d
	  on   d.indeks = pr.ProductNumber
	  inner join [mwbase].[prdkabat].[drutowki_wersje] as dw
	  on   dw.pozid = d.wersja
	  where ca.CategoryNumber = 'ODR'
	  ) as x
--	  where x.ProductNumber = 'ODR-08-08-1245-01'
