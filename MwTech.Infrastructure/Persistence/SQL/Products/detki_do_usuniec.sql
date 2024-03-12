use mwtech;


SELECT ca.CategoryNumber, pr.productNumber
  FROM [MwTech].[dbo].[Products] as pr
  inner join dbo.ProductCategories as ca
  on ca.id = pr.ProductCategoryId
  where 1 = 1
  and pr.IsActive = 1
  and ca.CategoryNumber in ('DET','DET-LUZ','DET-B','DET-KBK','DWY', 'DOB', 'DAP', 'DST', 'DWU', 'DWU-B', 'DKJ', 'DKJ-B')
  and pr.ProductNumber like 'D%'
  and pr.ProductNumber not like '%KB%'
  and pr.ProductNumber not like '%NT%'
  and pr.ProductNumber not like '%-u%'
  and pr.ProductNumber not like '%DEH%'
  and pr.ProductNumber not like '%TE'
  and pr.ProductNumber not like '%-0'
  and pr.ProductNumber not like '%co-but'
  order by ca.CategoryNumber, pr.productNumber




/*
  SELECT ca.CategoryNumber, pr.productNumber
  FROM [MwTech].[dbo].[Products] as pr
  inner join dbo.ProductCategories as ca
  on ca.id = pr.ProductCategoryId
  where 1 = 1
  and ca.CategoryNumber in ('DET','DET-LUZ','DET-B','DET-KBK','DWY', 'DOB', 'DAP', 'DST', 'DWU', 'DWU-B', 'DKJ', 'DKJ-B')
  and pr.ProductNumber like 'D%'
 and 
  (
     pr.ProductNumber like '%KB%'
  or pr.ProductNumber like '%NT%'
  or pr.ProductNumber like '%-u%'
  or pr.ProductNumber like '%DEH%'
  or pr.ProductNumber like '%TE'
  or pr.ProductNumber like '%-0'
  or pr.ProductNumber like '%co-but'
  )
  order by ca.CategoryNumber, pr.productNumber
  */






  /*
--  and ca.CategoryNumber in ('DET','DET-LUZ','DET-B', 'DWY', 'DOB', 'DAP', 'DST', 'DWU', 'DKJ')
--  and ca.CategoryNumber in ('DET','DET-LUZ','DET-B')
--  and ca.CategoryNumber in ('DWY')
  -- z KB
  and pr.ProductNumber not like '%KBWK'
  and pr.ProductNumber not like '%KBK'
  and pr.ProductNumber not like '%KB'
  and pr.ProductNumber not like '%KB-BUT65'
  and pr.ProductNumber not like '%KB-BUT'
  and pr.ProductNumber not like '%KB-BKR'
  and pr.ProductNumber not like '%KB-BBR'
  and pr.ProductNumber not like '%KB-DDK'
  and pr.ProductNumber not like '%KB-SL'
  -- z NT
  and pr.ProductNumber not like '%NTWK'
  and pr.ProductNumber not like '%NT'
  and pr.ProductNumber not like '%NT-BUT65'
  and pr.ProductNumber not like '%NT-SL'
  and pr.ProductNumber not like '%NT-BBR'
  and pr.ProductNumber not like '%NT-BUT'
  and pr.ProductNumber not like '%NT-DDK'
  -- z DEH  
  and pr.ProductNumber not like '%DEH%'
  
  -- do usuniecia
  -- 4 znakowe z __WK
  and pr.ProductNumber not like '%RDWK'
  and pr.ProductNumber not like '%TGWK'
  and pr.ProductNumber not like '%PAWK'
  and pr.ProductNumber not like '%ETWK'
  and pr.ProductNumber not like '%ESWK'
  and pr.ProductNumber not like '%SOWK'
  and pr.ProductNumber not like '%EUWK'
  and pr.ProductNumber not like '%EGWK'
  and pr.ProductNumber not like '%PRWK'
  and pr.ProductNumber not like '%MTWK'
  and pr.ProductNumber not like '%LSWK'
  and pr.ProductNumber not like '%PTWK'
  and pr.ProductNumber not like '%HTWK'
  and pr.ProductNumber not like '%MIWK'
  and pr.ProductNumber not like '%DOWK'
  and pr.ProductNumber not like '%TAWK'
  and pr.ProductNumber not like '%RWWK'
  and pr.ProductNumber not like '%PLWK'
  and pr.ProductNumber not like '%RNWK'
  and pr.ProductNumber not like '%LLWK'
  and pr.ProductNumber not like '%RGWK'
  and pr.ProductNumber not like '%PCWK'
  and pr.ProductNumber not like '%PNWK'
  and pr.ProductNumber not like '%AKWK'
  and pr.ProductNumber not like '%GRWK'
  and pr.ProductNumber not like '%SFWK'
  -- 2 znakowe bez odpowiedników z WK
  and pr.ProductNumber not like '%EC'
  and pr.ProductNumber not like '%AT'
  and pr.ProductNumber not like '%FD'
  and pr.ProductNumber not like '%WZ'
  and pr.ProductNumber not like '%WE'
  and pr.ProductNumber not like '%GW'
  and pr.ProductNumber not like '%FO'
  -- 2 znakowe z odpowiednikami z WK
  and pr.ProductNumber not like '%RD'
  and pr.ProductNumber not like '%TG'
  and pr.ProductNumber not like '%PA'
  and pr.ProductNumber not like '%ET'
  and pr.ProductNumber not like '%ES'
  and pr.ProductNumber not like '%SO'
  and pr.ProductNumber not like '%EU'
  and pr.ProductNumber not like '%EG'
  and pr.ProductNumber not like '%PR'
  and pr.ProductNumber not like '%MT'
  and pr.ProductNumber not like '%LS'
  and pr.ProductNumber not like '%PT'
  and pr.ProductNumber not like '%HT'
  and pr.ProductNumber not like '%MI'
  and pr.ProductNumber not like '%DO'
  and pr.ProductNumber not like '%TA'
  and pr.ProductNumber not like '%RW'
  and pr.ProductNumber not like '%PL'
  and pr.ProductNumber not like '%RN'
  and pr.ProductNumber not like '%LL'
  and pr.ProductNumber not like '%RG'
  and pr.ProductNumber not like '%PC'
  and pr.ProductNumber not like '%PN'
  and pr.ProductNumber not like '%AK'
  and pr.ProductNumber not like '%GR'
  and pr.ProductNumber not like '%SF'
    -- inne 2 znakowe
  and pr.ProductNumber not like '%00'
  and pr.ProductNumber not like '%-TE'
  and pr.ProductNumber not like '%EU-%'
  */

