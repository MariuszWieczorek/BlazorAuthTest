/****** Script for SelectTopNRows command from SSMS  ******/
SELECT 
      p.ProductCategoryId, ca.CategoryNumber, ca.Name, COUNT(*) as ile
  FROM dbo.Products as p
  inner join dbo.ProductCategories as ca
  on ca.Id = p.ProductCategoryId
  group by p.ProductCategoryId, ca.CategoryNumber, ca.Name

  SELECT 
      COUNT(*) as ile
  FROM dbo.Products as p

  SELECT p.*,u.Email
  FROM dbo.Products as p
  inner join dbo.AspNetUsers as u
  on u.Id = p.CreatedByUserId
  where ProductNumber in ('DKJ40060155TR15KB','DR16533155TR15KBK')