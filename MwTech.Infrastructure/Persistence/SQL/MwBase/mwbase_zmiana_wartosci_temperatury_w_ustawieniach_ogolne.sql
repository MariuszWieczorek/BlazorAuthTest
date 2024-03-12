/****** Script for SelectTopNRows command from SSMS  ******/
SELECT 
	p.pozId,
	sp.symbolParam,
	o.indeks,
    p.parametrId,
    p.wartoscCel,
	p.wartoscMin,
	p.wartoscMax,
    n.wersja,
    n.czyZatw1,
    n.czyZatw2
  FROM [prdkabat].[typ_maszyny_wyrob_ustawienia_pozycje] as p
  inner join [prdkabat].[typ_maszyny_wyrob_ustawienia_naglowki] as n
  on n.pozId = p.naglowekId
  inner join [mwbase].[prdkabat].[opony] as o
  on o.pozid = n.wyrobId
  inner join [prdkabat].[maszyny_szablony_parametrow_pozycje] as sp
  on sp.pozId = p.parametrId
  where parametrId = 8
  and n.czyZatw1 = 1 
  and n.czyZatw2 = 1 
  
  UPDATE p
  set wartoscCel = 20
    FROM [prdkabat].[typ_maszyny_wyrob_ustawienia_pozycje] as p
  inner join [prdkabat].[typ_maszyny_wyrob_ustawienia_naglowki] as n
  on n.pozId = p.naglowekId
  inner join [mwbase].[prdkabat].[opony] as o
  on o.pozid = n.wyrobId
  inner join [prdkabat].[maszyny_szablony_parametrow_pozycje] as sp
  on sp.pozId = p.parametrId
  where parametrId = 8
  and n.czyZatw1 = 1 
  and n.czyZatw2 = 1 

  