SELECT s.PropertyNumber
      ,sp.ProductPropertiesVersionId
      ,sp.PropertyId
      ,sp.Text
     ,sp.MinValue
	  ,sp.Value
	   ,sp.MaxValue
       ,sp.Value - sp.MinValue as tolerancja_plus_minus
	   ,(sp.MinValue + sp.MaxValue)/2 as cel
  FROM [MwTech].[dbo].ProductProperties as sp
  inner join dbo.Properties as s
  on s.Id = sp.PropertyId
  where s.PropertyNumber = 'dwy_masa_1mb'

  update sp
  set Value = round((sp.MinValue + sp.MaxValue)/2,3)
  ,sp.MinValue = ROUND(sp.MinValue,3)
  ,sp.MaxValue = ROUND(sp.MaxValue,3)
  FROM [MwTech].[dbo].ProductProperties as sp
  inner join dbo.Properties as s
  on s.Id = sp.PropertyId
  where s.PropertyNumber = 'dwy_masa_1mb'

/*
update sp
set MaxValue = Value + sp.Value - sp.MinValue
 FROM [MwTech].[dbo].ProductProperties as sp
  inner join dbo.Properties as s
  on s.Id = sp.PropertyId
  where s.PropertyNumber = 'dst_dlugosc_po_styknieciu'
  */
