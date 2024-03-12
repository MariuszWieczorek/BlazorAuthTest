/* (02) Import Wersji Produktów z MwBase */
/* mwtech_import02_wersji_produktow */
/* Oprócz mieszanek - mieszanki w imporcie z Comarch */


delete from [MwTech].[dbo].[Boms]
DBCC CHECKIDENT('[MwTech].[dbo].[Boms]', RESEED, 1)
delete from [MwTech].[dbo].[ProductVersions]
DBCC CHECKIDENT('[MwTech].[dbo].[ProductVersions]', RESEED, 1)


  insert into [MwTech].[dbo].[ProductVersions]
  (
      [ProductId]
	  ,[VersionNumber]
	  ,[Name]
      ,[Description]
      ,[CreatedByUserId]
      ,[CreatedDate]
      ,[MwbaseId]
	  
	  ,[IsAccepted01]
	  ,[Accepted01ByUserId]
	  ,[Accepted01Date]
	  
	  ,[IsAccepted02]
	  ,[Accepted02ByUserId]
	  ,[Accepted02Date]
	  
	  ,[DefaultVersion]
	  ,[ProductQty]
  )
  (

-- Bieżniki
	
	SELECT (SELECT Id from [MwTech].[dbo].[Products] as p where p.[mwbasematId] = i.[matid]) as ProductId
      ,w.[numerWersji]
      ,w.[nazwaWersji]
	  ,w.[Opis]
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
      ,i.[czasZapisu]
	  ,w.[pozid]
	  
	  ,w.[czyPotw1]
	  ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted01UserId
      ,w.[czasPotw1]
      
	  ,w.[czyPotw2]
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted02UserId
      ,w.[czasPotw2]
	  
	  ,iif(i.wersja = w.pozid,1,0) as DefaultVersion
	  ,1

	  FROM [mwbase].[prdkabat].[biezniki] as i
	  INNER JOIN [mwbase].[prdkabat].[biezniki_wersje] as w
	  ON w.[bieznik] = i.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  
	  WHERE not exists
	  (select * from [MwTech].[dbo].[ProductVersions] as xw 
	  inner join [MwTech].[dbo].[Products] as xp
	  on xw.ProductId = xp.Id
	  where xw.mwbaseid = w.pozid
	  and xp.ProductCategoryId in (7,8)
	  )


	 
	 
-- opony surowe
	
	union all
	  SELECT (SELECT Id from [MwTech].[dbo].[Products] as p where p.[mwbasematId] = i.[matid]) as ProductId
      ,w.[numerWersji]
      ,w.[nazwaWersji]
	  ,w.[Opis]
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
      ,i.[czasZapisu]
	  ,w.[pozid]
	  
	  ,w.[czyPotw1]
	  ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted01UserId
      ,w.[czasPotw1]
      
	  ,w.[czyPotw2]
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted02UserId
      ,w.[czasPotw2]
	  
	  ,iif(i.wersja = w.pozid,1,0) as DefaultVersion
	  ,1

	  FROM [mwbase].[prdkabat].[opony] as i
	  INNER JOIN [mwbase].[prdkabat].[opony_wersje] as w
	  ON w.[opona] = i.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
  	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId

	  WHERE not exists
	  (select * from [MwTech].[dbo].[ProductVersions] as xw 
	  inner join [MwTech].[dbo].[Products] as xp
	  on xw.ProductId = xp.Id
	  where mwbaseid = w.pozid
	  and xp.ProductCategoryId = 12)
	

-- kapa
	union all
	  SELECT (SELECT Id from [MwTech].[dbo].[Products] as p where p.[mwbasematId] = i.[matid]) as ProductId
      ,w.[numerWersji]
      ,w.[nazwaWersji]
	  ,w.[Opis]
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
      ,i.[czasZapisu]
	  ,w.[pozid]
	  
	  ,w.[czyPotw1]
	  ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted01UserId
      ,w.[czasPotw1]
      
	  ,w.[czyPotw2]
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted02UserId
      ,w.[czasPotw2]
	  
	  ,iif(i.wersja = w.pozid,1,0) as DefaultVersion
	  ,1

	  FROM [mwbase].[prdkabat].[kapy] as i
	  INNER JOIN [mwbase].[prdkabat].[kapy_wersje] as w
	  ON w.[kapa] = i.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	
	  WHERE not exists
	  (select * from [MwTech].[dbo].[ProductVersions] as xw 
	  inner join [MwTech].[dbo].[Products] as xp
	  on xw.ProductId = xp.Id
	  where mwbaseid = w.pozid
	  and xp.ProductCategoryId = 9)



-- drutowka

	union all
	  SELECT (SELECT Id from [MwTech].[dbo].[Products] as p where p.[mwbasematId] = i.[matid]) as ProductId
      ,w.[numerWersji]
      ,w.[nazwaWersji]
	  ,w.[Opis]
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
      ,i.[czasZapisu]
	  ,w.[pozid]
	  
	  ,w.[czyPotw1]
	  ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted01UserId
      ,w.[czasPotw1]
      
	  ,w.[czyPotw2]
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted02UserId
      ,w.[czasPotw2]
	  
	  ,iif(i.wersja = w.pozid,1,0) as DefaultVersion
	  ,1

	  FROM [mwbase].[prdkabat].[drutowki] as i
	  INNER JOIN [mwbase].[prdkabat].[drutowki_wersje] as w
	  ON w.[drutowka] = i.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  
	  WHERE not exists
	  (select * from [MwTech].[dbo].[ProductVersions] as xw 
	  inner join [MwTech].[dbo].[Products] as xp
	  on xw.ProductId = xp.Id
	  where mwbaseid = w.pozid
	  and xp.ProductCategoryId = 10)
--

-- opona wulk
	
	union all
	  SELECT (SELECT Id from [MwTech].[dbo].[Products] as p where p.[mwbasematId] = i.[matid]) as ProductId
      ,w.[numerWersji]
      ,w.[nazwaWersji]
	  ,w.[Opis]
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
      ,i.[czasZapisu]
	  ,w.[pozid]
	  
	  ,w.[czyPotw1]
	  ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted01UserId
      ,w.[czasPotw1]
      
	  ,w.[czyPotw2]
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted02UserId
      ,w.[czasPotw2]
	  
	  ,iif(i.wersja = w.pozid,1,0) as DefaultVersion
	  ,1

	  FROM [mwbase].[prdkabat].[opony_wulk] as i
	  INNER JOIN [mwbase].[prdkabat].[opony_wulk_wersje] as w
	  ON w.[oponaWulk] = i.[pozid]
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  
	  WHERE not exists
	  (select * from [MwTech].[dbo].[ProductVersions] as xw 
	  inner join [MwTech].[dbo].[Products] as xp
	  on xw.ProductId = xp.Id
	  where mwbaseid = w.pozid
	  and xp.ProductCategoryId = 11)
	  
--

-- drutowka + apex

	union all
	  SELECT (SELECT Id from [MwTech].[dbo].[Products] as p where p.[mwbasematId] = i.[matid]) as ProductId
      ,1
      ,'wersja 1'
	  ,''
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
      ,i.[czasZapisu]
	  
	  ,i.[pozid]
	  
	  ,1
	  ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted01UserId
      ,getdate()
      
	  ,1
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted02UserId
      ,getdate()
	  
	  ,1 as DefaultVersion
	  ,1

	  FROM [mwbase].[prdkabat].[apexy_drutowki] as i
	  
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  
	  WHERE not exists
	  (select * from [MwTech].[dbo].[ProductVersions] as xw 
	  inner join [MwTech].[dbo].[Products] as xp
	  on xw.ProductId = xp.Id
	  where mwbaseid = i.pozid
	  and xp.ProductCategoryId = 14)
--

-- apex

	union all
	  SELECT (SELECT Id from [MwTech].[dbo].[Products] as p where p.[mwbasematId] = i.[matid]) as ProductId
      ,1
      ,'wersja 1'
	  ,''
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
      ,i.[czasZapisu]
	  
	  ,i.[pozid]
	  
	  ,1
	  ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted01UserId
      ,getdate()
      
	  ,1
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted02UserId
      ,getdate()
	  
	  ,1 as DefaultVersion
	  ,1

	  FROM [mwbase].[prdkabat].[apexy] as i
	  
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  
	  WHERE not exists
	  (select * from [MwTech].[dbo].[ProductVersions] as xw 
	  inner join [MwTech].[dbo].[Products] as xp
	  on xw.ProductId = xp.Id
	  where mwbaseid = i.pozid
	  and xp.ProductCategoryId = 13)
--

-- Mieszanki
/*
	union all
	  SELECT (SELECT Id from [MwTech].[dbo].[Products] as p where p.[mwbasematId] = i.[matid]) as ProductId
      ,1
      ,'wersja 1'
	  ,''
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
      ,i.[czasZapisu]
	  
	  ,i.[pozid]
	  
	  ,1
	  ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted01UserId
      ,getdate()
      
	  ,1
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted02UserId
      ,getdate()
	  
	  ,1 as DefaultVersion
	  ,1

	  FROM [mwbase].[prdkabat].[mieszanki] as i
	  
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  
	  WHERE not exists
	  (select * from [MwTech].[dbo].[ProductVersions] as xw 
	  inner join [MwTech].[dbo].[Products] as xp
	  on xw.ProductId = xp.Id
	  where mwbaseid = i.pozid
	  and xp.ProductCategoryId = 2)
*/
--

-- Kordy Surowe
	
	union all
	  SELECT (SELECT Id from [MwTech].[dbo].[Products] as p where p.[mwbasematId] = i.[matid]) as ProductId
      ,1
      ,'wersja 1'
	  ,''
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
      ,i.[czasZapisu]
	  
	  ,i.[pozid]
	  
	  ,1
	  ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted01UserId
      ,getdate()
      
	  ,1
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted02UserId
      ,getdate()
	  
	  ,1 as DefaultVersion
	  ,1

	  FROM [mwbase].[prdkabat].[kordy_surowe] as i
	  
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  
	  WHERE not exists
	  (select * from [MwTech].[dbo].[ProductVersions] as xw 
	  inner join [MwTech].[dbo].[Products] as xp
	  on xw.ProductId = xp.Id
	  where mwbaseid = i.pozid
	  and xp.ProductCategoryId = 17)

--

--

-- Druty
	
	union all
	  SELECT (SELECT Id from [MwTech].[dbo].[Products] as p where p.[mwbasematId] = i.[matid]) as ProductId
      ,1
      ,'wersja 1'
	  ,''
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
      ,i.[czasZapisu]
	  
	  ,i.[pozid]
	  
	  ,1
	  ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted01UserId
      ,getdate()
      
	  ,1
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted02UserId
      ,getdate()
	  
	  ,1 as DefaultVersion
	  ,1

	  FROM [mwbase].[prdkabat].[druty] as i
	  
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  
	  WHERE not exists
	  (select * from [MwTech].[dbo].[ProductVersions] as xw 
	  inner join [MwTech].[dbo].[Products] as xp
	  on xw.ProductId = xp.Id
	  where mwbaseid = i.pozid
	  and xp.ProductCategoryId = 18)




-- Kordy Gumowane na 1000 gram !! zamieniam na 1 ba zmiana z gram na kg

	union all
	  SELECT (SELECT Id from [MwTech].[dbo].[Products] as p where p.[mwbasematId] = i.[matid]) as ProductId
      ,1
      ,'wersja 1'
	  ,''
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
      ,i.[czasZapisu]
	  
	  ,i.[pozid]
	  
	  ,1
	  ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted01UserId
      ,getdate()
      
	  ,1
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted02UserId
      ,getdate()
	  
	  ,1 as DefaultVersion
	  ,1

	  FROM [mwbase].[prdkabat].[kordy_gumowane] as i
	  
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  
	  WHERE not exists
	  (select * from [MwTech].[dbo].[ProductVersions] as xw 
	  inner join [MwTech].[dbo].[Products] as xp
	  on xw.ProductId = xp.Id
	  where mwbaseid = i.pozid
	  and xp.ProductCategoryId = 16)

--

-- Kordy Cięte
	
	union all
	  SELECT (SELECT Id from [MwTech].[dbo].[Products] as p where p.[mwbasematId] = i.[matid]) as ProductId
      ,1
      ,'wersja 1'
	  ,''
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
      ,i.[czasZapisu]
	  
	  ,i.[pozid]
	  
	  ,1
	  ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted01UserId
      ,getdate()
      
	  ,1
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted02UserId
      ,getdate()
	  
	  ,1 as DefaultVersion
	  ,1

	  FROM [mwbase].[prdkabat].[kordy_gumowane_ciete] as i
	  
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  
	  WHERE not exists
	  (select * from [MwTech].[dbo].[ProductVersions] as xw 
	  inner join [MwTech].[dbo].[Products] as xp
	  on xw.ProductId = xp.Id
	  where mwbaseid = i.pozid
	  and xp.ProductCategoryId = 15)
--

-- Tkaniny Ochronne
	
	union all
	  SELECT (SELECT Id from [MwTech].[dbo].[Products] as p where p.[mwbasematId] = i.[matid]) as ProductId
      ,1
      ,'wersja 1'
	  ,''
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
      ,i.[czasZapisu]
	  
	  ,i.[pozid]
	  
	  ,1
	  ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted01UserId
      ,getdate()
      
	  ,1
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted02UserId
      ,getdate()
	  
	  ,1 as DefaultVersion
	  ,1

	  FROM [mwbase].[prdkabat].[tkaniny_ochronne] as i
	  
	  INNER JOIN [mwbase].[gm].[materialy] as m
	  ON m.[matid] = i.matid
	  inner join [MwTech].[dbo].[Products] as tp
	  on tp.MwbaseMatid = i.matId
	  
	  WHERE not exists
	  (select * from [MwTech].[dbo].[ProductVersions] as xw 
	  inner join [MwTech].[dbo].[Products] as xp
	  on xw.ProductId = xp.Id
	  where mwbaseid = i.pozid
	  and xp.ProductCategoryId = 20)

--



--

  )

  go

  -- cała reszta nie mająca wersji

  insert into [MwTech].[dbo].[ProductVersions]
  (
      [ProductId]
	  ,[VersionNumber]
	  ,[Name]
      ,[Description]
      ,[CreatedByUserId]
      ,[CreatedDate]
      ,[MwbaseId]
	  
	  ,[IsAccepted01]
	  ,[Accepted01ByUserId]
	  ,[Accepted01Date]
	  
	  ,[IsAccepted02]
	  ,[Accepted02ByUserId]
	  ,[Accepted02Date]
	  
	  ,[DefaultVersion]
	  ,[ProductQty]
  )

  (
		  
	  select tp.Id as ProductId
      ,1
      ,'wersja 1'
	  ,''
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as userId
      ,getdate()
	  
	  ,0
	  
	  ,1
	  ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted01UserId
      ,getdate()
      
	  ,1
      ,'7f2bacf6-564a-4272-a4bb-f76832476024' as Accepted02UserId
      ,getdate()
	  
	  ,1 as DefaultVersion
	  ,1

	  FROM [MwTech].[dbo].[Products] as tp
	  
	  
	  WHERE not exists
	  (select * from [MwTech].[dbo].[ProductVersions] as xw 
	  inner join [MwTech].[dbo].[Products] as xp
	  on xp.Id = xw.ProductId
	  where xw.ProductId = tp.Id
	  )
	  AND tp.ProductCategoryId !=2
	  AND SUBSTRING(tp.ProductNumber,1,4) != 'MIE.'
	  
)	  



