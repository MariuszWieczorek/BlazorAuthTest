/****** Script for SelectTopNRows command from SSMS  ******/
use mwbase;

SELECT 
	p.pozId,
	sp.symbolParam,
	o.indeks,
    p.parametrId,
    p.wartosc,
    n.nazwaRecepty,
    n.wcmid,
    n.czyZatw1,
    n.czyZatw2,
    n.czyZatw3
  FROM [mwbase].[prdkabat].[wyrob_czynnosc_maszyna_recepty_pozycje] as p
  inner join [mwbase].[prdkabat].[wyrob_czynnosc_maszyna_recepty_naglowki] as n
  on n.pozId = p.naglowekId
  inner join [mwbase].[prdkabat].[wyrob_czynnosc_maszyna] as wcm
  on wcm.pozId = n.wcmId
  inner join [mwbase].[prdkabat].[opony] as o
  on o.pozid = wcm.wyrobId
  inner join [prdkabat].[maszyny_szablony_parametrow_pozycje] as sp
  on sp.pozId = p.parametrId
  where parametrId = 6
  and n.czyZatw1 = 1 
  and n.czyZatw2 = 1 
  and n.czyZatw3 = 1 

  UPDATE p
  set wartosc = 15
   FROM [mwbase].[prdkabat].[wyrob_czynnosc_maszyna_recepty_pozycje] as p
  inner join [mwbase].[prdkabat].[wyrob_czynnosc_maszyna_recepty_naglowki] as n
  on n.pozId = p.naglowekId
  inner join [mwbase].[prdkabat].[wyrob_czynnosc_maszyna] as wcm
  on wcm.pozId = n.wcmId
  inner join [mwbase].[prdkabat].[opony] as o
  on o.pozid = wcm.wyrobId
  inner join [prdkabat].[maszyny_szablony_parametrow_pozycje] as sp
  on sp.pozId = p.parametrId
  where parametrId = 6
  and n.czyZatw1 = 1 
  and n.czyZatw2 = 1 
  and n.czyZatw3 = 1 
  
  SELECT 
	p.pozId,
	sp.symbolParam,
	o.indeks,
    p.parametrId,
    p.wartosc,
    n.nazwaRecepty,
    n.wcmid,
    n.czyZatw1,
    n.czyZatw2,
    n.czyZatw3
  FROM [mwbase].[prdkabat].[wyrob_czynnosc_maszyna_recepty_pozycje] as p
  inner join [mwbase].[prdkabat].[wyrob_czynnosc_maszyna_recepty_naglowki] as n
  on n.pozId = p.naglowekId
  inner join [mwbase].[prdkabat].[wyrob_czynnosc_maszyna] as wcm
  on wcm.pozId = n.wcmId
  inner join [mwbase].[prdkabat].[opony] as o
  on o.pozid = wcm.wyrobId
  inner join [prdkabat].[maszyny_szablony_parametrow_pozycje] as sp
  on sp.pozId = p.parametrId
  where parametrId = 8
  and n.czyZatw1 = 1 
  and n.czyZatw2 = 1 
  and n.czyZatw3 = 1 

  UPDATE p
  set wartosc = 20
   FROM [mwbase].[prdkabat].[wyrob_czynnosc_maszyna_recepty_pozycje] as p
  inner join [mwbase].[prdkabat].[wyrob_czynnosc_maszyna_recepty_naglowki] as n
  on n.pozId = p.naglowekId
  inner join [mwbase].[prdkabat].[wyrob_czynnosc_maszyna] as wcm
  on wcm.pozId = n.wcmId
  inner join [mwbase].[prdkabat].[opony] as o
  on o.pozid = wcm.wyrobId
  inner join [prdkabat].[maszyny_szablony_parametrow_pozycje] as sp
  on sp.pozId = p.parametrId
  where parametrId = 8
  and n.czyZatw1 = 1 
  and n.czyZatw2 = 1 
  and n.czyZatw3 = 1 
