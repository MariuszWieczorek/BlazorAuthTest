select x.Imie, x.Nazwisko, x.barCode, count(*)
, lower(x.imie) + '.' + lower(x.nazwisko) + '@kabat.pl' as email
, trim(x.imie) + trim(x.nazwisko)
from(
SELECT w.pozId
      ,[matId]
      ,[wartosc]
      ,[dataPomiaru]
      ,w.zmiana
      ,[czasPomiaru]
      ,[czasZapisu]
	  ,u.Email
	  ,u.Imie
	  ,u.Nazwisko
	  ,u.rfidCode
	  ,u.barCode
  FROM [mwbase].[prdkabat].[pomiary_opony_wulk_waga] as w
  inner join wsp.users as u
  on u.pozid = w.UserZapis
  ) as x
  group by x.Imie, x.Nazwisko, x.barCode
  order by x.Imie, x.Nazwisko