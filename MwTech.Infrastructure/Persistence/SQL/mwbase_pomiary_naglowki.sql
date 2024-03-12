use mwtech;

/*
delete from dbo.MeasurementPositions
delete from dbo.MeasurementHeaders 
DBCC CHECKIDENT ('dbo.MeasurementPositions', RESEED, 0)
DBCC CHECKIDENT ('dbo.MeasurementHeaders', RESEED, 0)
*/


insert into dbo.MeasurementHeaders
(
  ProductId
 ,[Shift]
 ,[CreatedDate]
 ,[CreatedByUserId]
 )
SELECT x.ProductId, x.zmiana, x.czasZapisu, x.UserId
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
