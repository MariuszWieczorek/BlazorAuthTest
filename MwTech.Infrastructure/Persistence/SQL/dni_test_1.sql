/****** Script for SelectTopNRows command from SSMS  ******/
SELECT 
	   b.SetCategory	
      ,b.[nr_pozycji_nadrzednej]
	  ,b.PartCategory
      ,b.[numer_komponentu]
	  ,d.d1
	  ,d.d2
  FROM [MwTech].[dbo].[mwtech_bom_ifs] as b
  inner join dbo.temp_days as d
  on b.nr_pozycji_nadrzednej = d.ProductNumber or b.[numer_komponentu] = d.ProductNumber
  where b.IsActive = 1 
  and b.PartCategory in ('MIE','NAW')
  -- and b.SetCategory not in ('DWY','BNAZIMNO','OBC','OBB','OKA','OKG','BSOLIDP')
  and [nr_pozycji_nadrzednej] like 'F%' OR [nr_pozycji_nadrzednej] like 'R%' OR [nr_pozycji_nadrzednej] like 'K%'
  order by SetCategory