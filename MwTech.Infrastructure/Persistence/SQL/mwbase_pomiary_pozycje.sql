use mwtech;


delete from dbo.MeasurementPositions
DBCC CHECKIDENT ('dbo.MeasurementPositions', RESEED, 0)

insert into dbo.MeasurementPositions
(
  [MeasurementHeaderId]
 ,[Value]
 )
SELECT CAST(ROW_NUMBER() over(order by x.PozId) as numeric(10) ) as HeaderId
, x.Value
FROM (
SELECT 
       (select pr.id from dbo.Products as pr where pr.ProductNumber = m.INDEKS) as ProductId
	  ,m.INDEKS as ProductNumber 
      ,w.wartosc as Value
      ,w.zmiana
      ,[czasZapisu]
	  ,(select uu.id from dbo.AspNetUsers as uu where trim(uu.ReferenceNumber) = 'P'+trim(u.barCode)) as UserId
	  ,w.pozId
  FROM [mwbase].[prdkabat].[pomiary_opony_wulk_waga] as w
  inner join [mwbase].wsp.users as u
  on u.pozid = w.UserZapis
  left join [mwbase].gm.materialy as m
  ON m.matid = w.matid
  ) as x
  where x.ProductId is not null
  -- and x.pozId > 23483
  order by x.pozId
