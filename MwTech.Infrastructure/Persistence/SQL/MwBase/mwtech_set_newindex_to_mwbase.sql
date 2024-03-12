/****** Script for SelectTopNRows command from SSMS  ******/
SELECT m.[MATID] 
      ,m.[NAZWA] 
      ,m.[INDEKS] 
	  ,m.NEWINDEKS
	  ,p.ProductNumber
  FROM [mwbase].[gm].[materialy] as m
  inner join MwTech.dbo.Products as p
  on p.ProductNumber = REPLACE(REPLACE(m.INDEKS,'OBI','OBB'),'-B-','-') 
  where m.INDEKS like 'OBI%-B-%'
  

 
    update m
  set m.NEWINDEKS = p.ProductNumber
  FROM [mwbase].[gm].[materialy] as m
  inner join MwTech.dbo.Products as p
  on p.ProductNumber = REPLACE(REPLACE(m.INDEKS,'OBI','OBC'),'-C-','-') 
  where m.INDEKS like 'OBI%-C-%'
 