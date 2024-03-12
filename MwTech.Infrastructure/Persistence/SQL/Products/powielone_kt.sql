SELECT ca.CategoryNumber, pr.TechCardNumber, COUNT(*)
FROM dbo.Products as pr
inner join dbo.ProductCategories as ca
ON ca.Id = pr.ProductCategoryId
GROUP BY ca.CategoryNumber, pr.TechCardNumber
HAVING COUNT(*) > 1
order by ca.CategoryNumber, pr.TechCardNumber



