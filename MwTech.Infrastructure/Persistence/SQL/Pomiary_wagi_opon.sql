select x.CreatedDate, x.Shift, x.ProductNumber, x.ProductName
,Round(x.ProductWeight - (0.05 * x.ProductWeight),2) as MinProductWeight
,Round(x.ProductWeight,2) as NominalWeight
,Round(x.ProductWeight + (0.05 * x.ProductWeight) ,2) as MaxProductWeight
,Round(x.Value,2) as CurrentWeight
from
(
SELECT 
       pr.ProductNumber
	  ,pr.Name as ProductName
      ,h.Shift
      ,h.CreatedDate
      ,u.LastName + ' ' + u.FirstName as FullName
	  ,p.value
	  ,dbo.getProductWeight(pr.Id) as ProductWeight
  FROM dbo.MeasurementHeaders as h
  inner join dbo.Products as pr
  on h.ProductId = pr.Id
  inner join dbo.AspNetUsers as u
  on u.Id = h.CreatedByUserId
  inner join dbo.MeasurementPositions as p
  on p.MeasurementHeaderId = h.id
 ) as x
 order by x.CreatedDate, x.ProductNumber



