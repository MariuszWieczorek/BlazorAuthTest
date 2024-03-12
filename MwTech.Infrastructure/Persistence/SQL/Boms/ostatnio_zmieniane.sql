SELECT pr.ProductNumber
  FROM [MwTech].[dbo].[ProductVersions] as v
  inner join dbo.Products as pr 
  on v.ProductId = pr.id
  where 1 = 1
  and year(v.CreatedDate) = year(getdate()) 
  and month(v.CreatedDate) = month(getdate()) 
  and day(v.CreatedDate) = day(getdate()) 

