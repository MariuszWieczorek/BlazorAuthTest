/****** Script for SelectTopNRows command from SSMS  ******/
SELECT o.indeks
	  ,o.indeksHandlowy	
      ,m.INDEKS
	  ,m.NAZWA
      ,w.MembranaIlosc
	  ,w.membrNaIle
	  ,1/w.membrNaIle
  FROM [mwbase].[prdkabat].[opony_wulk_wersje] as w
  inner join mwbase.prdkabat.opony as o
  on o.pozid = w.oponaSurowa
  inner join gm.materialy as m
  on w.MembranaMatId = m.MATID

       select m.INDEKS
	  ,m.NAZWA
  FROM [mwbase].[prdkabat].[opony_wulk_wersje] as w
  inner join mwbase.prdkabat.opony as o
  on o.pozid = w.oponaSurowa
  inner join gm.materialy as m
  on w.MembranaMatId = m.MATID
  group by m.indeks, m.NAZWA
  order by m.INDEKS
  